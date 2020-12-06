using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal class Landblock {

        // TODO: Make configurable
        public static readonly double TIMEOUT = 60 * 5;

        public readonly LandblockId id;
        public readonly HashSet<InstanceId> objectIds = new();

        private double timeoutTime;

        public Landblock(LandblockId id, double time) {
            this.id = id;
            refreshTimeout(time);
        }

        public void refreshTimeout(double time) {
            timeoutTime = time + TIMEOUT;
        }

        public bool isTimedOut(double time) {
            return time > timeoutTime;
        }
    }
}
