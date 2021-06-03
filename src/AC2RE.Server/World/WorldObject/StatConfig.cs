using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal static class StatCfg {

        public enum SyncMode {
            NONE,
            PRIVATE,
            VISUAL,
        }

        private struct Config {

            public readonly SyncMode syncMode;
            public readonly bool persistent;

            public Config(SyncMode syncMode, bool persistent = false) {
                this.syncMode = syncMode;
                this.persistent = persistent;
            }
        }

        // TODO: Confirm these via packet logs, some are additional based on guesses
        private static readonly Dictionary<IntStat, Config> INT = new() {
            { IntStat.ETHEREALPHYSICSTYPELOW, new(SyncMode.VISUAL, persistent: true) },
            { IntStat.ETHEREALPHYSICSTYPEHIGH, new(SyncMode.VISUAL, persistent: true) },
            { IntStat.ETHEREALPLACEMENTTYPELOW, new(SyncMode.VISUAL, persistent: true) },
            { IntStat.ETHEREALPLACEMENTTYPEHIGH, new(SyncMode.VISUAL, persistent: true) },
            { IntStat.ETHEREALMOVEMENTTYPELOW, new(SyncMode.VISUAL, persistent: true) },
            { IntStat.ETHEREALMOVEMENTTYPEHIGH, new(SyncMode.VISUAL, persistent: true) },
            { IntStat.QUANTITY, new(SyncMode.VISUAL, persistent: true) },
            { IntStat.DURABILITY_CURRENTLEVEL, new(SyncMode.VISUAL, persistent: true) },
        };

        private static readonly Dictionary<BoolStat, Config> BOOL = new() {
            { BoolStat.NODRAW, new(SyncMode.VISUAL, persistent: true) },
            { BoolStat.DEAD, new(SyncMode.VISUAL, persistent: true) },
            { BoolStat.ISSELECTABLE, new(SyncMode.VISUAL, persistent: true) },
            { BoolStat.OPEN, new(SyncMode.VISUAL, persistent: true) },
        };

        private static readonly Dictionary<FloatStat, Config> FLOAT = new() {
            { FloatStat.PHYSICS_SCALE, new(SyncMode.VISUAL, persistent: true) },
        };

        private static readonly Dictionary<TimestampStat, Config> TIMESTAMP = new() {

        };

        private static readonly Dictionary<DataIdStat, Config> DATA_ID = new() {

        };

        private static readonly Dictionary<InstanceIdStat, Config> INSTANCE_ID = new() {
            { InstanceIdStat.CONTAINER, new(SyncMode.VISUAL, persistent: true) },
            { InstanceIdStat.EQUIPPER, new(SyncMode.VISUAL, persistent: true) },
            { InstanceIdStat.PLUNDERER, new(SyncMode.VISUAL, persistent: true) },
            { InstanceIdStat.ORIGINATOR, new(SyncMode.VISUAL, persistent: true) },
            { InstanceIdStat.CLAIMANT, new(SyncMode.VISUAL, persistent: true) },
            { InstanceIdStat.ALLEGIANCE_MONARCH, new(SyncMode.VISUAL, persistent: true) },
        };

        private static readonly Dictionary<PositionStat, Config> POSITION = new() {

        };

        private static readonly Dictionary<StringInfoStat, Config> STRING_INFO = new() {
            { StringInfoStat.NAME, new(SyncMode.VISUAL, persistent: true) },
            { StringInfoStat.PLURALNAME, new(SyncMode.VISUAL, persistent: true) },
        };

        private static readonly Dictionary<LongIntStat, Config> LONG_INT = new() {
            { LongIntStat.TOTALXP, new(SyncMode.PRIVATE, persistent: true) },
            { LongIntStat.AVAILABLEXP, new(SyncMode.PRIVATE, persistent: true) },
            { LongIntStat.XPTORAISEVITAE, new(SyncMode.PRIVATE, persistent: true) },
            { LongIntStat.TOTALCRAFTXP, new(SyncMode.PRIVATE, persistent: true) },
            { LongIntStat.AVAILABLECRAFTXP, new(SyncMode.PRIVATE, persistent: true) },
        };

        public static SyncMode getSyncMode(IntStat stat) => INT.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(IntStat stat) => INT.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(BoolStat stat) => BOOL.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(BoolStat stat) => BOOL.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(FloatStat stat) => FLOAT.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(FloatStat stat) => FLOAT.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(TimestampStat stat) => TIMESTAMP.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(TimestampStat stat) => TIMESTAMP.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(DataIdStat stat) => DATA_ID.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(DataIdStat stat) => DATA_ID.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(InstanceIdStat stat) => INSTANCE_ID.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(InstanceIdStat stat) => INSTANCE_ID.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(PositionStat stat) => POSITION.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(PositionStat stat) => POSITION.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(StringInfoStat stat) => STRING_INFO.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(StringInfoStat stat) => STRING_INFO.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
        public static SyncMode getSyncMode(LongIntStat stat) => LONG_INT.TryGetValue(stat, out Config statConfig) ? statConfig.syncMode : SyncMode.NONE;
        public static bool isPersistent(LongIntStat stat) => LONG_INT.TryGetValue(stat, out Config statConfig) ? statConfig.persistent : false;
    }
}
