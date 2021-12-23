using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server;

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
        get => getQ(StringInfoStat.Name);
        set {
            setQ(StringInfoStat.Name, value);
            qualities.weenieDesc.name = value;
        }
    }

    public StringInfo? pluralName {
        get => getQ(StringInfoStat.PluralName);
        set {
            setQ(StringInfoStat.PluralName, value);
            qualities.weenieDesc.pluralName = value;
        }
    }

    public DataId iconDid {
        get => getQ(DataIdStat.IconID);
        set {
            setQ(DataIdStat.IconID, value);
            qualities.weenieDesc.iconDid = value;
        }
    }

    public InstanceId containerId {
        get => getQ(InstanceIdStat.Container);
        set {
            setQ(InstanceIdStat.Container, value);
            qualities.weenieDesc.containerId = value;
        }
    }

    public InstanceId equipperId {
        get => getQ(InstanceIdStat.Equipper);
        set {
            setQ(InstanceIdStat.Equipper, value);
            qualities.weenieDesc.wielderId = value;
        }
    }

    public InvLoc equippedLocation {
        get => (InvLoc)getQ(IntStat.CurrentEquippedLocation);
        set => setQ(IntStat.CurrentEquippedLocation, (int)value);
    }

    public InstanceId monarchId {
        get => getQ(InstanceIdStat.Allegiance_Monarch);
        set {
            setQ(InstanceIdStat.Allegiance_Monarch, value);
            qualities.weenieDesc.monarchId = value;
        }
    }

    public InstanceId originatorId {
        get => getQ(InstanceIdStat.Originator);
        set {
            setQ(InstanceIdStat.Originator, value);
            qualities.weenieDesc.originatorId = value;
        }
    }

    public InstanceId claimantId {
        get => getQ(InstanceIdStat.Claimant);
        set {
            setQ(InstanceIdStat.Claimant, value);
            qualities.weenieDesc.claimantId = value;
        }
    }

    public InstanceId killerId {
        get => getQ(InstanceIdStat.Plunderer);
        set {
            setQ(InstanceIdStat.Plunderer, value);
            qualities.weenieDesc.killerId = value;
        }
    }

    public InstanceId summonerId {
        get => getQ(InstanceIdStat.AI_PetSummoner);
        set {
            setQ(InstanceIdStat.AI_PetSummoner, value);
            qualities.weenieDesc.petSummonerId = value;
        }
    }

    public int quantity {
        get => getQ(IntStat.Quantity);
        set {
            setQ(IntStat.Quantity, value);
            qualities.weenieDesc.quantity = value;
        }
    }

    public int value {
        get => getQ(IntStat.Value);
        set {
            setQ(IntStat.Value, value);
            qualities.weenieDesc.value = value;
        }
    }

    public FactionType faction {
        get => (FactionType)getQ(IntStat.Faction_Membership);
        set {
            setQ(IntStat.Faction_Membership, (int)value);
            qualities.weenieDesc.factionType = value;
        }
    }

    public int pkAlwaysTruePermissions {
        get => getQ(IntStat.PK_AlwaysTruePermissions);
        set {
            setQ(IntStat.PK_AlwaysTruePermissions, value);
            qualities.weenieDesc.pkAlwaysTruePermissions = value;
        }
    }

    public int pkAlwaysFalsePermissions {
        get => getQ(IntStat.PK_AlwaysFalsePermissions);
        set {
            setQ(IntStat.PK_AlwaysFalsePermissions, value);
            qualities.weenieDesc.pkAlwaysFalsePermissions = value;
        }
    }

    public int physicsTypeLow {
        get => getQ(IntStat.EtherealPhysicsTypeLow);
        set {
            setQ(IntStat.EtherealPhysicsTypeLow, value);
            qualities.weenieDesc.physicsTypeLow = value;
        }
    }

    public int physicsTypeHigh {
        get => getQ(IntStat.EtherealPhysicsTypeHigh);
        set {
            setQ(IntStat.EtherealPhysicsTypeHigh, value);
            qualities.weenieDesc.physicsTypeHigh = value;
        }
    }

    public int movementEtherealLow {
        get => getQ(IntStat.EtherealMovementTypeLow);
        set {
            setQ(IntStat.EtherealMovementTypeLow, value);
            qualities.weenieDesc.movementEtherealLow = value;
        }
    }

    public int movementEtherealHigh {
        get => getQ(IntStat.EtherealMovementTypeHigh);
        set {
            setQ(IntStat.EtherealMovementTypeHigh, value);
            qualities.weenieDesc.movementEtherealHigh = value;
        }
    }

    public int placementEtherealLow {
        get => getQ(IntStat.EtherealPlacementTypeLow);
        set {
            setQ(IntStat.EtherealPlacementTypeLow, value);
            qualities.weenieDesc.placementEtherealLow = value;
        }
    }

    public int placementEtherealHigh {
        get => getQ(IntStat.EtherealPlacementTypeHigh);
        set {
            setQ(IntStat.EtherealPlacementTypeHigh, value);
            qualities.weenieDesc.placementEtherealHigh = value;
        }
    }

    public int durability {
        get => getQ(IntStat.Durability_CurrentLevel);
        set {
            setQ(IntStat.Durability_CurrentLevel, value);
            qualities.weenieDesc.durabilityCurrentLevel = value;
        }
    }

    public int durabilityMax {
        get => getQ(IntStat.Durability_MaxLevel);
        set {
            setQ(IntStat.Durability_MaxLevel, value);
            qualities.weenieDesc.durabilityMaxLevel = value;
        }
    }

    public float scale {
        get => getQ(FloatStat.Physics_Scale);
        set {
            setQ(FloatStat.Physics_Scale, value);
            qualities.weenieDesc.scale = value;
        }
    }

    public bool open {
        get => getQ(BoolStat.Open);
        set {
            setQ(BoolStat.Open, value);
            if (value) {
                qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.Open;
            } else {
                qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.Open;
            }
        }
    }

    public bool dead {
        get => getQ(BoolStat.Dead);
        set {
            setQ(BoolStat.Dead, value);
            if (value) {
                qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.Dead;
            } else {
                qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.Dead;
            }
        }
    }

    public bool selectable {
        get => getQ(BoolStat.IsSelectable);
        set {
            setQ(BoolStat.IsSelectable, value);
            if (value) {
                qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.Selectable;
            } else {
                qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.Selectable;
            }
        }
    }

    public bool takeable {
        get => getQ(BoolStat.IsTakeable);
        set => setQ(BoolStat.IsTakeable, value);
    }

    public bool noDraw {
        get => getQ(BoolStat.NoDraw);
        set {
            setQ(BoolStat.NoDraw, value);
            if (value) {
                qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.NoDraw;
            } else {
                qualities.weenieDesc.packFlags &= ~WeenieDesc.PackFlag.NoDraw;
            }
        }
    }

    public SpeciesType species {
        get => (SpeciesType)getQ(IntStat.Species);
        set => setQ(IntStat.Species, (int)value);
    }

    public SexType sex {
        get => (SexType)getQ(IntStat.Sex);
        set => setQ(IntStat.Sex, (int)value);
    }

    public InvLoc preferredInvLoc {
        get => (InvLoc)getQ(IntStat.PreferredInventoryLocation);
        set => setQ(IntStat.PreferredInventoryLocation, (int)value);
    }

    public InvLoc validInvLocs {
        get => (InvLoc)getQ(IntStat.ValidInventoryLocations);
        set => qualities.ints[IntStat.ValidInventoryLocations] = (int)value;
    }

    public HoldingLocation primaryHoldLoc {
        get => (HoldingLocation)getQ(IntStat.Inv_PrimaryParentingLocation);
        set => qualities.ints[IntStat.Inv_PrimaryParentingLocation] = (int)value;
    }

    public Orientation holdOrientation {
        get => (Orientation)getQ(IntStat.PlacementPosition);
        set => setQ(IntStat.PlacementPosition, (int)value);
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
        get => getQ(DataIdStat.PhysObj);
        set => setQ(DataIdStat.PhysObj, value);
    }

    public DataId pileAppearanceDid {
        get => getQ(DataIdStat.PileAppearanceID);
        set => setQ(DataIdStat.PileAppearanceID, value);
    }

    public int health {
        get => getQ(IntStat.Health_CurrentLevel);
        set => setQ(IntStat.Health_CurrentLevel, value);
    }

    public int healthMax {
        get => getQ(IntStat.Health_CachedMax);
        set => setQ(IntStat.Health_CachedMax, value);
    }

    public float healthRegen {
        get => getQ(FloatStat.Health_RegenRate);
        set => setQ(FloatStat.Health_RegenRate, value);
    }

    public int vigor {
        get => getQ(IntStat.Vigor_CurrentLevel);
        set => setQ(IntStat.Vigor_CurrentLevel, value);
    }

    public int vigorMax {
        get => getQ(IntStat.Vigor_CachedMax);
        set => setQ(IntStat.Vigor_CachedMax, value);
    }

    public float vigorRegen {
        get => getQ(FloatStat.Vigor_RegenRate);
        set => setQ(FloatStat.Vigor_RegenRate, value);
    }

    public int capacity {
        get => getQ(IntStat.ContainerMaxCapacity);
        set => setQ(IntStat.ContainerMaxCapacity, value);
    }

    public int playerClass {
        get => getQ(IntStat.Class);
        set => setQ(IntStat.Class, value);
    }

    public int level {
        get => getQ(IntStat.Level);
        set => setQ(IntStat.Level, value);
    }

    public long xp {
        get => getQ(LongIntStat.TotalXP);
        set => setQ(LongIntStat.TotalXP, value);
    }

    public long xpAvailable {
        get => getQ(LongIntStat.AvailableXP);
        set => setQ(LongIntStat.AvailableXP, value);
    }

    public long craftXp {
        get => getQ(LongIntStat.TotalCraftXP);
        set => setQ(LongIntStat.TotalCraftXP, value);
    }

    public long craftXpAvailable {
        get => getQ(LongIntStat.AvailableCraftXP);
        set => setQ(LongIntStat.AvailableCraftXP, value);
    }

    public int money {
        get => getQ(IntStat.Money);
        set => setQ(IntStat.Money, value);
    }

    public bool mounted {
        get => getQ(BoolStat.Player_IsOnMount);
        set => setQ(BoolStat.Player_IsOnMount, value);
    }

    public ImplementType implementType {
        get => (ImplementType)getQ(IntStat.ImplementType);
        set => setQ(IntStat.ImplementType, (int)value);
    }

    public ModeId singleWeaponMode {
        get => (ModeId)getQ(IntStat.Weapon_SingleWeaponStance);
        set => setQ(IntStat.Weapon_SingleWeaponStance, (int)value);
    }

    public ModeId withShieldMode {
        get => (ModeId)getQ(IntStat.Weapon_WithShieldStance);
        set => setQ(IntStat.Weapon_WithShieldStance, (int)value);
    }

    public ModeId dualWieldMode {
        get => (ModeId)getQ(IntStat.Weapon_DualWieldStance);
        set => setQ(IntStat.Weapon_DualWieldStance, (int)value);
    }

    private void broadcastQualities() {
        if (inWorld) {
            foreach (object dirtyStat in dirtyStats) {
                switch (dirtyStat) {
                    case IntStat stat:
                        broadcastStat(stat);
                        break;
                    case BoolStat stat:
                        broadcastStat(stat);
                        break;
                    case FloatStat stat:
                        broadcastStat(stat);
                        break;
                    case TimestampStat stat:
                        broadcastStat(stat);
                        break;
                    case DataIdStat stat:
                        broadcastStat(stat);
                        break;
                    case InstanceIdStat stat:
                        broadcastStat(stat);
                        break;
                    case PositionStat stat:
                        broadcastStat(stat);
                        break;
                    case StringInfoStat stat:
                        broadcastStat(stat);
                        break;
                    case LongIntStat stat:
                        broadcastStat(stat);
                        break;
                }
            }
        }

        dirtyStats.Clear();
    }

    private void broadcastStat(IntStat stat) {
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
    }

    private void broadcastStat(BoolStat stat) {
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
    }

    private void broadcastStat(FloatStat stat) {
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
    }

    private void broadcastStat(TimestampStat stat) {
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
    }

    private void broadcastStat(DataIdStat stat) {
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
    }

    private void broadcastStat(InstanceIdStat stat) {
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
    }

    private void broadcastStat(PositionStat stat) {
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
    }

    private void broadcastStat(StringInfoStat stat) {
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
    }

    private void broadcastStat(LongIntStat stat) {
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
