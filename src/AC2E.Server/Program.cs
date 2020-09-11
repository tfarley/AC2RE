using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class Program {

        private static readonly int SERVER_LOGON_PORT = 7777;
        private static readonly AC2Server SERVER = new AC2Server();

        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Hello World!");

            runServer();
        }

        private static void runServer() {
            SERVER.start(SERVER_LOGON_PORT);

            Task.Run(() => {
                while (true) {
                    switch (Console.ReadKey(true).Key) {
                        case ConsoleKey.Enter:
                            SERVER.start(SERVER_LOGON_PORT);
                            break;
                        case ConsoleKey.Escape:
                            SERVER.stop();
                            break;
                    }
                }
            });

            while (true) {
                SERVER.tick();
                Thread.Sleep(10);
            }
        }
    }
}
