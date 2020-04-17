using System;
using System.IO;
using System.Text;
using System.Threading;

class Program {

    private static readonly AC2Server server = new AC2Server();

    static void Main(string[] args) {
        ALog.setCategoryEnabled(CatTransport.i, true);
        ALog.setCategoryEnabled(CatProto.i, true);

        ALog.info("Hello World!");

        //parseWLib();

        //parseDatFiles();

        runServer();
    }

    private static void printDecryptedString(byte[] bytes, Encoding encoding) {
        CryptoUtil.decrypt(bytes, 0, bytes.Length);
        ALog.info($"Str: {encoding.GetString(bytes)}");
    }

    private static void printEncryptedString(string str, Encoding encoding) {
        byte[] bytes = encoding.GetBytes(str);
        CryptoUtil.encrypt(bytes, 0, bytes.Length);
        ALog.info($"Str: {str}");
        ALog.info($"Enc: {BitConverter.ToString(bytes).Replace("-", "")}");
    }

    private static void parseWLib() {
        string wlibFileName = "56000005";
        if (File.Exists(wlibFileName)) {
            using (BinaryReader data = new BinaryReader(File.OpenRead(wlibFileName))) {
                var wlib = new WLib(data);
                ALog.info("Parsed wlib.");
            }
        }
    }

    private static void parseDatFiles() {
        string datFilesDirectory = "DatFiles";
        string parsedExtension = ".txt";
        if (Directory.Exists(datFilesDirectory)) {
            foreach (string datFileName in Directory.EnumerateFiles(datFilesDirectory)) {
                if (!datFileName.EndsWith(parsedExtension) && !File.Exists(datFileName + parsedExtension)) {
                    using (BinaryReader data = new BinaryReader(File.OpenRead(datFileName)))
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
        server.start(7777);

        while (true) {
            server.processReceive();
            Thread.Sleep(100);
        }
    }
}
