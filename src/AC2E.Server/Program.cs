using AC2E.Def;
using AC2E.Utils;
using Serilog;
using System;
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

            parsePortalDat("G:\\Asheron's Call 2\\portal.dat", false, false);

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

        private static void parsePortalDat(string portalDatFileName, bool parseEnumMappers, bool parseWLib) {
            string parsedDirectory = "portalparsed";
            string empDirectory = Path.Combine(parsedDirectory, "emp");
            string wlibDirectory = Path.Combine(parsedDirectory, "wlib");
            if (File.Exists(portalDatFileName)) {
                Directory.CreateDirectory(empDirectory);
                Directory.CreateDirectory(wlibDirectory);

                int numFiles = 0;
                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(portalDatFileName)))) {
                    BTree filesystemTree = new BTree(datReader);
                    foreach (BTree.BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;
                        foreach (BTree.BTEntry entry in node.entries) {
                            if (parseEnumMappers && (entry.gid >> 24) == 0x23) {
                                using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(empDirectory, $"{entry.gid:X8}.emp.txt"))))
                                using (AC2Reader data = new AC2Reader(new MemoryStream(datReader.readFileBytes(entry.offset, entry.size)))) {
                                    var emp = new EnumMapper(data);
                                    foreach (var mapping in emp.idToString) {
                                        output.WriteLine($"{mapping.Key}\t{mapping.Value}");
                                    }
                                }
                            }
                            if (parseWLib && (entry.gid >> 24) == 0x56) {
                                using (AC2Reader data = new AC2Reader(new MemoryStream(datReader.readFileBytes(entry.offset, entry.size)))) {
                                    var wlib = new WLib(data);
                                    using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(wlibDirectory, $"{entry.gid:X8}.wlib.packages.txt")))) {
                                        Dump.dumpPackages(output, wlib.byteStream);
                                    }
                                    var disasm = new Disasm(wlib.byteStream);
                                    using (StreamWriter output = new StreamWriter(File.OpenWrite(Path.Combine(wlibDirectory, $"{entry.gid:X8}.wlib.disasm.txt")))) {
                                        disasm.write(output);
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
