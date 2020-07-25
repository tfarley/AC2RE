using AC2E.Def;
using AC2E.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AC2E.Server {

    internal class Program {

        private static readonly AC2Server SERVER = new AC2Server();

        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Hello World!");

            parseDat("G:\\Asheron's Call 2\\portal.dat", "portalparsed", DbType.PSDESC);
            //parseDat("G:\\Asheron's Call 2\\cell_1.dat", "cell1parsed", DbType.ENVCELL);
            //parseDat("G:\\Asheron's Call 2\\local_English.dat", "localparsed", DbType.RENDERSURFACE_LOCAL, DbType.STRING, DbType.STRING_TABLE);

            runServer();
        }

        private static void printDecryptedString(byte[] bytes, Encoding encoding) {
            AC2Crypto.decrypt(bytes, 0, bytes.Length);
            Log.Information($"Str: {encoding.GetString(bytes)}");
        }

        private static void printEncryptedString(string str, Encoding encoding) {
            byte[] bytes = encoding.GetBytes(str);
            AC2Crypto.encrypt(bytes, 0, bytes.Length);
            Log.Information($"Str: {str}");
            Log.Information($"Enc: {BitConverter.ToString(bytes).Replace("-", "")}");
        }

        private static void parseDat(string datFileName, string outputBaseDir, params DbType[] typesToParse) {
            if (File.Exists(datFileName)) {
                HashSet<DbType> typesToParseSet = new HashSet<DbType>(typesToParse);

                Dictionary<DbType, string> directoryCache = new Dictionary<DbType, string>();

                DbTypeDef mprTypeDef = DbTypeDef.TYPE_TO_DEF[DbType.MASTER_PROPERTY];

                int numFiles = 0;
                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                    BTree filesystemTree = new BTree(datReader);
                    // Parse data in first pass that is required for second pass
                    bool foundMasterProperty = false;
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        foreach (BTree.BTEntry entry in node.entries) {
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
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;
                        foreach (BTree.BTEntry entry in node.entries) {
                            if (entry.gid.id >= 0x80000000) {
                                //Log.Warning($"Dat gid {entry.gid} beyond known limit.");
                                continue;
                            }

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

                            switch (DbTypeDef.getType(entry.gid)) {
                                case DbType.CAMERA_FX: {
                                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                            var cameraFx = new CameraFX(data);

                                            File.WriteAllText(outputPath + ".txt", Util.objectToString(cameraFx));

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
                                case DbType.MAPNOTE_DESC: {
                                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                            var mapNoteDesc = new CMapNoteDesc(data);

                                            File.WriteAllText(outputPath + ".txt", Util.objectToString(mapNoteDesc));

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
                                case DbType.RENDERSURFACE: {
                                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                            var surface = new RenderSurface(data);

                                            File.WriteAllBytes(outputPath, surface.sourceBits);

                                            checkFullRead(data, entry);
                                        }
                                        break;
                                    }
                                case DbType.RENDERSURFACE_LOCAL: {
                                        using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                            var surface = new RenderSurface(data);

                                            File.WriteAllBytes(outputPath, surface.sourceBits);

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
                                            var vDesc = new VisualDesc(data);

                                            File.WriteAllText(outputPath + ".txt", Util.objectToString(vDesc));

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
                    }
                }

                Log.Information($"Parsed dat {datFileName}, num files: {numFiles}.");
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

        private static void runServer() {
            SERVER.start(7777);

            while (true) {
                SERVER.processReceive();
                Thread.Sleep(10);
            }
        }
    }
}
