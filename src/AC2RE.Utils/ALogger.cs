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
#if DEBUG
            levelSwitch = new(LogEventLevel.Debug);
#else
            levelSwitch = new();
#endif

            logger = loggerConfig
                .MinimumLevel.ControlledBy(levelSwitch)
                .CreateLogger();
        }

        public ALogger(LoggerConfiguration loggerConfig, LogEventLevel initialMinimumLevel) {
            levelSwitch = new(initialMinimumLevel);

            logger = loggerConfig
                .MinimumLevel.ControlledBy(levelSwitch)
                .CreateLogger();
        }

        [Conditional("DEBUG")]
        public void trace(string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Verbose(message);
        }

        [Conditional("DEBUG")]
        public void debug(string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Debug(message);
        }

        public void info(string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Information(message);
        }

        public void warn(string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Warning(message);
        }

        public void warn(Exception e, string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Warning(e, message);
        }

        public void error(string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Error(message);
        }

        public void error(Exception e, string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Error(e, message);
        }

        public void fatal(string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Fatal(message);
        }

        public void fatal(Exception e, string message, params object[] ctxValues) {
            addCtx(logger, ctxValues).Fatal(e, message);
        }

        private static ILogger addCtx(ILogger logger, params object[] ctxValues) {
            for (int i = 0; i < ctxValues.Length; i += 2) {
                logger = logger.ForContext(ctxValues[i].ToString(), ctxValues[i + 1]);
            }

            return logger;
        }
    }
}
