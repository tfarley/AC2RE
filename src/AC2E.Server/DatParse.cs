using AC2E.Def;
using AC2E.Utils;
using Serilog;
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
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        foreach (BTree.BTEntry entry in node.entries) {
                            allSeenTypes.Add(DbTypeDef.getType(entry.gid));
                            if (mprTypeDef.contains(entry.gid)) {
                                if (!directoryCache.TryGetValue(DbType.MASTER_PROPERTY, out string directory)) {
                                    directory = getOrCreateDir(outputBaseDir, mprTypeDef.strDataDir);
                                    directoryCache[DbType.MASTER_PROPERTY] = directory;
                                }

                                using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                    MasterProperty.instance = new MasterProperty(data);

                                    if (typesToParseSet.Contains(DbType.MASTER_PROPERTY)) {
                                        File.WriteAllText(Path.Combine(directory, $"{entry.gid.id:X8}{mprTypeDef.extension}.txt"), Util.objectToString(MasterProperty.instance));
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

                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;
                        foreach (BTree.BTEntry entry in node.entries) {
                            DbType dbType = DbTypeDef.getType(entry.gid);
                            if (dbType == DbType.UNDEFINED) {
                                Log.Warning($"Unhandled dat gid {entry.gid}.");
                                continue;
                            }

                            if (!typesToParseSet.Contains(dbType)) {
                                continue;
                            }

                            DbTypeDef dbTypeDef = DbTypeDef.TYPE_TO_DEF[dbType];

                            if (!directoryCache.TryGetValue(dbType, out string directory)) {
                                directory = getOrCreateDir(outputBaseDir, dbTypeDef.strDataDir);
                                directoryCache[dbType] = directory;
                            }

                            string outputPath = Path.Combine(directory, $"{entry.gid.id:X8}{dbTypeDef.extension}");

                            parseFile(datReader, entry, outputPath);
                        }
                    }
                }

                Log.Information($"Parsed dat {datFileName}, num files: {numFiles}.");
            }
        }

        private static void parseFile(DatReader datReader, BTree.BTEntry entry, string outputPath) {
            DbType dbType = DbTypeDef.getType(entry.gid);

            switch (dbType) {
                case DbType.APPEARANCE: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var appearanceTable = new AppearanceTable(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(appearanceTable));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.ANIMMAP: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            DataId did = data.ReadDataId();
                            var mapping = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(mapping));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.BEHAVIORTABLE: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var behaviorTable = new BehaviorTable(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(behaviorTable));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.BLOCK_MAP: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var blockMap = new BlockMap(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(blockMap));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.CAMERA_FX: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var cameraFx = new CameraFX(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(cameraFx));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.CHARTEMPLATE: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var charTemplate = new CharTemplate(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(charTemplate));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.DAY_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var dayDesc = new CDayDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(dayDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.ENCOUNTER_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var encounterDesc = new CEncounterDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(encounterDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
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
                case DbType.ENTITYDESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var entityDesc = new EntityDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(entityDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.FX_TABLE: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var dbFxTable = new DBFXTable(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(dbFxTable));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.FXSCRIPT: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var fxScript = new FxScript(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(fxScript));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.GAME_TIME: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var gameTime = new GameTime(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(gameTime));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.INPUTMAPPER: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var enumIdMap = new EnumIDMap(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(enumIdMap));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.KEYMAP: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var keyMap = new CKeyMap(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(keyMap));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.MAPNOTE_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var mapNoteDesc = new CMapNoteDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(mapNoteDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.MATERIALINSTANCE: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var materialInstance = new MaterialInstance(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(materialInstance));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.MATERIALMODIFIER: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var materialModifier = new MaterialModifier(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(materialModifier));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.MOTIONINTERPDESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var motionInterpDesc = new MotionInterpDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(motionInterpDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.MUSICINFO: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var musicInfo = new MusicInfo(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(musicInfo));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.PHYSICS_MATERIAL: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            DataId did = data.ReadDataId();
                            var collection = new PropertyCollection(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(collection));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.PSDESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var psDesc = new PSDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(psDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.PROPERTY_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var propertyDesc = new PropertyDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(propertyDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.QUALITIES: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var qualities = new CBaseQualities(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(qualities));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.QUALITY_FILTER: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            DataId did = data.ReadDataId();
                            var qualityFilter = data.ReadDictionary(data.ReadUInt32, data.ReadInt32);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(qualityFilter));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.RENDERSURFACE:
                case DbType.RENDERSURFACE_LOCAL: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var surface = new RenderSurface(data);

                            File.WriteAllBytes(outputPath, surface.sourceBits);

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.RENDERTEXTURE:
                case DbType.RENDERTEXTURE_LOCAL: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var texture = new RenderTexture(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(texture));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.SOUND_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var soundDesc = new CSoundDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(soundDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.SOUNDINFO: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var soundInfo = new SoundInfo(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(soundInfo));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.SKY_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var skyDesc = new CSkyDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(skyDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.SURFACE_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var surfaceDesc = new CSurfaceDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(surfaceDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.UI_LAYOUT: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var layout = new LayoutDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(layout));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.UI_SCENE: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var uiScene = new UIScene(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(uiScene));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.VALIDMODES: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var availableModes = data.ReadList(data.ReadUInt32);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(availableModes));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                case DbType.VISUAL_DESC: {
                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                            var visualDesc = new VisualDesc(data);

                            File.WriteAllText(outputPath + ".txt", Util.objectToString(visualDesc));

                            checkFullRead(data, entry);
                        }
                        break;
                    }
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

                            checkFullRead(data, entry);
                        }
                        break;
                    }
                default:
                    Log.Warning($"Unhandled DbType {dbType}.");
                    break;
            }
        }

        private static string getOrCreateDir(string baseDir, string path) {
            return Directory.CreateDirectory(Path.Combine(baseDir, path)).FullName;
        }

        private static void checkFullRead(AC2Reader data, BTree.BTEntry entry) {
            if (data.BaseStream.Position < data.BaseStream.Length) {
                Log.Warning($"File {entry.gid.id:X8} was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
            }
        }
    }
}
