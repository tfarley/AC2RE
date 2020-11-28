using AC2RE.Utils;
using Serilog;

namespace AC2RE.DatTool {

    internal static class Logs {

        private static LoggerConfiguration getBaseLoggerConfig(string prefix) => new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: ALogger.getConsoleTemplate(prefix));

        public static readonly ALogger GENERAL = new ALogger(getBaseLoggerConfig("GEN"));
    }
}
