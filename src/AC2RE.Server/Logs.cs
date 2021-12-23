using AC2RE.Utils;
using Serilog;

namespace AC2RE.Server;

internal static class Logs {

    private static LoggerConfiguration getBaseLoggerConfig(string prefix) => new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: ALogger.getConsoleTemplate(prefix));

    public static readonly ALogger GENERAL = new(getBaseLoggerConfig("GEN"));

    public static readonly ALogger STATUS = new(getBaseLoggerConfig("STS"));

    public static readonly ALogger NET = new(getBaseLoggerConfig("NET"));

    public static readonly ALogger ACCOUNT = new(getBaseLoggerConfig("ACT"));
}
