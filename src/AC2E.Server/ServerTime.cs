using Serilog;
using System;
using System.Diagnostics;

namespace AC2E.Server {

    internal class ServerTime {

        public float startTime { get; private set; }
        public float time { get; private set; }
        public float elapsedTime { get; private set; }
        private Stopwatch stopwatch = new Stopwatch();

        public void restart() {
            if (!Stopwatch.IsHighResolution) {
                Log.Warning($"High resolution stopwatch is not available.");
            }

            startTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond;
            stopwatch.Restart();
        }

        public void tick() {
            elapsedTime = (float)stopwatch.Elapsed.TotalSeconds;
            time = startTime + elapsedTime;
        }
    }
}
