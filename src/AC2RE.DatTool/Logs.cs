using AC2RE.Utils;
using Serilog;

namespace AC2RE.DatTool {

    internal static class Logs {

        private static LoggerConfiguration getBaseLoggerConfig(string prefix) => new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: CLogger.getConsoleTemplate(prefix));

        public static readonly CLogger GENERAL = new CLogger(getBaseLoggerConfig("GEN"));
    }
}
