using AC2E.Def;
using AC2E.Tools;
using Serilog;
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

            /*
            using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\portal.dat")) {
                DatParse.parseDat(datReader, "portalparsed", DbType.MESH);
            }
            */

            /*
            using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\cell_1.dat")) {
                //DatParse.parseDat(datReader, "cell1parsed", DbType.ENVCELL);
                CellParse.getMissingCells(datReader);
            }
            */

            /*
            using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\cell_2.dat")) {
                DatParse.parseDat(datReader, "cell2parsed", DbType.DATFILEDATA);
            }
            */

            /*
            using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\highres.dat")) {
                DatParse.parseDat(datReader, "highresparsed", DbType.DATFILEDATA);
            }
            */

            /*
            using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\local_English.dat")) {
                DatParse.parseDat(datReader, "localenglishparsed", DbType.ENCODED_WAV);
            }
            */

            /*
            using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\local_English.dat")) {
                DatParse.parseDat(datReader, "localenglishparsed", DbType.ENCODED_WAV);
            }
            */

            runServer();
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
