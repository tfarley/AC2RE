using System;

namespace AC2RE.Utils {

    public static class RandomExtensions {

        public static uint NextUInt(this Random random) {
            return (uint)random.Next(int.MinValue, int.MaxValue);
        }

        public static ulong NextUInt64(this Random random) {
            return (ulong)random.NextInt64(long.MinValue, long.MaxValue);
        }
    }
}
