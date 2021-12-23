using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server;

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
        { IntStat.EtherealPhysicsTypeLow, new(SyncMode.VISUAL, persistent: true) },
        { IntStat.EtherealPhysicsTypeHigh, new(SyncMode.VISUAL, persistent: true) },
        { IntStat.EtherealPlacementTypeLow, new(SyncMode.VISUAL, persistent: true) },
        { IntStat.EtherealPlacementTypeHigh, new(SyncMode.VISUAL, persistent: true) },
        { IntStat.EtherealMovementTypeLow, new(SyncMode.VISUAL, persistent: true) },
        { IntStat.EtherealMovementTypeHigh, new(SyncMode.VISUAL, persistent: true) },
        { IntStat.Quantity, new(SyncMode.VISUAL, persistent: true) },
        { IntStat.Durability_CurrentLevel, new(SyncMode.VISUAL, persistent: true) },
    };

    private static readonly Dictionary<BoolStat, Config> BOOL = new() {
        { BoolStat.NoDraw, new(SyncMode.VISUAL, persistent: true) },
        { BoolStat.Dead, new(SyncMode.VISUAL, persistent: true) },
        { BoolStat.IsSelectable, new(SyncMode.VISUAL, persistent: true) },
        { BoolStat.Open, new(SyncMode.VISUAL, persistent: true) },
    };

    private static readonly Dictionary<FloatStat, Config> FLOAT = new() {
        { FloatStat.Physics_Scale, new(SyncMode.VISUAL, persistent: true) },
    };

    private static readonly Dictionary<TimestampStat, Config> TIMESTAMP = new() {

    };

    private static readonly Dictionary<DataIdStat, Config> DATA_ID = new() {

    };

    private static readonly Dictionary<InstanceIdStat, Config> INSTANCE_ID = new() {
        { InstanceIdStat.Container, new(SyncMode.VISUAL, persistent: true) },
        { InstanceIdStat.Equipper, new(SyncMode.VISUAL, persistent: true) },
        { InstanceIdStat.Plunderer, new(SyncMode.VISUAL, persistent: true) },
        { InstanceIdStat.Originator, new(SyncMode.VISUAL, persistent: true) },
        { InstanceIdStat.Claimant, new(SyncMode.VISUAL, persistent: true) },
        { InstanceIdStat.Allegiance_Monarch, new(SyncMode.VISUAL, persistent: true) },
    };

    private static readonly Dictionary<PositionStat, Config> POSITION = new() {

    };

    private static readonly Dictionary<StringInfoStat, Config> STRING_INFO = new() {
        { StringInfoStat.Name, new(SyncMode.VISUAL, persistent: true) },
        { StringInfoStat.PluralName, new(SyncMode.VISUAL, persistent: true) },
    };

    private static readonly Dictionary<LongIntStat, Config> LONG_INT = new() {
        { LongIntStat.TotalXP, new(SyncMode.PRIVATE, persistent: true) },
        { LongIntStat.AvailableXP, new(SyncMode.PRIVATE, persistent: true) },
        { LongIntStat.XPToRaiseVitae, new(SyncMode.PRIVATE, persistent: true) },
        { LongIntStat.TotalCraftXP, new(SyncMode.PRIVATE, persistent: true) },
        { LongIntStat.AvailableCraftXP, new(SyncMode.PRIVATE, persistent: true) },
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
