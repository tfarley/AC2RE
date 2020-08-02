using AC2E.Def;
using AC2E.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Server {

    internal class DatParse {

        public static void parseDat(string datFileName, string outputBaseDir, params DbType[] typesToParse) {
            if (File.Exists(datFileName)) {
                HashSet<DbType> typesToParseSet = new HashSet<DbType>(typesToParse);

                Dictionary<DbType, string> directoryCache = new Dictionary<DbType, string>();

                DbTypeDef mprTypeDef = DbTypeDef.TYPE_TO_DEF[DbType.MASTER_PROPERTY];

                HashSet<DbType> allSeenTypes = new HashSet<DbType>();

                int numFiles = 0;
                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                    BTree filesystemTree = new BTree(datReader);
                    // Parse data in first pass that is required for second pass
                    bool foundMasterProperty = false;
                    foreach (BTNode node in filesystemTree.offsetToNode.Values) {
                        foreach (BTEntry entry in node.entries) {
                            allSeenTypes.Add(DbTypeDef.getType(entry.did));
                            if (mprTypeDef.contains(entry.did)) {
                                if (!directoryCache.TryGetValue(DbType.MASTER_PROPERTY, out string directory)) {
                                    directory = getOrCreateDir(outputBaseDir, mprTypeDef.strDataDir);
                                    directoryCache[DbType.MASTER_PROPERTY] = directory;
                                }

                                using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                    MasterProperty.instance = new MasterProperty(data);

                                    if (typesToParseSet.Contains(DbType.MASTER_PROPERTY)) {
                                        File.WriteAllText(Path.Combine(directory, $"{entry.did.id:X8}{mprTypeDef.extension}.txt"), Util.objectToString(MasterProperty.instance));
                                    }

                                    checkFullRead(data, entry);
                                }

                                foundMasterProperty = true;
                                break;
                            }
                        }

                        if (foundMasterProperty) {
                            break;
                        }
                    }

                    Log.Information($"All seen types in {datFileName}: {Util.objectToString(allSeenTypes)}.");

                    foreach (BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;
                        foreach (BTEntry entry in node.entries) {
                            DbType dbType = DbTypeDef.getType(entry.did);
                            if (dbType == DbType.UNDEFINED) {
                                Log.Warning($"Unhandled dat gid {entry.did}.");
                                continue;
                            }

                            if (dbType == DbType.MASTER_PROPERTY || !typesToParseSet.Contains(dbType)) {
                                continue;
                            }

                            DbTypeDef dbTypeDef = DbTypeDef.TYPE_TO_DEF[dbType];

                            if (!directoryCache.TryGetValue(dbType, out string directory)) {
                                directory = getOrCreateDir(outputBaseDir, dbTypeDef.strDataDir);
                                directoryCache[dbType] = directory;
                            }

                            string outputPath = Path.Combine(directory, $"{entry.did.id:X8}{dbTypeDef.extension}");

                            parseFile(datReader, entry, outputPath);
                        }
                    }
                }

                Log.Information($"Parsed dat {datFileName}, num files: {numFiles}.");
            }
        }

        private static void parseFile(DatReader datReader, BTEntry entry, string outputPath) {
            DbType dbType = DbTypeDef.getType(entry.did);

            switch (dbType) {
                case DbType.APPEARANCE:
                    readAndDump(datReader, entry, outputPath, data => new AppearanceTable(data));
                    break;
                case DbType.ANIMMAP:
                    readAndDump(datReader, entry, outputPath, data => {
                        DataId did = data.ReadDataId();
                        return data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
                    });
                    break;
                case DbType.BEHAVIORTABLE:
                    readAndDump(datReader, entry, outputPath, data => new BehaviorTable(data));
                    break;
                case DbType.BLOCK_MAP:
                    readAndDump(datReader, entry, outputPath, data => new BlockMap(data));
                    break;
                case DbType.CAMERA_FX:
                    readAndDump(datReader, entry, outputPath, data => new CameraFX(data));
                    break;
                case DbType.CHARTEMPLATE:
                    readAndDump(datReader, entry, outputPath, data => new CharTemplate(data));
                    break;
                case DbType.DATFILEDATA: {
                        DatFileDataId datFileDataId = (DatFileDataId)entry.did.id;

                        switch (datFileDataId) {
                            case DatFileDataId.ITERATION_LIST:
                                readAndDump(datReader, entry, outputPath, data => new CMostlyConsecutiveIntSet(data));
                                break;
                            default:
                                throw new NotImplementedException(datFileDataId.ToString());
                        }
                        break;
                    }
                case DbType.DAY_DESC:
                    readAndDump(datReader, entry, outputPath, data => new CDayDesc(data));
                    break;
                case DbType.ENCOUNTER_DESC:
                    readAndDump(datReader, entry, outputPath, data => new CEncounterDesc(data));
                    break;
                case DbType.ENUM_MAPPER: {
                        using (StreamWriter output = new StreamWriter(File.OpenWrite(outputPath + ".txt")))
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var emp = new EnumMapper(data);

                            foreach (var mapping in emp.idToString) {
                                output.WriteLine($"{mapping.Key}\t{mapping.Value}");
                            }

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.ENTITYDESC:
                    readAndDump(datReader, entry, outputPath, data => new EntityDesc(data));
                    break;
                case DbType.FX_TABLE:
                    readAndDump(datReader, entry, outputPath, data => new DBFXTable(data));
                    break;
                case DbType.FXSCRIPT:
                    readAndDump(datReader, entry, outputPath, data => new FxScript(data));
                    break;
                case DbType.GAME_TIME:
                    readAndDump(datReader, entry, outputPath, data => new GameTime(data));
                    break;
                case DbType.INPUTMAPPER:
                    readAndDump(datReader, entry, outputPath, data => new EnumIDMap(data));
                    break;
                case DbType.KEYMAP:
                    readAndDump(datReader, entry, outputPath, data => new CKeyMap(data));
                    break;
                case DbType.MAPNOTE_DESC:
                    readAndDump(datReader, entry, outputPath, data => new CMapNoteDesc(data));
                    break;
                case DbType.MATERIALINSTANCE:
                    readAndDump(datReader, entry, outputPath, data => new MaterialInstance(data));
                    break;
                case DbType.MATERIALMODIFIER:
                    readAndDump(datReader, entry, outputPath, data => new MaterialModifier(data));
                    break;
                case DbType.MOTIONINTERPDESC:
                    readAndDump(datReader, entry, outputPath, data => new MotionInterpDesc(data));
                    break;
                case DbType.MUSICINFO:
                    readAndDump(datReader, entry, outputPath, data => new MusicInfo(data));
                    break;
                case DbType.PHYSICS_MATERIAL:
                    readAndDump(datReader, entry, outputPath, data => {
                        DataId did = data.ReadDataId();
                        return new PropertyCollection(data);
                    });
                    break;
                case DbType.PSDESC:
                    readAndDump(datReader, entry, outputPath, data => new PSDesc(data));
                    break;
                case DbType.PROPERTY_DESC:
                    readAndDump(datReader, entry, outputPath, data => new PropertyDesc(data));
                    break;
                case DbType.QUALITIES:
                    readAndDump(datReader, entry, outputPath, data => new CBaseQualities(data));
                    break;
                case DbType.QUALITY_FILTER:
                    readAndDump(datReader, entry, outputPath, data => {
                        DataId did = data.ReadDataId();
                        return data.ReadDictionary(data.ReadUInt32, data.ReadInt32);
                    });
                    break;
                case DbType.RENDERSURFACE:
                case DbType.RENDERSURFACE_LOCAL: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var surface = new RenderSurface(data);

                            File.WriteAllBytes(outputPath, surface.sourceData);

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.RENDERTEXTURE:
                case DbType.RENDERTEXTURE_LOCAL:
                    readAndDump(datReader, entry, outputPath, data => new RenderTexture(data));
                    break;
                case DbType.SOUND_DESC:
                    readAndDump(datReader, entry, outputPath, data => new CSoundDesc(data));
                    break;
                case DbType.SOUNDINFO:
                    readAndDump(datReader, entry, outputPath, data => new SoundInfo(data));
                    break;
                case DbType.SKY_DESC:
                    readAndDump(datReader, entry, outputPath, data => new CSkyDesc(data));
                    break;
                case DbType.STRING_TABLE:
                    readAndDump(datReader, entry, outputPath, data => new StringTable(data));
                    break;
                case DbType.SURFACE_DESC:
                    readAndDump(datReader, entry, outputPath, data => new CSurfaceDesc(data));
                    break;
                case DbType.UI_LAYOUT:
                    readAndDump(datReader, entry, outputPath, data => new LayoutDesc(data));
                    break;
                case DbType.UI_SCENE:
                    readAndDump(datReader, entry, outputPath, data => new UIScene(data));
                    break;
                case DbType.VALIDMODES:
                    readAndDump(datReader, entry, outputPath, data => data.ReadList(data.ReadUInt32));
                    break;
                case DbType.VISUAL_DESC:
                    readAndDump(datReader, entry, outputPath, data => new VisualDesc(data));
                    break;
                case DbType.WLIB: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var wlib = new WLib(data);
                            using (StreamWriter output = new StreamWriter(File.OpenWrite(outputPath + ".packages.txt"))) {
                                Dump.dumpPackages(output, wlib.byteStream);
                            }

                            var disasm = new Disasm(wlib.byteStream);
                            using (StreamWriter output = new StreamWriter(File.OpenWrite(outputPath + ".disasm.txt"))) {
                                disasm.write(output);
                            }

                            File.WriteAllText(outputPath + ".frames.txt", Util.objectToString(wlib.byteStream.frames));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                default:
                    Log.Warning($"Unhandled DbType {dbType}.");
                    break;
            }
        }

        private static void readAndDump(DatReader datReader, BTEntry entry, string outputPath, Func<AC2Reader, object> readFunc) {
            using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                File.WriteAllText(outputPath + ".txt", Util.objectToString(readFunc.Invoke(data)));

                checkFullRead(data, entry);
            }
        }

        private static string getOrCreateDir(string baseDir, string path) {
            return Directory.CreateDirectory(Path.Combine(baseDir, path)).FullName;
        }

        private static void checkFullRead(AC2Reader data, BTEntry entry) {
            if (data.BaseStream.Position < data.BaseStream.Length) {
                Log.Warning($"File {entry.did.id:X8} was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
            }
        }
    }
}
