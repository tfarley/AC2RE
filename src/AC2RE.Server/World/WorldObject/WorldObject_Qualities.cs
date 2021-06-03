using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal partial class WorldObject {

        public CBaseQualities qualities;

        private HashSet<object> dirtyStats = new();

        private void initQualities() {
            qualities = new();
            qualities.weenieDesc = new();
        }

        public void syncWeenieDesc() {
            WeenieDesc weenie = qualities.weenieDesc;
            weenie.entityDid = entityDid;
            weenie.name = name;
            weenie.pluralName = pluralName;
            weenie.iconDid = iconDid;
            weenie.containerId = containerId;
            weenie.wielderId = equipperId;
            weenie.monarchId = monarchId;
            weenie.originatorId = originatorId;
            weenie.claimantId = claimantId;
            weenie.killerId = killerId;
            weenie.petSummonerId = summonerId;
            weenie.quantity = quantity;
            weenie.value = value;
            weenie.factionType = faction;
            weenie.pkAlwaysTruePermissions = pkAlwaysTruePermissions;
            weenie.pkAlwaysFalsePermissions = pkAlwaysFalsePermissions;
            weenie.physicsTypeLow = physicsTypeLow;
            weenie.physicsTypeHigh = physicsTypeHigh;
            weenie.movementEtherealLow = movementEtherealLow;
            weenie.movementEtherealHigh = movementEtherealHigh;
            weenie.placementEtherealLow = placementEtherealLow;
            weenie.placementEtherealHigh = placementEtherealHigh;
            weenie.durabilityCurrentLevel = durability;
            weenie.durabilityMaxLevel = durabilityMax;
            weenie.scale = scale;
        }

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
            get => getQ(DataIdStat.ICONID);
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

        public InstanceId equipperId {
            get => getQ(InstanceIdStat.EQUIPPER);
            set {
                setQ(InstanceIdStat.EQUIPPER, value);
                qualities.weenieDesc.wielderId = value;
            }
        }

        public InvLoc equippedLocation {
            get => (InvLoc)getQ(IntStat.CURRENTEQUIPPEDLOCATION);
            set => setQ(IntStat.CURRENTEQUIPPEDLOCATION, (int)value);
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

        public DataId physicsEntityDid {
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

        private void broadcastQualities() {
            if (inWorld) {
                foreach (object dirtyStat in dirtyStats) {
                    switch (dirtyStat) {
                        case IntStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    int statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateIntPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateIntVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case BoolStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    bool statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateBoolPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateBoolVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case FloatStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    float statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateFloatPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateFloatVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case TimestampStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    double statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateTimestampPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateTimestampVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case DataIdStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    DataId statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateDataIdPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateDataIdVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case InstanceIdStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    InstanceId statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateInstanceIdPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateInstanceIdVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case PositionStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    Position? statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdatePositionPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdatePositionVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case StringInfoStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    StringInfo? statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateStringInfoPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateStringInfoVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                        case LongIntStat stat: {
                                StatCfg.SyncMode syncMode = StatCfg.getSyncMode(stat);
                                if (syncMode != StatCfg.SyncMode.NONE) {
                                    long statVal = getQ(stat);

                                    if (player != null && syncMode == StatCfg.SyncMode.PRIVATE) {
                                        world.playerManager.send(player, new QualUpdateLongIntPrivateMsg(stat, statVal));
                                    }

                                    if (syncMode == StatCfg.SyncMode.VISUAL) {
                                        world.playerManager.sendAllVisible(id, new QualUpdateLongIntVisualMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), stat, statVal));
                                    }
                                }
                                break;
                            }
                    }
                }
            }

            dirtyStats.Clear();
        }

        public int getQ(IntStat stat) => qualities.ints?.GetValueOrDefault(stat) ?? default;
        public void setQ(IntStat stat, int value) {
            if (qualities.ints == null) {
                if (value != default) {
                    qualities.ints = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.ints[stat] = value;
                } else {
                    qualities.ints.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public bool getQ(BoolStat stat) => qualities.bools?.GetValueOrDefault(stat) ?? default;
        public void setQ(BoolStat stat, bool value) {
            if (qualities.bools == null) {
                if (value != default) {
                    qualities.bools = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.bools[stat] = value;
                } else {
                    qualities.bools.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public float getQ(FloatStat stat) => qualities.floats?.GetValueOrDefault(stat) ?? default;
        public void setQ(FloatStat stat, float value) {
            if (qualities.floats == null) {
                if (value != default) {
                    qualities.floats = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.floats[stat] = value;
                } else {
                    qualities.floats.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public double getQ(TimestampStat stat) => qualities.doubles?.GetValueOrDefault(stat) ?? default;
        public void setQ(TimestampStat stat, double value) {
            if (qualities.doubles == null) {
                if (value != default) {
                    qualities.doubles = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.doubles[stat] = value;
                } else {
                    qualities.doubles.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public string? getQ(StringStat stat) => qualities.strings?.GetValueOrDefault(stat) ?? default;
        public void setQ(StringStat stat, string? value) {
            if (qualities.strings == null) {
                if (value != default) {
                    qualities.strings = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.strings[stat] = value;
                } else {
                    qualities.strings.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public DataId getQ(DataIdStat stat) => qualities.dids?.GetValueOrDefault(stat) ?? default;
        public void setQ(DataIdStat stat, DataId value) {
            if (qualities.dids == null) {
                if (value != default) {
                    qualities.dids = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.dids[stat] = value;
                } else {
                    qualities.dids.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public InstanceId getQ(InstanceIdStat stat) => qualities.ids?.GetValueOrDefault(stat) ?? default;
        public void setQ(InstanceIdStat stat, InstanceId value) {
            if (qualities.ids == null) {
                if (value != default) {
                    qualities.ids = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.ids[stat] = value;
                } else {
                    qualities.ids.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public Position? getQ(PositionStat stat) => qualities.poss?.GetValueOrDefault(stat) ?? default;
        public void setQ(PositionStat stat, Position? value) {
            if (qualities.poss == null) {
                if (value != default) {
                    qualities.poss = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.poss[stat] = value;
                } else {
                    qualities.poss.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public StringInfo? getQ(StringInfoStat stat) => qualities.stringInfos?.GetValueOrDefault(stat) ?? default;
        public void setQ(StringInfoStat stat, StringInfo? value) {
            if (qualities.stringInfos == null) {
                if (value != default) {
                    qualities.stringInfos = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.stringInfos[stat] = value;
                } else {
                    qualities.stringInfos.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public PackageId getQ(uint stat) => qualities.packageIds?.GetValueOrDefault(stat) ?? default;
        public void setQ(uint stat, PackageId value) {
            if (qualities.packageIds == null) {
                if (value != default) {
                    qualities.packageIds = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.packageIds[stat] = value;
                } else {
                    qualities.packageIds.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }

        public long getQ(LongIntStat stat) => qualities.longs?.GetValueOrDefault(stat) ?? default;
        public void setQ(LongIntStat stat, long value) {
            if (qualities.longs == null) {
                if (value != default) {
                    qualities.longs = new() { { stat, value } };
                    dirtyStats.Add(stat);
                }
            } else {
                if (value != default) {
                    qualities.longs[stat] = value;
                } else {
                    qualities.longs.Remove(stat);
                }
                dirtyStats.Add(stat);
            }
        }
    }
}
