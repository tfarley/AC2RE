using AC2RE.Utils;
using Serilog;

namespace AC2RE.Server {

    internal static class Logs {

        private static LoggerConfiguration getBaseLoggerConfig(string prefix) => new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: ALogger.getConsoleTemplate(prefix));

        public static readonly ALogger GENERAL = new ALogger(getBaseLoggerConfig("GEN"));

        public static readonly ALogger STATUS = new ALogger(getBaseLoggerConfig("STS"));

        public static readonly ALogger NET = new ALogger(getBaseLoggerConfig("NET"));

        public static readonly ALogger ACCOUNT = new ALogger(getBaseLoggerConfig("ACT"));
    }
}
