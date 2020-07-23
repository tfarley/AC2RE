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

            parsePortalDat("G:\\Asheron's Call 2\\portal.dat", DbType.UI_SCENE);
            parseLocalDat("G:\\Asheron's Call 2\\local_English.dat", DbType.RENDERSURFACE_LOCAL, DbType.STRING, DbType.STRING_TABLE);

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

        private static void parsePortalDat(string datFileName, params DbType[] typesToParse) {
            if (File.Exists(datFileName)) {
                HashSet<DbType> typesToParseSet = new HashSet<DbType>(typesToParse);

                DbTypeDef cameraFxTypeDef = DbTypeDef.DEFS[DbType.CAMERA_FX];
                DbTypeDef empTypeDef = DbTypeDef.DEFS[DbType.ENUM_MAPPER];
                DbTypeDef entityTypeDef = DbTypeDef.DEFS[DbType.ENTITYDESC];
                DbTypeDef fxScriptTypeDef = DbTypeDef.DEFS[DbType.FXSCRIPT];
                DbTypeDef mapnoteTypeDef = DbTypeDef.DEFS[DbType.MAPNOTE_DESC];
                DbTypeDef mprTypeDef = DbTypeDef.DEFS[DbType.MASTER_PROPERTY];
                DbTypeDef pmatTypeDef = DbTypeDef.DEFS[DbType.PHYSICS_MATERIAL];
                DbTypeDef surfaceTypeDef = DbTypeDef.DEFS[DbType.RENDERSURFACE];
                DbTypeDef uiSceneTypeDef = DbTypeDef.DEFS[DbType.UI_SCENE];
                DbTypeDef validmodesTypeDef = DbTypeDef.DEFS[DbType.VALIDMODES];
                DbTypeDef vdTypeDef = DbTypeDef.DEFS[DbType.VISUAL_DESC];
                DbTypeDef wlibTypeDef = DbTypeDef.DEFS[DbType.WLIB];

                string parsedDirectory = "portalparsed";
                string cameraFxDirectory = getOrCreateDir(parsedDirectory, cameraFxTypeDef.strDataDir);
                string empDirectory = getOrCreateDir(parsedDirectory, empTypeDef.strDataDir);
                string entityDirectory = getOrCreateDir(parsedDirectory, entityTypeDef.strDataDir);
                string fxScriptDirectory = getOrCreateDir(parsedDirectory, fxScriptTypeDef.strDataDir);
                string mapnotesDirectory = getOrCreateDir(parsedDirectory, mapnoteTypeDef.strDataDir);
                string mprDirectory = getOrCreateDir(parsedDirectory, mprTypeDef.strDataDir);
                string pmatDirectory = getOrCreateDir(parsedDirectory, pmatTypeDef.strDataDir);
                string surfaceDirectory = getOrCreateDir(parsedDirectory, surfaceTypeDef.strDataDir);
                string uiSceneDirectory = getOrCreateDir(parsedDirectory, uiSceneTypeDef.strDataDir);
                string validmodesDirectory = getOrCreateDir(parsedDirectory, validmodesTypeDef.strDataDir);
                string vdDirectory = getOrCreateDir(parsedDirectory, vdTypeDef.strDataDir);
                string wlibDirectory = getOrCreateDir(parsedDirectory, wlibTypeDef.strDataDir);

                int numFiles = 0;
                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                    BTree filesystemTree = new BTree(datReader);
                    // Parse data in first pass that is required for second pass
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        foreach (BTree.BTEntry entry in node.entries) {
                            if (mprTypeDef.contains(entry.gid)) {
                                using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                    MasterProperty.instance = new MasterProperty(data);

                                    if (typesToParseSet.Contains(DbType.MASTER_PROPERTY)) {
                                        File.WriteAllText(Path.Combine(mprDirectory, $"{entry.gid.id:X8}{mprTypeDef.extension}.txt"), Util.objectToString(MasterProperty.instance));
                                    }

                                    checkFullRead(data, entry);
                                }
                            }
                        }
                    }
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;
                        foreach (BTree.BTEntry entry in node.entries) {
                            if (cameraFxTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.CAMERA_FX)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var cameraFx = new CameraFX(data);

                                        File.WriteAllText(Path.Combine(cameraFxDirectory, $"{entry.gid.id:X8}{cameraFxTypeDef.extension}.txt"), Util.objectToString(cameraFx));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (empTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.ENUM_MAPPER)) {
                                    using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(empDirectory, $"{entry.gid.id:X8}{empTypeDef.extension}.txt"))))
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var emp = new EnumMapper(data);

                                        foreach (var mapping in emp.idToString) {
                                            output.WriteLine($"{mapping.Key}\t{mapping.Value}");
                                        }

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (entityTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.ENTITYDESC)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var entityDesc = new EntityDesc(data);

                                        File.WriteAllText(Path.Combine(entityDirectory, $"{entry.gid.id:X8}{entityTypeDef.extension}.txt"), Util.objectToString(entityDesc));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (fxScriptTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.FXSCRIPT)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var fxScript = new FxScript(data);

                                        File.WriteAllText(Path.Combine(fxScriptDirectory, $"{entry.gid.id:X8}{fxScriptTypeDef.extension}.txt"), Util.objectToString(fxScript));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (mapnoteTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.MAPNOTE_DESC)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var mapNoteDesc = new CMapNoteDesc(data);

                                        File.WriteAllText(Path.Combine(mapnotesDirectory, $"{entry.gid.id:X8}{mapnoteTypeDef.extension}.txt"), Util.objectToString(mapNoteDesc));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (pmatTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.PHYSICS_MATERIAL)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        DataId did = data.ReadDataId();
                                        var collection = new PropertyCollection(data);

                                        File.WriteAllText(Path.Combine(pmatDirectory, $"{entry.gid.id:X8}{pmatTypeDef.extension}.txt"), Util.objectToString(collection));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (surfaceTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.RENDERSURFACE)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var surface = new RenderSurface(data);

                                        File.WriteAllBytes(Path.Combine(surfaceDirectory, $"{entry.gid.id:X8}{surfaceTypeDef.extension}"), surface.content);

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (uiSceneTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.UI_SCENE)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var uiScene = new UIScene(data);

                                        File.WriteAllText(Path.Combine(uiSceneDirectory, $"{entry.gid.id:X8}{uiSceneTypeDef.extension}.txt"), Util.objectToString(uiScene));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (validmodesTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.VALIDMODES)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var availableModes = data.ReadList(data.ReadUInt32);

                                        File.WriteAllText(Path.Combine(validmodesDirectory, $"{entry.gid.id:X8}{validmodesTypeDef.extension}.txt"), Util.objectToString(availableModes));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (vdTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.VISUAL_DESC)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var vDesc = new VisualDesc(data);

                                        File.WriteAllText(Path.Combine(vdDirectory, $"{entry.gid.id:X8}{vdTypeDef.extension}.txt"), Util.objectToString(vDesc));

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (wlibTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.WLIB)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var wlib = new WLib(data);
                                        using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(wlibDirectory, $"{entry.gid.id:X8}{wlibTypeDef.extension}.packages.txt")))) {
                                            Dump.dumpPackages(output, wlib.byteStream);
                                        }

                                        var disasm = new Disasm(wlib.byteStream);
                                        using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(wlibDirectory, $"{entry.gid.id:X8}{wlibTypeDef.extension}.disasm.txt")))) {
                                            disasm.write(output);
                                        }

                                        checkFullRead(data, entry);
                                    }
                                }
                            }
                        }
                    }
                }

                Log.Information($"Parsed portal dat, num files: {numFiles}.");
            }
        }

        private static void parseLocalDat(string datFileName, params DbType[] typesToParse) {
            if (File.Exists(datFileName)) {
                HashSet<DbType> typesToParseSet = new HashSet<DbType>(typesToParse);

                DbTypeDef surfacelocalTypeDef = DbTypeDef.DEFS[DbType.RENDERSURFACE_LOCAL];
                DbTypeDef stringTypeDef = DbTypeDef.DEFS[DbType.STRING];
                DbTypeDef stringTableTypeDef = DbTypeDef.DEFS[DbType.STRING_TABLE];

                string parsedDirectory = "localparsed";
                string surfacelocalDirectory = getOrCreateDir(parsedDirectory, surfacelocalTypeDef.strDataDir);
                string stringDirectory = getOrCreateDir(parsedDirectory, stringTypeDef.strDataDir);
                string stringTableDirectory = getOrCreateDir(parsedDirectory, stringTableTypeDef.strDataDir);

                int numFiles = 0;
                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                    BTree filesystemTree = new BTree(datReader);
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;
                        foreach (BTree.BTEntry entry in node.entries) {
                            if (surfacelocalTypeDef.contains(entry.gid)) {
                                if (typesToParseSet.Contains(DbType.RENDERSURFACE_LOCAL)) {
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        var surface = new RenderSurface(data);

                                        File.WriteAllBytes(Path.Combine(surfacelocalDirectory, $"{entry.gid.id:X8}{surfacelocalTypeDef.extension}"), surface.content);

                                        checkFullRead(data, entry);
                                    }
                                }
                            } else if (stringTypeDef.contains(entry.gid)) {
                                /*if (typesToParseSet.Contains(DbType.STRING)) {
                                    using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(stringDirectory, $"{entry.gid.id:X8}{stringTypeDef.extension}.txt"))))
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        //var emp = new EnumMapper(data);

                                        //foreach (var mapping in emp.idToString) {
                                        //    output.WriteLine($"{mapping.Key}\t{mapping.Value}");
                                        //}

                                        checkFullRead(data, entry);
                                    }
                                }*/
                            } else if (stringTableTypeDef.contains(entry.gid)) {
                                /*if (typesToParseSet.Contains(DbType.STRING_TABLE)) {
                                    using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(stringTableDirectory, $"{entry.gid.id:X8}{stringTableTypeDef.extension}.txt"))))
                                    using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                        //var emp = new EnumMapper(data);

                                        //foreach (var mapping in emp.idToString) {
                                        //    output.WriteLine($"{mapping.Key}\t{mapping.Value}");
                                        //}

                                        checkFullRead(data, entry);
                                    }
                                }*/
                            }
                        }
                    }
                }

                Log.Information($"Parsed local dat, num files: {numFiles}.");
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
