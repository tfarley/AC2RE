using AC2E.Def;
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

            DatParse.parseDat("G:\\Asheron's Call 2\\portal.dat", "portalparsed", DbType.PROPERTY_DESC);
            //DatParse.parseDat("G:\\Asheron's Call 2\\cell_1.dat", "cell1parsed", DbType.ENVCELL);
            //DatParse.parseDat("G:\\Asheron's Call 2\\local_English.dat", "localparsed", DbType.RENDERSURFACE_LOCAL, DbType.STRING, DbType.STRING_TABLE);

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
