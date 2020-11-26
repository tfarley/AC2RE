using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Server {

    internal class WorldObject {

        public readonly InstanceId id;
        public bool deleted;
        public ushort instanceStamp;

        [DatabaseIgnore]
        public bool inWorld;

        public float heading;

        [DatabaseIgnore]
        public Vector3 motion;

        public Dictionary<InvLoc, InstanceId> equippedItemIds = new();
        public List<InstanceId> containedItemIds = new();

        public PhysicsDesc physics;
        public VisualDesc visual;
        public CBaseQualities qualities;

        [DatabaseIgnore]
        public StringInfo? name {
            get => getQ(StringInfoStat.NAME);
            set {
                setQ(StringInfoStat.NAME, value);
                qualities.weenieDesc.name = value;
            }
        }

        [DatabaseIgnore]
        public StringInfo? pluralName {
            get => getQ(StringInfoStat.PLURALNAME);
            set {
                setQ(StringInfoStat.PLURALNAME, value);
                qualities.weenieDesc.pluralName = value;
            }
        }

        [DatabaseIgnore]
        public DataId iconDid {
            get => getQ(DataIdStat.PHYSOBJ);
            set {
                setQ(DataIdStat.ICONID, value);
                qualities.weenieDesc.iconDid = value;
            }
        }

        [DatabaseIgnore]
        public InstanceId containerId {
            get => getQ(InstanceIdStat.CONTAINER);
            set {
                setQ(InstanceIdStat.CONTAINER, value);
                qualities.weenieDesc.containerId = value;
            }
        }

        [DatabaseIgnore]
        public InstanceId wielderId {
            get => getQ(InstanceIdStat.EQUIPPER);
            set {
                setQ(InstanceIdStat.EQUIPPER, value);
                qualities.weenieDesc.wielderId = value;
            }
        }

        [DatabaseIgnore]
        public InstanceId monarchId {
            get => getQ(InstanceIdStat.ALLEGIANCE_MONARCH);
            set {
                setQ(InstanceIdStat.ALLEGIANCE_MONARCH, value);
                qualities.weenieDesc.monarchId = value;
            }
        }

        [DatabaseIgnore]
        public InstanceId originatorId {
            get => getQ(InstanceIdStat.ORIGINATOR);
            set {
                setQ(InstanceIdStat.ORIGINATOR, value);
                qualities.weenieDesc.originatorId = value;
            }
        }

        [DatabaseIgnore]
        public InstanceId claimantId {
            get => getQ(InstanceIdStat.CLAIMANT);
            set {
                setQ(InstanceIdStat.CLAIMANT, value);
                qualities.weenieDesc.claimantId = value;
            }
        }

        [DatabaseIgnore]
        public InstanceId killerId {
            get => getQ(InstanceIdStat.PLUNDERER);
            set {
                setQ(InstanceIdStat.PLUNDERER, value);
                qualities.weenieDesc.killerId = value;
            }
        }

        [DatabaseIgnore]
        public InstanceId summonerId {
            get => getQ(InstanceIdStat.AI_PETSUMMONER);
            set {
                setQ(InstanceIdStat.AI_PETSUMMONER, value);
                qualities.weenieDesc.petSummonerId = value;
            }
        }

        [DatabaseIgnore]
        public int quantity {
            get => getQ(IntStat.QUANTITY);
            set {
                setQ(IntStat.QUANTITY, value);
                qualities.weenieDesc.quantity = value;
            }
        }

        [DatabaseIgnore]
        public int value {
            get => getQ(IntStat.VALUE);
            set {
                setQ(IntStat.VALUE, value);
                qualities.weenieDesc.value = value;
            }
        }

        [DatabaseIgnore]
        public FactionType faction {
            get => (FactionType)getQ(IntStat.FACTION_MEMBERSHIP);
            set {
                setQ(IntStat.FACTION_MEMBERSHIP, (int)value);
                qualities.weenieDesc.factionType = value;
            }
        }

        [DatabaseIgnore]
        public int pkAlwaysTruePermissions {
            get => getQ(IntStat.PK_ALWAYSTRUEPERMISSIONS);
            set {
                setQ(IntStat.PK_ALWAYSTRUEPERMISSIONS, value);
                qualities.weenieDesc.pkAlwaysTruePermissions = value;
            }
        }

        [DatabaseIgnore]
        public int pkAlwaysFalsePermissions {
            get => getQ(IntStat.PK_ALWAYSFALSEPERMISSIONS);
            set {
                setQ(IntStat.PK_ALWAYSFALSEPERMISSIONS, value);
                qualities.weenieDesc.pkAlwaysFalsePermissions = value;
            }
        }

        [DatabaseIgnore]
        public int physicsTypeLow {
            get => getQ(IntStat.ETHEREALPHYSICSTYPELOW);
            set {
                setQ(IntStat.ETHEREALPHYSICSTYPELOW, value);
                qualities.weenieDesc.physicsTypeLow = value;
            }
        }

        [DatabaseIgnore]
        public int physicsTypeHigh {
            get => getQ(IntStat.ETHEREALPHYSICSTYPEHIGH);
            set {
                setQ(IntStat.ETHEREALPHYSICSTYPEHIGH, value);
                qualities.weenieDesc.physicsTypeHigh = value;
            }
        }

        [DatabaseIgnore]
        public int movementEtherealLow {
            get => getQ(IntStat.ETHEREALMOVEMENTTYPELOW);
            set {
                setQ(IntStat.ETHEREALMOVEMENTTYPELOW, value);
                qualities.weenieDesc.movementEtherealLow = value;
            }
        }

        [DatabaseIgnore]
        public int movementEtherealHigh {
            get => getQ(IntStat.ETHEREALMOVEMENTTYPEHIGH);
            set {
                setQ(IntStat.ETHEREALMOVEMENTTYPEHIGH, value);
                qualities.weenieDesc.movementEtherealHigh = value;
            }
        }

        [DatabaseIgnore]
        public int placementEtherealLow {
            get => getQ(IntStat.ETHEREALPLACEMENTTYPELOW);
            set {
                setQ(IntStat.ETHEREALPLACEMENTTYPELOW, value);
                qualities.weenieDesc.placementEtherealLow = value;
            }
        }

        [DatabaseIgnore]
        public int placementEtherealHigh {
            get => getQ(IntStat.ETHEREALPLACEMENTTYPEHIGH);
            set {
                setQ(IntStat.ETHEREALPLACEMENTTYPEHIGH, value);
                qualities.weenieDesc.placementEtherealHigh = value;
            }
        }

        [DatabaseIgnore]
        public int durability {
            get => getQ(IntStat.DURABILITY_CURRENTLEVEL);
            set {
                setQ(IntStat.DURABILITY_CURRENTLEVEL, value);
                qualities.weenieDesc.durabilityCurrentLevel = value;
            }
        }

        [DatabaseIgnore]
        public int durabilityMax {
            get => getQ(IntStat.DURABILITY_MAXLEVEL);
            set {
                setQ(IntStat.DURABILITY_MAXLEVEL, value);
                qualities.weenieDesc.durabilityMaxLevel = value;
            }
        }

        [DatabaseIgnore]
        public float scale {
            get => getQ(FloatStat.PHYSICS_SCALE);
            set {
                setQ(FloatStat.PHYSICS_SCALE, value);
                qualities.weenieDesc.scale = value;
            }
        }

        [DatabaseIgnore]
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

        [DatabaseIgnore]
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

        [DatabaseIgnore]
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

        [DatabaseIgnore]
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

        [DatabaseIgnore]
        public SpeciesType species {
            get => (SpeciesType)getQ(IntStat.SPECIES);
            set => setQ(IntStat.SPECIES, (int)value);
        }

        [DatabaseIgnore]
        public SexType sex {
            get => (SexType)getQ(IntStat.SEX);
            set => setQ(IntStat.SEX, (int)value);
        }

        [DatabaseIgnore]
        public InvLoc preferredInvLoc {
            get => (InvLoc)getQ(IntStat.PREFERREDINVENTORYLOCATION);
            set => setQ(IntStat.PREFERREDINVENTORYLOCATION, (int)value);
        }

        [DatabaseIgnore]
        public InvLoc validInvLocs {
            get => (InvLoc)getQ(IntStat.VALIDINVENTORYLOCATIONS);
            set => qualities.ints[IntStat.VALIDINVENTORYLOCATIONS] = (int)value;
        }

        [DatabaseIgnore]
        public HoldingLocation primaryHoldLoc {
            get => (HoldingLocation)getQ(IntStat.INV_PRIMARYPARENTINGLOCATION);
            set => qualities.ints[IntStat.INV_PRIMARYPARENTINGLOCATION] = (int)value;
        }

        [DatabaseIgnore]
        public Orientation holdOrientation {
            get => (Orientation)getQ(IntStat.INV_PRIMARYPARENTINGLOCATION);
            set => setQ(IntStat.INV_PRIMARYPARENTINGLOCATION, (int)value);
        }

        [DatabaseIgnore]
        public DataId entityDid {
            get => qualities.weenieDesc.entityDid;
            set => qualities.weenieDesc.entityDid = value;
        }

        [DatabaseIgnore]
        public DataId physObjDid {
            get => getQ(DataIdStat.PHYSOBJ);
            set => setQ(DataIdStat.PHYSOBJ, value);
        }

        [DatabaseIgnore]
        public int health {
            get => getQ(IntStat.HEALTH_CURRENTLEVEL);
            set => setQ(IntStat.HEALTH_CURRENTLEVEL, value);
        }

        [DatabaseIgnore]
        public int healthMax {
            get => getQ(IntStat.HEALTH_CACHEDMAX);
            set => setQ(IntStat.HEALTH_CACHEDMAX, value);
        }

        [DatabaseIgnore]
        public float healthRegen {
            get => getQ(FloatStat.HEALTH_REGENRATE);
            set => setQ(FloatStat.HEALTH_REGENRATE, value);
        }

        [DatabaseIgnore]
        public int vigor {
            get => getQ(IntStat.VIGOR_CURRENTLEVEL);
            set => setQ(IntStat.VIGOR_CURRENTLEVEL, value);
        }

        [DatabaseIgnore]
        public int vigorMax {
            get => getQ(IntStat.VIGOR_CACHEDMAX);
            set => setQ(IntStat.VIGOR_CACHEDMAX, value);
        }

        [DatabaseIgnore]
        public float vigorRegen {
            get => getQ(FloatStat.VIGOR_REGENRATE);
            set => setQ(FloatStat.VIGOR_REGENRATE, value);
        }

        [DatabaseIgnore]
        public int capacity {
            get => getQ(IntStat.CONTAINERMAXCAPACITY);
            set => setQ(IntStat.CONTAINERMAXCAPACITY, value);
        }

        [DatabaseIgnore]
        public int playerClass {
            get => getQ(IntStat.CLASS);
            set => setQ(IntStat.CLASS, value);
        }

        [DatabaseIgnore]
        public int level {
            get => getQ(IntStat.LEVEL);
            set => setQ(IntStat.LEVEL, value);
        }

        [DatabaseIgnore]
        public long xp {
            get => getQ(LongIntStat.TOTALXP);
            set => setQ(LongIntStat.TOTALXP, value);
        }

        [DatabaseIgnore]
        public long xpAvailable {
            get => getQ(LongIntStat.AVAILABLEXP);
            set => setQ(LongIntStat.AVAILABLEXP, value);
        }

        [DatabaseIgnore]
        public long craftXp {
            get => getQ(LongIntStat.TOTALCRAFTXP);
            set => setQ(LongIntStat.TOTALCRAFTXP, value);
        }

        [DatabaseIgnore]
        public long craftXpAvailable {
            get => getQ(LongIntStat.AVAILABLECRAFTXP);
            set => setQ(LongIntStat.AVAILABLECRAFTXP, value);
        }

        [DatabaseIgnore]
        public int money {
            get => getQ(IntStat.MONEY);
            set => setQ(IntStat.MONEY, value);
        }

        [DatabaseIgnore]
        public bool mounted {
            get => getQ(BoolStat.PLAYER_ISONMOUNT);
            set => setQ(BoolStat.PLAYER_ISONMOUNT, value);
        }

        public WorldObject(InstanceId id) : this(id, new(), new(), new()) {
            qualities.weenieDesc = new();
        }

        public WorldObject(InstanceId id, PhysicsDesc physics, VisualDesc visual, CBaseQualities qualities) {
            this.id = id;

            this.physics = physics;
            this.visual = visual;
            this.qualities = qualities;
        }

        public InstanceIdWithStamp getInstanceIdWithStamp(ushort otherStamp = 0) {
            return new() {
                id = id,
                instanceStamp = instanceStamp,
                otherStamp = otherStamp,
            };
        }

        // TODO: When any quality is set, broadcast update message to players

        private int getQ(IntStat stat) => qualities.ints?.GetValueOrDefault(stat) ?? default;
        private void setQ(IntStat stat, int value) {
            if (qualities.ints == null) {
                qualities.ints = new() { { stat, value } };
            } else {
                qualities.ints[stat] = value;
            }
        }

        private bool getQ(BoolStat stat) => qualities.bools?.GetValueOrDefault(stat) ?? default;
        private void setQ(BoolStat stat, bool value) {
            if (qualities.bools == null) {
                qualities.bools = new() { { stat, value } };
            } else {
                qualities.bools[stat] = value;
            }
        }

        private float getQ(FloatStat stat) => qualities.floats?.GetValueOrDefault(stat) ?? default;
        private void setQ(FloatStat stat, float value) {
            if (qualities.floats == null) {
                qualities.floats = new() { { stat, value } };
            } else {
                qualities.floats[stat] = value;
            }
        }

        private double getQ(TimestampStat stat) => qualities.doubles?.GetValueOrDefault(stat) ?? default;
        private void setQ(TimestampStat stat, double value) {
            if (qualities.doubles == null) {
                qualities.doubles = new() { { stat, value } };
            } else {
                qualities.doubles[stat] = value;
            }
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
        }

        private InstanceId getQ(InstanceIdStat stat) => qualities.ids?.GetValueOrDefault(stat) ?? default;
        private void setQ(InstanceIdStat stat, InstanceId value) {
            if (qualities.ids == null) {
                qualities.ids = new() { { stat, value } };
            } else {
                qualities.ids[stat] = value;
            }
        }

        private Position? getQ(PositionStat stat) => qualities.poss?.GetValueOrDefault(stat) ?? default;
        private void setQ(PositionStat stat, Position? value) {
            if (qualities.poss == null) {
                qualities.poss = new() { { stat, value } };
            } else {
                qualities.poss[stat] = value;
            }
        }

        private StringInfo? getQ(StringInfoStat stat) => qualities.stringInfos?.GetValueOrDefault(stat) ?? default;
        private void setQ(StringInfoStat stat, StringInfo? value) {
            if (qualities.stringInfos == null) {
                qualities.stringInfos = new() { { stat, value } };
            } else {
                qualities.stringInfos[stat] = value;
            }
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
        }
    }
}
