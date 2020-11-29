using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Diagnostics;

namespace AC2RE.Utils {

    public class ALogger {

        public static string getConsoleTemplate(string prefix) => "[{Timestamp:HH:mm:ss} {Level:u3}] " + prefix + " {Message:lj} {Properties}{NewLine}{Exception}";

        public LogEventLevel level {
            get => levelSwitch.MinimumLevel;
            set => levelSwitch.MinimumLevel = value;
        }

        private readonly ILogger logger;
        private readonly LoggingLevelSwitch levelSwitch;

        public ALogger(LoggerConfiguration loggerConfig) {
            levelSwitch = new(LogEventLevel.Debug);
            logger = loggerConfig
                .MinimumLevel.ControlledBy(levelSwitch)
                .CreateLogger();
        }

        private ALogger(ILogger logger, LoggingLevelSwitch levelSwitch) {
            this.logger = logger;
            this.levelSwitch = levelSwitch;
        }

        public ALogger forContext(params object[] contextKeyValues) {
            return new ALogger(addContext(logger, contextKeyValues), levelSwitch);
        }

        [Conditional("DEBUG")]
        public void trace(string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Verbose(message);
        }

        [Conditional("DEBUG")]
        public void debug(string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Debug(message);
        }

        public void info(string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Information(message);
        }

        public void warn(string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Warning(message);
        }

        public void warn(Exception e, string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Warning(e, message);
        }

        public void error(string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Error(message);
        }

        public void error(Exception e, string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Error(e, message);
        }

        public void fatal(string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Fatal(message);
        }

        public void fatal(Exception e, string message, params object[] contextKeyValues) {
            addContext(logger, contextKeyValues).Fatal(e, message);
        }

        private static ILogger addContext(ILogger logger, params object[] contextKeyValues) {
            for (int i = 0; i < contextKeyValues.Length; i += 2) {
                logger = logger.ForContext(contextKeyValues[i].ToString(), contextKeyValues[i + 1]);
            }

            return logger;
        }
    }
}
