using System;

namespace AC2RE.Utils {

    public static class RandomExtensions {

        public static uint NextUint(this Random random) {
            return (uint)random.Next();
        }

        public static long NextLong(this Random random) {
            long randomHigh = random.Next();
            long randomLow = random.Next();
            // Shift up high int and mask off low int (since during casting it will sign extend, which messes up the OR)
            return (randomHigh << 32) | (randomLow & 0x00000000FFFFFFFFL);
        }

        public static ulong NextUlong(this Random random) {
            return (ulong)random.NextLong();
        }
    }
}
