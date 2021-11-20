using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AC2RE.DatTool {

    internal class DatParse {

        public static Dictionary<PackageType, List<DataId>> getWeeniePackageTypes(DatReader portalDatReader) {
            Dictionary<PackageType, List<DataId>> packageTypeToDids = new();

            foreach (DataId did in portalDatReader.dids) {
                DbType dbType = DbTypeDef.getType(DbTypeDef.DatType.PORTAL, did);

                if (dbType == DbType.WSTATE) {
                    using (AC2Reader data = portalDatReader.getFileReader(did)) {
                        WState wstate = new(data);
                        packageTypeToDids.GetOrCreate(wstate.packageType).Add(did);
                    }
                }
            }

            return packageTypeToDids;
        }

        public static Dictionary<DataId, string> getMonsterNames(DatReader portalDatReader, DatReader localDatReader) {
            Dictionary<DataId, string> monsterDidToName = new();

            foreach (DataId did in portalDatReader.dids) {
                DbType dbType = DbTypeDef.getType(DbTypeDef.DatType.PORTAL, did);

                if (dbType == DbType.ENTITYDESC) {
                    using (AC2Reader data = portalDatReader.getFileReader(did)) {
                        EntityDesc entityDesc = new(data);
                        if (entityDesc.dataId != DataId.NULL) {
                            DbType relatedDbType = DbTypeDef.getType(DbTypeDef.DatType.PORTAL, entityDesc.dataId);

                            if (relatedDbType == DbType.ENTITYDESC) {
                                using (AC2Reader relatedData = portalDatReader.getFileReader(entityDesc.dataId)) {
                                    EntityDesc relatedEntityDesc = new(relatedData);
                                    if (PackageManager.isPackageType(relatedEntityDesc.packageType, PackageType.MonsterTemplate) && entityDesc.properties != null) {
                                        string? name = null;
                                        string? description = null;
                                        foreach (PropertyGroup propertyGroup in entityDesc.properties.groups) {
                                            foreach (BaseProperty property in propertyGroup.properties) {
                                                if (property.name == PropertyName.Name) {
                                                    name = readString(localDatReader, (StringInfo)property.value);
                                                } else if (property.name == PropertyName.Description) {
                                                    description = readString(localDatReader, (StringInfo)property.value);
                                                }
                                            }
                                        }
                                        monsterDidToName[did] = $"{name} / {description}";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return monsterDidToName;
        }

        public static void getSkills(DatReader portalDatReader, Dictionary<SkillId, DataId> skillIdToDid, Dictionary<SkillId, Skill> skillIdToSkill) {
            foreach (DataId did in portalDatReader.dids) {
                DbType dbType = DbTypeDef.getType(DbTypeDef.DatType.PORTAL, did);

                if (dbType == DbType.WSTATE) {
                    using (AC2Reader data = portalDatReader.getFileReader(did)) {
                        WState wstate = new(data);
                        if (PackageManager.isPackageType(wstate.packageType, PackageType.Skill)) {
                            Skill skill = (Skill)wstate.package;
                            SkillId skillId = (SkillId)skill.enumVal;
                            skillIdToDid[skillId] = did;
                            skillIdToSkill[skillId] = skill;
                        }
                    }
                }
            }
        }

        public static Dictionary<DataId, Effect> getEffects(DatReader portalDatReader) {
            Dictionary<DataId, Effect> didToEffect = new();

            foreach (DataId did in portalDatReader.dids) {
                DbType dbType = DbTypeDef.getType(DbTypeDef.DatType.PORTAL, did);

                if (dbType == DbType.WSTATE) {
                    using (AC2Reader data = portalDatReader.getFileReader(did)) {
                        WState wstate = new(data);
                        if (PackageManager.isPackageType(wstate.packageType, PackageType.Effect)) {
                            didToEffect[did] = (Effect)wstate.package;
                        }
                    }
                }
            }

            return didToEffect;
        }

        private static string? readString(DatReader localDatReader, StringInfo stringInfo) {
            if (stringInfo.literalValue != null) {
                return stringInfo.literalValue;
            }

            using (AC2Reader data = localDatReader.getFileReader(stringInfo.tableDid)) {
                StringTable stringTable = new(data);
                return stringTable.strings.GetValueOrDefault(stringInfo.stringId)?.strings.FirstOrDefault();
            }
        }

        public static void parseDat(DbTypeDef.DatType datType, DatReader datReader, string? outputBaseDir, params DbType[] typesToParse) {
            Logs.GENERAL.info("Parsing dat...",
                "fileName", datReader.datFileName);

            HashSet<DbType> typesToParseSet = new(typesToParse);

            Dictionary<DbType, string?> directoryCache = new();
            Dictionary<DataId, string> didToFileName = new();

            HashSet<DbType> allSeenTypes = new();

            if (datType == DbTypeDef.DatType.PORTAL) {
                // Parse data in first pass that is required for second pass
                MasterProperty.loadMasterProperties(datReader);
                PackageManager.loadPackageTypes(datReader);
            }

            int numFiles = 0;
            foreach (DataId did in datReader.dids) {
                DbType dbType = DbTypeDef.getType(datType, did);

                DbTypeDef dbTypeDef = DbTypeDef.TYPE_TO_DEF[dbType];

                if (!directoryCache.TryGetValue(dbType, out string? directory)) {
                    if (typesToParseSet.Contains(dbType)) {
                        directory = outputBaseDir != null ? getOrCreateDir(outputBaseDir, dbTypeDef.strDataDir) : null;
                        directoryCache[dbType] = directory;
                    }
                }

                if (dbType == DbType.FILE2ID_TABLE) {
                    using (AC2Reader data = datReader.getFileReader(did)) {
                        DBFile2IDTable file2IdTable = new(data);

                        if (directory != null && typesToParseSet.Contains(dbType)) {
                            File.WriteAllText(Path.Combine(directory!, $"{did.id:X8}{dbTypeDef.extension}.txt"), Util.objectToString(file2IdTable));
                        }

                        foreach (var element in file2IdTable.cacheByDid.Values) {
                            foreach ((DataId entryDid, DBFile2IDTable.TFileEntry entry) in element.didToEntry) {
                                didToFileName.Add(entryDid, entry.fileName);
                            }
                        }

                        checkFullRead(data, did);
                    }
                }
            }

            foreach (DataId did in datReader.dids) {
                numFiles++;

                DbType dbType = DbTypeDef.getType(datType, did);

                allSeenTypes.Add(dbType);

                if (dbType == DbType.UNDEFINED) {
                    Logs.GENERAL.warn("Unhandled dat gid",
                        "gid", did);
                    continue;
                }

                if (dbType == DbType.FILE2ID_TABLE || !typesToParseSet.Contains(dbType)) {
                    continue;
                }

                DbTypeDef dbTypeDef = DbTypeDef.TYPE_TO_DEF[dbType];

                if (!directoryCache.TryGetValue(dbType, out string? directory)) {
                    directory = outputBaseDir != null ? getOrCreateDir(outputBaseDir, dbTypeDef.strDataDir) : null;
                    directoryCache[dbType] = directory;
                }

                if (didToFileName.TryGetValue(did, out string? fileName)) {
                    fileName = $"{did.id:X8}_{fileName}"; ;
                } else {
                    fileName = $"{did.id:X8}{dbTypeDef.extension}";
                }

                string? outputPath = directory != null ? Path.Combine(directory, fileName) : null;

                parseFile(datReader, did, dbType, outputPath);
            }

            Logs.GENERAL.info("Parsed dat",
                "fileName", datReader.datFileName,
                "numFiles", numFiles,
                "allSeenTypes", Util.objectToString(allSeenTypes));
        }

        private static void parseFile(DatReader datReader, DataId did, DbType dbType, string? outputPath) {
            switch (dbType) {
                case DbType.APPEARANCE:
                    readAndDump(datReader, did, outputPath, data => new AppearanceTable(data));
                    break;
                case DbType.ANIMMAP:
                    readAndDump(datReader, did, outputPath, data => {
                        DataId did = data.ReadDataId();
                        return data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
                    });
                    break;
                case DbType.BEHAVIORTABLE:
                    readAndDump(datReader, did, outputPath, data => new BehaviorTable(data));
                    break;
                case DbType.BLOCK_MAP:
                    readAndDump(datReader, did, outputPath, data => new BlockMap(data));
                    break;
                case DbType.CAMERA_FX:
                    readAndDump(datReader, did, outputPath, data => new CameraFX(data));
                    break;
                case DbType.CHARTEMPLATE:
                    readAndDump(datReader, did, outputPath, data => new CharTemplate(data));
                    break;
                case DbType.DATFILEDATA: {
                        DatFileDataId datFileDataId = (DatFileDataId)did.id;

                        switch (datFileDataId) {
                            case DatFileDataId.IterationList:
                                readAndDump(datReader, did, outputPath, data => new CMostlyConsecutiveIntSet(data));
                                break;
                            default:
                                throw new NotImplementedException(datFileDataId.ToString());
                        }
                        break;
                    }
                case DbType.DAY_DESC:
                    readAndDump(datReader, did, outputPath, data => new CDayDesc(data));
                    break;
                case DbType.DBANIMATOR:
                    readAndDump(datReader, did, outputPath, data => new DBAnimator(data));
                    break;
                case DbType.ENCODED_WAV: {
                        if (outputPath == null) {
                            break;
                        }
                        using (Stream output = File.Open(outputPath + ".wav", FileMode.Create, FileAccess.Write, FileShare.Read)) {
                            // 4 DID + 4 file size
                            int ac2HeaderSize = sizeof(uint) + sizeof(uint);
                            datReader.readFile(did, output, ac2HeaderSize);
                        }
                        break;
                    }
                case DbType.ENCOUNTER_DESC:
                    readAndDump(datReader, did, outputPath, data => new CEncounterDesc(data));
                    break;
                case DbType.ENUM_MAPPER: {
                        using (StreamWriter? output = outputPath != null ? new(File.Open(outputPath + ".txt", FileMode.Create, FileAccess.Write, FileShare.Read)) : null)
                        using (AC2Reader data = datReader.getFileReader(did)) {
                            EnumMapper emp = new(data);

                            if (output != null) {
                                foreach ((uint id, string str) in emp.idToString) {
                                    output.WriteLine($"{id}\t{str}");
                                }
                            }

                            checkFullRead(data, did);
                        }
                        break;
                    }
                case DbType.ENTITYDESC:
                    readAndDump(datReader, did, outputPath, data => new EntityDesc(data));
                    break;
                case DbType.ENVCELL:
                    readAndDump(datReader, did, outputPath, data => new CEnvCell(data));
                    break;
                case DbType.FILE2ID_TABLE:
                    readAndDump(datReader, did, outputPath, data => new DBFile2IDTable(data));
                    break;
                case DbType.FOG_DESC:
                    readAndDump(datReader, did, outputPath, data => new CFogDesc(data));
                    break;
                case DbType.FX_TABLE:
                    readAndDump(datReader, did, outputPath, data => new DBFXTable(data));
                    break;
                case DbType.FXSCRIPT:
                    readAndDump(datReader, did, outputPath, data => new FxScript(data));
                    break;
                case DbType.GAME_TIME:
                    readAndDump(datReader, did, outputPath, data => new GameTime(data));
                    break;
                case DbType.INPUTMAPPER:
                    readAndDump(datReader, did, outputPath, data => new EnumIDMap(data));
                    break;
                case DbType.KEYMAP:
                    readAndDump(datReader, did, outputPath, data => new CKeyMap(data));
                    break;
                case DbType.LANDBLOCKDATA:
                    readAndDump(datReader, did, outputPath, data => new CLandBlockData(data));
                    break;
                case DbType.LANDBLOCKINFO:
                    readAndDump(datReader, did, outputPath, data => new CLandBlockInfo(data));
                    break;
                case DbType.LIGHTINFO:
                    readAndDump(datReader, did, outputPath, data => new CLightInfo(data));
                    break;
                case DbType.MAPNOTE_DESC:
                    readAndDump(datReader, did, outputPath, data => new CMapNoteDesc(data));
                    break;
                case DbType.MASTER_PROPERTY:
                    readAndDump(datReader, did, outputPath, data => new MasterProperty(data));
                    break;
                case DbType.MATERIALINSTANCE:
                    readAndDump(datReader, did, outputPath, data => new MaterialInstance(data));
                    break;
                case DbType.MATERIALMODIFIER:
                    readAndDump(datReader, did, outputPath, data => new MaterialModifier(data));
                    break;
                case DbType.MESH:
                    readAndDump(datReader, did, outputPath, data => {
                        DataId did = data.ReadDataId();
                        return new CStaticMesh(data);
                    });
                    break;
                case DbType.MOTIONINTERPDESC:
                    readAndDump(datReader, did, outputPath, data => new MotionInterpDesc(data));
                    break;
                case DbType.MUSICINFO:
                    readAndDump(datReader, did, outputPath, data => new MusicInfo(data));
                    break;
                case DbType.PHYSICS_MATERIAL:
                    readAndDump(datReader, did, outputPath, data => {
                        DataId did = data.ReadDataId();
                        return new PropertyCollection(data);
                    });
                    break;
                case DbType.PSDESC:
                    readAndDump(datReader, did, outputPath, data => new PSDesc(data));
                    break;
                case DbType.PROPERTY_DESC:
                    readAndDump(datReader, did, outputPath, data => new PropertyDesc(data));
                    break;
                case DbType.QUALITIES:
                    readAndDump(datReader, did, outputPath, data => new CBaseQualities(data));
                    break;
                case DbType.QUALITY_FILTER:
                    readAndDump(datReader, did, outputPath, data => {
                        DataId did = data.ReadDataId();
                        return data.ReadDictionary(data.ReadUInt32, data.ReadInt32);
                    });
                    break;
                case DbType.RENDERMATERIAL:
                    readAndDump(datReader, did, outputPath, data => new RenderMaterial(data));
                    break;
                case DbType.RENDERSURFACE:
                case DbType.RENDERSURFACE_LOCAL: {
                        using (AC2Reader data = datReader.getFileReader(did)) {
                            RenderSurface surface = new(data);

                            if (outputPath != null) {
                                File.WriteAllBytes(outputPath, surface.sourceData);
                            }

                            checkFullRead(data, did);
                        }
                        break;
                    }
                case DbType.RENDERTEXTURE:
                case DbType.RENDERTEXTURE_LOCAL:
                    readAndDump(datReader, did, outputPath, data => new RenderTexture(data));
                    break;
                case DbType.SETUP:
                    readAndDump(datReader, did, outputPath, data => new CSetup(data));
                    break;
                case DbType.SOUND_DESC:
                    readAndDump(datReader, did, outputPath, data => new CSoundDesc(data));
                    break;
                case DbType.SOUNDINFO:
                    readAndDump(datReader, did, outputPath, data => new SoundInfo(data));
                    break;
                case DbType.SKY_DESC:
                    readAndDump(datReader, did, outputPath, data => new CSkyDesc(data));
                    break;
                case DbType.STRING_STATE:
                    readAndDump(datReader, did, outputPath, data => new StringState(data));
                    break;
                case DbType.STRING_TABLE:
                    readAndDump(datReader, did, outputPath, data => new StringTable(data));
                    break;
                case DbType.SURFACE_DESC:
                    readAndDump(datReader, did, outputPath, data => new CSurfaceDesc(data));
                    break;
                case DbType.UI_LAYOUT:
                    readAndDump(datReader, did, outputPath, data => new LayoutDesc(data));
                    break;
                case DbType.UI_SCENE:
                    readAndDump(datReader, did, outputPath, data => new UIScene(data));
                    break;
                case DbType.VALIDMODES:
                    readAndDump(datReader, did, outputPath, data => data.ReadList(() => (ModeId)data.ReadUInt32()));
                    break;
                case DbType.VISUAL_DESC:
                    readAndDump(datReader, did, outputPath, data => new VisualDesc(data));
                    break;
                case DbType.WAVE: {
                        if (outputPath == null) {
                            break;
                        }
                        using (Stream output = File.Open(outputPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                        using (BinaryWriter outputWriter = new(output)) {
                            BTEntry entry = datReader.getEntry(did);
                            // 4 DID + 4 unk + 4 file size
                            int ac2HeaderSize = sizeof(uint) + sizeof(uint) + sizeof(uint);
                            int fmtHeaderSize = 16;
                            outputWriter.Write(new[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' });
                            // 4 "WAVE" + 4 "fmt " + 4 fmtHeaderSize + 16 fmtHeader + 4 "data" + 4 dataSize
                            outputWriter.Write(entry.size - ac2HeaderSize + sizeof(uint) + sizeof(uint) + sizeof(uint) + fmtHeaderSize + sizeof(uint) + sizeof(uint));
                            outputWriter.Write(new[] { (byte)'W', (byte)'A', (byte)'V', (byte)'E' });

                            outputWriter.Write(new[] { (byte)'f', (byte)'m', (byte)'t', (byte)' ' });
                            outputWriter.Write(fmtHeaderSize);
                            // + 4 to skip nextBlockOffset
                            datReader.data.BaseStream.Seek(entry.offset + sizeof(uint) + ac2HeaderSize, SeekOrigin.Begin);
                            byte[] fmtHeader = datReader.data.ReadBytes(fmtHeaderSize);
                            outputWriter.Write(fmtHeader);

                            outputWriter.Write(new[] { (byte)'d', (byte)'a', (byte)'t', (byte)'a' });
                            outputWriter.Write(entry.size - ac2HeaderSize - fmtHeaderSize);
                            datReader.readFile(did, output, ac2HeaderSize + fmtHeaderSize);
                        }
                        break;
                    }
                case DbType.WSTATE:
                    readAndDump(datReader, did, outputPath, data => new WState(data));
                    break;
                case DbType.WLIB: {
                        using (AC2Reader data = datReader.getFileReader(did)) {
                            WLib wlib = new(data);
                            if (outputPath != null) {
                                using (StreamWriter output = new(File.Open(outputPath + ".packages.txt", FileMode.Create, FileAccess.Write, FileShare.Read))) {
                                    Dump.dumpPackages(output, wlib.byteStream);
                                }
                            }

                            Disasm disasm = new(wlib.byteStream);
                            if (outputPath != null) {
                                using (StreamWriter output = new(File.Open(outputPath + ".disasm.txt", FileMode.Create, FileAccess.Write, FileShare.Read))) {
                                    disasm.write(output);
                                }
                            }

                            if (outputPath != null) {
                                File.WriteAllText(outputPath + ".frames.txt", Util.objectToString(wlib.byteStream.frames));
                            }

                            checkFullRead(data, did);
                        }
                        break;
                    }
                default:
                    Logs.GENERAL.warn("Unhandled DbType",
                        "dbType", dbType);
                    break;
            }
        }

        private static void readAndDump(DatReader datReader, DataId did, string? outputPath, Func<AC2Reader, object> readFunc) {
            using (AC2Reader data = datReader.getFileReader(did)) {
                object readObj = readFunc.Invoke(data);

                if (outputPath != null) {
                    File.WriteAllText(outputPath + ".txt", Util.objectToString(readObj));
                }

                checkFullRead(data, did);
            }
        }

        private static string getOrCreateDir(string baseDir, string path) {
            return Directory.CreateDirectory(Path.Combine(baseDir, path)).FullName;
        }

        private static void checkFullRead(AC2Reader data, DataId did) {
            if (data.BaseStream.Position < data.BaseStream.Length) {
                Logs.GENERAL.warn("File was not fully read",
                    "did", $"{did.id:X8}",
                    "pos", data.BaseStream.Position,
                    "len", data.BaseStream.Length);
            }
        }

        public static DataId getDidContainingOffset(DatReader datReader, uint offset) {
            foreach (DataId did in datReader.dids) {
                foreach ((uint blockOffset, int blockSize) in datReader.getFileBlocks(did)) {
                    if (offset >= blockOffset && offset < blockOffset + blockSize) {
                        return did;
                    }
                }
            }
            return DataId.NULL;
        }
    }
}
