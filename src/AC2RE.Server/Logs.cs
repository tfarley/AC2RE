using AC2RE.Utils;
using Serilog;

namespace AC2RE.Server {

    internal static class Logs {

        private static LoggerConfiguration getBaseLoggerConfig(string prefix) => new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: CLogger.getConsoleTemplate(prefix));

        public static readonly CLogger GENERAL = new CLogger(getBaseLoggerConfig("GEN"));

        public static readonly CLogger STATUS = new CLogger(getBaseLoggerConfig("STS"));

        public static readonly CLogger NET = new CLogger(getBaseLoggerConfig("NET"));

        public static readonly CLogger ACCOUNT = new CLogger(getBaseLoggerConfig("ACT"));
    }
}
