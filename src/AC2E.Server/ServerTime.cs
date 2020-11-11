using Serilog;
using System;
using System.Diagnostics;

namespace AC2E.Server {

    internal class ServerTime {

        public readonly double tickDeltaTime;
        public readonly double maxDeltaTime;

        public double startTime { get; private set; }
        public double time { get; private set; }
        public float elapsedTime { get; private set; }

        private readonly Stopwatch stopwatch = new();

        private uint tick;
        private double frameAccum;
        private double frameTime;
        private double lastFrameTime;

        public ServerTime(double tickDeltaTime, double maxDeltaTime) {
            this.tickDeltaTime = tickDeltaTime;
            this.maxDeltaTime = maxDeltaTime;
        }

        public void restart() {
            if (!Stopwatch.IsHighResolution) {
                Log.Warning($"High resolution stopwatch is not available.");
            }

            startTime = (double)(DateTime.UtcNow - new DateTime(2020, 1, 1)).Ticks / TimeSpan.TicksPerSecond;
            time = 0.0;
            elapsedTime = 0.0f;

            stopwatch.Restart();

            tick = 0;
            frameAccum = 0.0;
            frameTime = 0.0;
            lastFrameTime = 0.0;
        }

        internal void beginFrame() {
            frameTime = stopwatch.Elapsed.TotalSeconds;
            frameAccum += frameTime - lastFrameTime;
            lastFrameTime = frameTime;
        }

        internal bool tryTick() {
            if (stopwatch.Elapsed.TotalSeconds - frameTime > maxDeltaTime) {
                // Ran overtime - reset accumulator to try and catch up, and stop ticking which effectively slows down the simulation
                frameAccum = 0.0f;
                return false;
            }

            if (frameAccum < tickDeltaTime) {
                // Not enough time remaining in accumulator for a full tick, so stop ticking
                return false;
            }

            frameAccum -= tickDeltaTime;

            tick++;
            double elapsedTimeDouble = tick * tickDeltaTime;
            elapsedTime = (float)elapsedTimeDouble;
            time = startTime + elapsedTimeDouble;

            return true;
        }
    }
}
