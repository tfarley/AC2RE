using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal partial class WorldObject {

        [DbPersist]
        public CBaseQualities qualities;

        private HashSet<object> dirtyStats = new();

        private void initQualities() {
            qualities = new();
            qualities.weenieDesc = new();
        }

        // TODO: Confirm these via packet logs, some are additional based on guesses
        private static readonly HashSet<IntStat> INT_VISUAL_STATS = new() {
            IntStat.ETHEREALPHYSICSTYPELOW,
            IntStat.ETHEREALPHYSICSTYPEHIGH,
            IntStat.ETHEREALPLACEMENTTYPELOW,
            IntStat.ETHEREALPLACEMENTTYPEHIGH,
            IntStat.ETHEREALMOVEMENTTYPELOW,
            IntStat.ETHEREALMOVEMENTTYPEHIGH,
            IntStat.QUANTITY,
            IntStat.DURABILITY_CURRENTLEVEL,
        };

        private static readonly HashSet<BoolStat> BOOL_VISUAL_STATS = new() {
            BoolStat.NODRAW,
            BoolStat.DEAD,
            BoolStat.ISSELECTABLE,
            BoolStat.OPEN,
        };

        private static readonly HashSet<FloatStat> FLOAT_VISUAL_STATS = new() {
            FloatStat.PHYSICS_SCALE,
        };

        private static readonly HashSet<TimestampStat> TIMESTAMP_VISUAL_STATS = new() {

        };

        private static readonly HashSet<DataIdStat> DATA_ID_VISUAL_STATS = new() {

        };

        private static readonly HashSet<InstanceIdStat> INSTANCE_ID_VISUAL_STATS = new() {
            InstanceIdStat.CONTAINER,
            InstanceIdStat.EQUIPPER,
            InstanceIdStat.PLUNDERER,
            InstanceIdStat.ORIGINATOR,
            InstanceIdStat.CLAIMANT,
            InstanceIdStat.ALLEGIANCE_MONARCH,
        };

        private static readonly HashSet<PositionStat> POSITION_VISUAL_STATS = new() {

        };

        private static readonly HashSet<StringInfoStat> STRING_INFO_VISUAL_STATS = new() {
            StringInfoStat.NAME,
            StringInfoStat.PLURALNAME,
        };

        private static readonly HashSet<LongIntStat> LONG_INT_VISUAL_STATS = new() {

        };

        public StringInfo? name {
            get => getQ(StringInfoStat.NAME);
            set {
                setQ(StringInfoStat.NAME, value);
                qualities.weenieDesc.name = value;
            }
        }

        public StringInfo? pluralName {
            get => getQ(StringInfoStat.PLURALNAME);
            set {
                setQ(StringInfoStat.PLURALNAME, value);
                qualities.weenieDesc.pluralName = value;
            }
        }

        public DataId iconDid {
            get => getQ(DataIdStat.PHYSOBJ);
            set {
                setQ(DataIdStat.ICONID, value);
                qualities.weenieDesc.iconDid = value;
            }
        }

        public InstanceId containerId {
            get => getQ(InstanceIdStat.CONTAINER);
            set {
                setQ(InstanceIdStat.CONTAINER, value);
                qualities.weenieDesc.containerId = value;
            }
        }

        public InstanceId wielderId {
            get => getQ(InstanceIdStat.EQUIPPER);
            set {
                setQ(InstanceIdStat.EQUIPPER, value);
                qualities.weenieDesc.wielderId = value;
            }
        }

        public InstanceId monarchId {
            get => getQ(InstanceIdStat.ALLEGIANCE_MONARCH);
            set {
                setQ(InstanceIdStat.ALLEGIANCE_MONARCH, value);
                qualities.weenieDesc.monarchId = value;
            }
        }

        public InstanceId originatorId {
            get => getQ(InstanceIdStat.ORIGINATOR);
            set {
                setQ(InstanceIdStat.ORIGINATOR, value);
                qualities.weenieDesc.originatorId = value;
            }
        }

        public InstanceId claimantId {
            get => getQ(InstanceIdStat.CLAIMANT);
            set {
                setQ(InstanceIdStat.CLAIMANT, value);
                qualities.weenieDesc.claimantId = value;
            }
        }

        public InstanceId killerId {
            get => getQ(InstanceIdStat.PLUNDERER);
            set {
                setQ(InstanceIdStat.PLUNDERER, value);
                qualities.weenieDesc.killerId = value;
            }
        }

        public InstanceId summonerId {
            get => getQ(InstanceIdStat.AI_PETSUMMONER);
            set {
                setQ(InstanceIdStat.AI_PETSUMMONER, value);
                qualities.weenieDesc.petSummonerId = value;
            }
        }

        public int quantity {
            get => getQ(IntStat.QUANTITY);
            set {
                setQ(IntStat.QUANTITY, value);
                qualities.weenieDesc.quantity = value;
            }
        }

        public int value {
            get => getQ(IntStat.VALUE);
            set {
                setQ(IntStat.VALUE, value);
                qualities.weenieDesc.value = value;
            }
        }

        public FactionType faction {
            get => (FactionType)getQ(IntStat.FACTION_MEMBERSHIP);
            set {
                setQ(IntStat.FACTION_MEMBERSHIP, (int)value);
                qualities.weenieDesc.factionType = value;
            }
        }

        public int pkAlwaysTruePermissions {
            get => getQ(IntStat.PK_ALWAYSTRUEPERMISSIONS);
            set {
                setQ(IntStat.PK_ALWAYSTRUEPERMISSIONS, value);
                qualities.weenieDesc.pkAlwaysTruePermissions = value;
            }
        }

        public int pkAlwaysFalsePermissions {
            get => getQ(IntStat.PK_ALWAYSFALSEPERMISSIONS);
            set {
                setQ(IntStat.PK_ALWAYSFALSEPERMISSIONS, value);
                qualities.weenieDesc.pkAlwaysFalsePermissions = value;
            }
        }

        public int physicsTypeLow {
            get => getQ(IntStat.ETHEREALPHYSICSTYPELOW);
            set {
                setQ(IntStat.ETHEREALPHYSICSTYPELOW, value);
                qualities.weenieDesc.physicsTypeLow = value;
            }
        }

        public int physicsTypeHigh {
            get => getQ(IntStat.ETHEREALPHYSICSTYPEHIGH);
            set {
                setQ(IntStat.ETHEREALPHYSICSTYPEHIGH, value);
                qualities.weenieDesc.physicsTypeHigh = value;
            }
        }

        public int movementEtherealLow {
            get => getQ(IntStat.ETHEREALMOVEMENTTYPELOW);
            set {
                setQ(IntStat.ETHEREALMOVEMENTTYPELOW, value);
                qualities.weenieDesc.movementEtherealLow = value;
            }
        }

        public int movementEtherealHigh {
            get => getQ(IntStat.ETHEREALMOVEMENTTYPEHIGH);
            set {
                setQ(IntStat.ETHEREALMOVEMENTTYPEHIGH, value);
                qualities.weenieDesc.movementEtherealHigh = value;
            }
        }

        public int placementEtherealLow {
            get => getQ(IntStat.ETHEREALPLACEMENTTYPELOW);
            set {
                setQ(IntStat.ETHEREALPLACEMENTTYPELOW, value);
                qualities.weenieDesc.placementEtherealLow = value;
            }
        }

        public int placementEtherealHigh {
            get => getQ(IntStat.ETHEREALPLACEMENTTYPEHIGH);
            set {
                setQ(IntStat.ETHEREALPLACEMENTTYPEHIGH, value);
                qualities.weenieDesc.placementEtherealHigh = value;
            }
        }

        public int durability {
            get => getQ(IntStat.DURABILITY_CURRENTLEVEL);
            set {
                setQ(IntStat.DURABILITY_CURRENTLEVEL, value);
                qualities.weenieDesc.durabilityCurrentLevel = value;
            }
        }

        public int durabilityMax {
            get => getQ(IntStat.DURABILITY_MAXLEVEL);
            set {
                setQ(IntStat.DURABILITY_MAXLEVEL, value);
                qualities.weenieDesc.durabilityMaxLevel = value;
            }
        }

        public float scale {
            get => getQ(FloatStat.PHYSICS_SCALE);
            set {
                setQ(FloatStat.PHYSICS_SCALE, value);
                qualities.weenieDesc.scale = value;
            }
        }

        public bool open {
            get => getQ(BoolStat.OPEN);
            set {
                setQ(BoolStat.OPEN, value);
                if (value) {
                    qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.OPEN;
                } else {
                    qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.OPEN;
                }
            }
        }

        public bool dead {
            get => getQ(BoolStat.DEAD);
            set {
                setQ(BoolStat.DEAD, value);
                if (value) {
                    qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.DEAD;
                } else {
                    qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.DEAD;
                }
            }
        }

        public bool selectable {
            get => getQ(BoolStat.ISSELECTABLE);
            set {
                setQ(BoolStat.ISSELECTABLE, value);
                if (value) {
                    qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.SELECTABLE;
                } else {
                    qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.SELECTABLE;
                }
            }
        }

        public bool noDraw {
            get => getQ(BoolStat.NODRAW);
            set {
                setQ(BoolStat.NODRAW, value);
                if (value) {
                    qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.NO_DRAW;
                } else {
                    qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.NO_DRAW;
                }
            }
        }

        public SpeciesType species {
            get => (SpeciesType)getQ(IntStat.SPECIES);
            set => setQ(IntStat.SPECIES, (int)value);
        }

        public SexType sex {
            get => (SexType)getQ(IntStat.SEX);
            set => setQ(IntStat.SEX, (int)value);
        }

        public InvLoc preferredInvLoc {
            get => (InvLoc)getQ(IntStat.PREFERREDINVENTORYLOCATION);
            set => setQ(IntStat.PREFERREDINVENTORYLOCATION, (int)value);
        }

        public InvLoc validInvLocs {
            get => (InvLoc)getQ(IntStat.VALIDINVENTORYLOCATIONS);
            set => qualities.ints[IntStat.VALIDINVENTORYLOCATIONS] = (int)value;
        }

        public HoldingLocation primaryHoldLoc {
            get => (HoldingLocation)getQ(IntStat.INV_PRIMARYPARENTINGLOCATION);
            set => qualities.ints[IntStat.INV_PRIMARYPARENTINGLOCATION] = (int)value;
        }

        public Orientation holdOrientation {
            get => (Orientation)getQ(IntStat.PLACEMENTPOSITION);
            set => setQ(IntStat.PLACEMENTPOSITION, (int)value);
        }

        public DataId entityDid {
            get => qualities.weenieDesc.entityDid;
            set => qualities.weenieDesc.entityDid = value;
        }

        public PackageType packageType {
            get => qualities.weenieDesc.packageType;
            set => qualities.weenieDesc.packageType = value;
        }

        public DataId physObjDid {
            get => getQ(DataIdStat.PHYSOBJ);
            set => setQ(DataIdStat.PHYSOBJ, value);
        }

        public int health {
            get => getQ(IntStat.HEALTH_CURRENTLEVEL);
            set => setQ(IntStat.HEALTH_CURRENTLEVEL, value);
        }

        public int healthMax {
            get => getQ(IntStat.HEALTH_CACHEDMAX);
            set => setQ(IntStat.HEALTH_CACHEDMAX, value);
        }

        public float healthRegen {
            get => getQ(FloatStat.HEALTH_REGENRATE);
            set => setQ(FloatStat.HEALTH_REGENRATE, value);
        }

        public int vigor {
            get => getQ(IntStat.VIGOR_CURRENTLEVEL);
            set => setQ(IntStat.VIGOR_CURRENTLEVEL, value);
        }

        public int vigorMax {
            get => getQ(IntStat.VIGOR_CACHEDMAX);
            set => setQ(IntStat.VIGOR_CACHEDMAX, value);
        }

        public float vigorRegen {
            get => getQ(FloatStat.VIGOR_REGENRATE);
            set => setQ(FloatStat.VIGOR_REGENRATE, value);
        }

        public int capacity {
            get => getQ(IntStat.CONTAINERMAXCAPACITY);
            set => setQ(IntStat.CONTAINERMAXCAPACITY, value);
        }

        public int playerClass {
            get => getQ(IntStat.CLASS);
            set => setQ(IntStat.CLASS, value);
        }

        public int level {
            get => getQ(IntStat.LEVEL);
            set => setQ(IntStat.LEVEL, value);
        }

        public long xp {
            get => getQ(LongIntStat.TOTALXP);
            set => setQ(LongIntStat.TOTALXP, value);
        }

        public long xpAvailable {
            get => getQ(LongIntStat.AVAILABLEXP);
            set => setQ(LongIntStat.AVAILABLEXP, value);
        }

        public long craftXp {
            get => getQ(LongIntStat.TOTALCRAFTXP);
            set => setQ(LongIntStat.TOTALCRAFTXP, value);
        }

        public long craftXpAvailable {
            get => getQ(LongIntStat.AVAILABLECRAFTXP);
            set => setQ(LongIntStat.AVAILABLECRAFTXP, value);
        }

        public int money {
            get => getQ(IntStat.MONEY);
            set => setQ(IntStat.MONEY, value);
        }

        public bool mounted {
            get => getQ(BoolStat.PLAYER_ISONMOUNT);
            set => setQ(BoolStat.PLAYER_ISONMOUNT, value);
        }

        public void broadcastQualities() {
            if (inWorld) {
                Player? owningPlayer = world.playerManager.get(id);
                foreach (object dirtyStat in dirtyStats) {
                    switch (dirtyStat) {
                        case IntStat stat: {
                                int statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateIntPrivateMsg(stat, statVal));
                                }

                                if (INT_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateIntVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case BoolStat stat: {
                                bool statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateBoolPrivateMsg(stat, statVal));
                                }

                                if (BOOL_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateBoolVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case FloatStat stat: {
                                float statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateFloatPrivateMsg(stat, statVal));
                                }

                                if (FLOAT_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateFloatVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case TimestampStat stat: {
                                double statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateTimestampPrivateMsg(stat, statVal));
                                }

                                if (TIMESTAMP_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateTimestampVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case DataIdStat stat: {
                                DataId statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateDataIdPrivateMsg(stat, statVal));
                                }

                                if (DATA_ID_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateDataIdVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case InstanceIdStat stat: {
                                InstanceId statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateInstanceIdPrivateMsg(stat, statVal));
                                }

                                if (INSTANCE_ID_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateInstanceIdVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case PositionStat stat: {
                                Position? statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdatePositionPrivateMsg(stat, statVal));
                                }

                                if (POSITION_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdatePositionVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case StringInfoStat stat: {
                                StringInfo? statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateStringInfoPrivateMsg(stat, statVal));
                                }

                                if (STRING_INFO_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateStringInfoVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                        case LongIntStat stat: {
                                long statVal = getQ(stat);

                                if (owningPlayer != null) {
                                    world.playerManager.send(owningPlayer, new QualUpdateLongIntPrivateMsg(stat, statVal));
                                }

                                if (LONG_INT_VISUAL_STATS.Contains(stat)) {
                                    world.playerManager.sendAllVisibleExcept(id, owningPlayer, new QualUpdateLongIntVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                }
                                break;
                            }
                    }
                }
            }

            dirtyStats.Clear();
        }

        private int getQ(IntStat stat) => qualities.ints?.GetValueOrDefault(stat) ?? default;
        private void setQ(IntStat stat, int value) {
            if (qualities.ints == null) {
                qualities.ints = new() { { stat, value } };
            } else {
                qualities.ints[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private bool getQ(BoolStat stat) => qualities.bools?.GetValueOrDefault(stat) ?? default;
        private void setQ(BoolStat stat, bool value) {
            if (qualities.bools == null) {
                qualities.bools = new() { { stat, value } };
            } else {
                qualities.bools[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private float getQ(FloatStat stat) => qualities.floats?.GetValueOrDefault(stat) ?? default;
        private void setQ(FloatStat stat, float value) {
            if (qualities.floats == null) {
                qualities.floats = new() { { stat, value } };
            } else {
                qualities.floats[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private double getQ(TimestampStat stat) => qualities.doubles?.GetValueOrDefault(stat) ?? default;
        private void setQ(TimestampStat stat, double value) {
            if (qualities.doubles == null) {
                qualities.doubles = new() { { stat, value } };
            } else {
                qualities.doubles[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private string? getQ(StringStat stat) => qualities.strings?.GetValueOrDefault(stat) ?? default;
        private void setQ(StringStat stat, string? value) {
            if (qualities.strings == null) {
                qualities.strings = new() { { stat, value } };
            } else {
                qualities.strings[stat] = value;
            }
        }

        private DataId getQ(DataIdStat stat) => qualities.dids?.GetValueOrDefault(stat) ?? default;
        private void setQ(DataIdStat stat, DataId value) {
            if (qualities.dids == null) {
                qualities.dids = new() { { stat, value } };
            } else {
                qualities.dids[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private InstanceId getQ(InstanceIdStat stat) => qualities.ids?.GetValueOrDefault(stat) ?? default;
        private void setQ(InstanceIdStat stat, InstanceId value) {
            if (qualities.ids == null) {
                qualities.ids = new() { { stat, value } };
            } else {
                qualities.ids[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private Position? getQ(PositionStat stat) => qualities.poss?.GetValueOrDefault(stat) ?? default;
        private void setQ(PositionStat stat, Position? value) {
            if (qualities.poss == null) {
                qualities.poss = new() { { stat, value } };
            } else {
                qualities.poss[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private StringInfo? getQ(StringInfoStat stat) => qualities.stringInfos?.GetValueOrDefault(stat) ?? default;
        private void setQ(StringInfoStat stat, StringInfo? value) {
            if (qualities.stringInfos == null) {
                qualities.stringInfos = new() { { stat, value } };
            } else {
                qualities.stringInfos[stat] = value;
            }

            dirtyStats.Add(stat);
        }

        private PackageId getQ(uint stat) => qualities.packageIds?.GetValueOrDefault(stat) ?? default;
        private void setQ(uint stat, PackageId value) {
            if (qualities.packageIds == null) {
                qualities.packageIds = new() { { stat, value } };
            } else {
                qualities.packageIds[stat] = value;
            }
        }

        private long getQ(LongIntStat stat) => qualities.longs?.GetValueOrDefault(stat) ?? default;
        private void setQ(LongIntStat stat, long value) {
            if (qualities.longs == null) {
                qualities.longs = new() { { stat, value } };
            } else {
                qualities.longs[stat] = value;
            }

            dirtyStats.Add(stat);
        }
    }
}
