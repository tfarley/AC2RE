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

            //parsePortalDat("G:\\Asheron's Call 2\\portal.dat", "vd");

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

        private static void parsePortalDat(string portalDatFileName, params string[] filesToParse) {
            HashSet<string> filesToParseSet = new HashSet<string>(filesToParse);
            string parsedDirectory = "portalparsed";
            string empDirectory = Path.Combine(parsedDirectory, "emp");
            string mprDirectory = Path.Combine(parsedDirectory, "mpr");
            string vdDirectory = Path.Combine(parsedDirectory, "vd");
            string wlibDirectory = Path.Combine(parsedDirectory, "wlib");
            if (File.Exists(portalDatFileName)) {
                Directory.CreateDirectory(empDirectory);
                Directory.CreateDirectory(mprDirectory);
                Directory.CreateDirectory(vdDirectory);
                Directory.CreateDirectory(wlibDirectory);

                int numFiles = 0;
                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(portalDatFileName)))) {
                    BTree filesystemTree = new BTree(datReader);
                    // Parse data in first pass that is required for second pass
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        foreach (BTree.BTEntry entry in node.entries) {
                            if ((filesToParseSet.Contains("mpr") || filesToParseSet.Contains("vd")) && (entry.gid >> 24) == 0x34) {
                                using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                    MasterProperty.instance = new MasterProperty(data);

                                    if (filesToParseSet.Contains("mpr")) {
                                        File.WriteAllText(Path.Combine(mprDirectory, $"{entry.gid:X8}.mpr.txt"), Util.objectToString(MasterProperty.instance));
                                    }

                                    if (data.BaseStream.Position < data.BaseStream.Length) {
                                        Log.Warning($"mpr file {entry.gid:X8} was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                                    }
                                }
                            }
                        }
                    }
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;
                        foreach (BTree.BTEntry entry in node.entries) {
                            if (filesToParseSet.Contains("emp") && (entry.gid >> 24) == 0x23) {
                                using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(empDirectory, $"{entry.gid:X8}.emp.txt"))))
                                using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                    var emp = new EnumMapper(data);
                                    foreach (var mapping in emp.idToString) {
                                        output.WriteLine($"{mapping.Key}\t{mapping.Value}");
                                    }

                                    if (data.BaseStream.Position < data.BaseStream.Length) {
                                        Log.Warning($"emp file {entry.gid:X8} was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                                    }
                                }
                            }
                            if (filesToParseSet.Contains("wlib") && (entry.gid >> 24) == 0x56) {
                                using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                    var wlib = new WLib(data);
                                    using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(wlibDirectory, $"{entry.gid:X8}.wlib.packages.txt")))) {
                                        Dump.dumpPackages(output, wlib.byteStream);
                                    }
                                    var disasm = new Disasm(wlib.byteStream);
                                    using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(wlibDirectory, $"{entry.gid:X8}.wlib.disasm.txt")))) {
                                        disasm.write(output);
                                    }

                                    if (data.BaseStream.Position < data.BaseStream.Length) {
                                        Log.Warning($"wlib file {entry.gid:X8} was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                                    }
                                }
                            }
                            if (filesToParseSet.Contains("vd") && (entry.gid >> 24) == 0x1F) {
                                using (AC2Reader data = datReader.getFileReader(entry.offset, entry.size)) {
                                    var vDesc = new VisualDesc(data);

                                    File.WriteAllText(Path.Combine(vdDirectory, $"{entry.gid:X8}.vd.txt"), Util.objectToString(vDesc));

                                    if (data.BaseStream.Position < data.BaseStream.Length) {
                                        Log.Warning($"vdesc file {entry.gid:X8} was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                                    }
                                }
                            }
                        }
                    }
                }

                Log.Information($"Parsed portal dat, num files: {numFiles}.");
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
