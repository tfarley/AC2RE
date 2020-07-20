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

            //parseWLib();

            //parseDatFiles();

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

        private static void parseWLib() {
            string wlibFileName = "56000005";
            if (File.Exists(wlibFileName)) {
                using (AC2Reader data = new AC2Reader(File.OpenRead(wlibFileName))) {
                    var wlib = new WLib(data);
                    Log.Information("Parsed wlib.");
                    using (StreamWriter output = new StreamWriter(File.OpenWrite("wlib.packages.txt"))) {
                        Dump.dumpPackages(output, wlib.byteStream);
                    }
                    var disasm = new Disasm(wlib.byteStream);
                    Log.Information("Disassembled bytestream.");
                    using (StreamWriter output = new StreamWriter(File.OpenWrite("wlib.disasm.txt"))) {
                        disasm.write(output);
                    }
                }
            }
        }

        private static void parseDatFiles() {
            string datFilesDirectory = "DatFiles";
            string parsedExtension = ".txt";
            if (Directory.Exists(datFilesDirectory)) {
                foreach (string datFileName in Directory.EnumerateFiles(datFilesDirectory)) {
                    if (!datFileName.EndsWith(parsedExtension) && !File.Exists(datFileName + parsedExtension)) {
                        using (AC2Reader data = new AC2Reader(File.OpenRead(datFileName)))
                        using (StreamWriter output = new StreamWriter(File.OpenWrite(datFileName + parsedExtension))) {
                            var emp = new EnumMapper(data);
                            foreach (var mapping in emp.idToString) {
                                output.WriteLine($"{mapping.Key}\t{mapping.Value}");
                            }
                        }
                    }
                }
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
