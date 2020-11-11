using Serilog;
using System;
using System.Threading;

namespace AC2E.Server {

    internal class Program {

        private static readonly int SERVER_LOGON_PORT = 7777;
        private static readonly AC2Server SERVER = new();

        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Hello World!");

            runServer();
        }

        private static void runServer() {
            AppDomain.CurrentDomain.ProcessExit += (_, _) => SERVER.stop();

            SERVER.start(SERVER_LOGON_PORT);

            while (true) {
                if (Console.KeyAvailable) {
                    switch (Console.ReadKey(true).Key) {
                        case ConsoleKey.Enter:
                            SERVER.start(SERVER_LOGON_PORT);
                            break;
                        case ConsoleKey.Escape:
                            SERVER.stop();
                            break;
                    }
                }

                SERVER.tick();
                Thread.Sleep(10);
            }
        }
    }
}
