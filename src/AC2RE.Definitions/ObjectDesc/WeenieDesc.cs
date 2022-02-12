using System;

namespace AC2RE.Definitions;

public class WeenieDesc {

    // Enum gmWDDataMask
    [Flags]
    public enum PackFlag : uint {
        Undef = 0, // Undef_gmWDDataMask
        MyPackageID = 1 << 0, // MyPackageID_gmWDDataMask 0x00000001
        Bitfield = 1 << 1, // Bitfield_gmWDDataMask 0x00000002
        Name = 1 << 2, // Name_gmWDDataMask 0x00000004
        PluralName = 1 << 3, // PluralName_gmWDDataMask 0x00000008
        IconID = 1 << 4, // IconID_gmWDDataMask 0x00000010
        ContainerID = 1 << 5, // ContainerID_gmWDDataMask 0x00000020
        WielderID = 1 << 6, // WielderID_gmWDDataMask 0x00000040
        Physics_Scale = 1 << 7, // Physics_Scale_gmWDDataMask 0x00000080
        MonarchID = 1 << 8, // MonarchID_gmWDDataMask 0x00000100
        OriginatorID = 1 << 9, // OriginatorID_gmWDDataMask 0x00000200
        Open = 1 << 10, // Open_gmWDDataMask 0x00000400
        Quantity = 1 << 11, // Quantity_gmWDDataMask 0x00000800
        Value = 1 << 12, // Value_gmWDDataMask 0x00001000
        FactionType = 1 << 13, // FactionType_gmWDDataMask 0x00002000
        Dead = 1 << 14, // Dead_gmWDDataMask 0x00004000
        PhysicsTypeLowDWord = 1 << 15, // PhysicsTypeLowDWord_gmWDDataMask 0x00008000
        PhysicsTypeHighDWord = 1 << 16, // PhysicsTypeHighDWord_gmWDDataMask 0x00010000
        MovementEtherealLowDWord = 1 << 17, // MovementEtherealLowDWord_gmWDDataMask 0x00020000
        MovementEtherealHighDWord = 1 << 18, // MovementEtherealHighDWord_gmWDDataMask 0x00040000
        PlacementEtherealLowDWord = 1 << 19, // PlacementEtherealLowDWord_gmWDDataMask 0x00080000
        PlacementEtherealHighDWord = 1 << 20, // PlacementEtherealHighDWord_gmWDDataMask 0x00100000
        PKAlwaysTruePermissions = 1 << 21, // PKAlwaysTruePermissions_gmWDDataMask 0x00200000
        PKAlwaysFalsePermissions = 1 << 22, // PKAlwaysFalsePermissions_gmWDDataMask 0x00400000
        Selectable = 1 << 23, // Selectable_gmWDDataMask 0x00800000
        NoDraw = 1 << 24, // NoDraw_gmWDDataMask 0x01000000
        EntityDID = 1 << 25, // EntityDID_gmWDDataMask 0x02000000
        PetSummonerID = 1 << 26, // PetSummonerID_gmWDDataMask 0x04000000
        Durability_CurrentLevel = 1 << 27, // Durability_CurrentLevel_gmWDDataMask 0x08000000
        Durability_MaxLevel = 1 << 28, // Durability_MaxLevel_gmWDDataMask 0x10000000
        ClaimantID = 1 << 29, // ClaimantID_gmWDDataMask 0x20000000
        KillerID = 1 << 30, // KillerID_gmWDDataMask 0x40000000
    }

    // Enum gmWeenieDesc::BitfieldIndex
    [Flags]
    public enum Bitfield : uint {
        Undef = 0, // Undef_BitfieldIndex
        Open = 1 << 0, // Open_BitfieldIndex 0x00000001
        Selectable = 1 << 1, // Selectable_BitfieldIndex 0x00000002
        NoDraw = 1 << 2, // NoDraw_BitfieldIndex 0x00000004
        Dead = 1 << 3, // Dead_BitfieldIndex 0x00000008
    }

    // WeenieDesc
    public Bitfield bitfield; // _bitfield
    public PackageType packageType; // _my_pkgid
    public DataId entityDid; // m_entityDID
    public PackFlag packFlags; // _data_mask
    public StringInfo name; // _name
    public StringInfo pluralName; // _plural_name
    public DataId iconDid; // _icon
    public InstanceId containerId; // _container
    public InstanceId wielderId; // _wielder
    public InstanceId monarchId; // _monarch
    public InstanceId originatorId; // _originator
    public InstanceId claimantId; // _claimant
    public InstanceId killerId; // _killer
    public InstanceId petSummonerId; // _petSummoner
    public int quantity; // _quantity
    public int value; // _value
    public FactionType factionType; // _factionType
    public int pkAlwaysTruePermissions; // _pkAlwaysTrue
    public int pkAlwaysFalsePermissions; // _pkAlwaysFalse
    public int physicsTypeLow; // _physicsTypeLowDWord // TODO: How to combine?
    public int physicsTypeHigh; // _physicsTypeHighDWord // TODO: How to combine?
    public int movementEtherealLow; // _movementEtherealLowDWord // TODO: How to combine?
    public int movementEtherealHigh; // _movementEtherealHighDWord // TODO: How to combine?
    public int placementEtherealLow; // _placementEtherealLowDWord // TODO: How to combine?
    public int placementEtherealHigh; // _placementEtherealHighDWord // TODO: How to combine?
    public int durabilityCurrentLevel; // _durability_CurrentLevel
    public int durabilityMaxLevel; // _durability_MaxLevel
    public float scale; // _scale

    public WeenieDesc() {

    }

    public WeenieDesc(AC2Reader data) {
        packFlags = data.ReadEnum<PackFlag>();
        if (packFlags.HasFlag(PackFlag.MyPackageID)) {
            packageType = data.ReadEnum<PackageType>();
        }
        if (packFlags.HasFlag(PackFlag.EntityDID)) {
            entityDid = data.ReadDataId();
        }
        if (packFlags.HasFlag(PackFlag.Bitfield)) {
            bitfield = data.ReadEnum<Bitfield>();
        }
        if (packFlags.HasFlag(PackFlag.Name)) {
            name = new(data);
        }
        if (packFlags.HasFlag(PackFlag.PluralName)) {
            pluralName = new(data);
        }
        if (packFlags.HasFlag(PackFlag.IconID)) {
            iconDid = data.ReadDataId();
        }
        if (packFlags.HasFlag(PackFlag.ContainerID)) {
            containerId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.WielderID)) {
            wielderId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.MonarchID)) {
            monarchId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.OriginatorID)) {
            originatorId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.ClaimantID)) {
            claimantId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.KillerID)) {
            killerId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.PetSummonerID)) {
            petSummonerId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.Physics_Scale)) {
            scale = data.ReadSingle();
        }
        if (packFlags.HasFlag(PackFlag.Quantity)) {
            quantity = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.Value)) {
            value = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.FactionType)) {
            factionType = data.ReadEnum<FactionType>();
        }
        if (packFlags.HasFlag(PackFlag.PKAlwaysTruePermissions)) {
            pkAlwaysTruePermissions = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.PKAlwaysFalsePermissions)) {
            pkAlwaysFalsePermissions = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.PhysicsTypeLowDWord)) {
            physicsTypeLow = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.PhysicsTypeHighDWord)) {
            physicsTypeHigh = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.MovementEtherealLowDWord)) {
            movementEtherealLow = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.MovementEtherealHighDWord)) {
            movementEtherealHigh = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.PlacementEtherealLowDWord)) {
            placementEtherealLow = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.PlacementEtherealHighDWord)) {
            placementEtherealHigh = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.Durability_CurrentLevel)) {
            durabilityCurrentLevel = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.Durability_MaxLevel)) {
            durabilityMaxLevel = data.ReadInt32();
        }
    }

    public void write(AC2Writer data) {
        packFlags = 0;
        if (packageType != default) packFlags |= PackFlag.MyPackageID;
        if (entityDid != default) packFlags |= PackFlag.EntityDID;
        if (bitfield != default) packFlags |= PackFlag.Bitfield;
        if (name != default) packFlags |= PackFlag.Name;
        if (pluralName != default) packFlags |= PackFlag.PluralName;
        if (iconDid != default) packFlags |= PackFlag.IconID;
        if (containerId != default) packFlags |= PackFlag.ContainerID;
        if (wielderId != default) packFlags |= PackFlag.WielderID;
        if (monarchId != default) packFlags |= PackFlag.MonarchID;
        if (originatorId != default) packFlags |= PackFlag.OriginatorID;
        if (claimantId != default) packFlags |= PackFlag.ClaimantID;
        if (killerId != default) packFlags |= PackFlag.KillerID;
        if (petSummonerId != default) packFlags |= PackFlag.PetSummonerID;
        if (scale != default) packFlags |= PackFlag.Physics_Scale;
        if (quantity != default) packFlags |= PackFlag.Quantity;
        if (value != default) packFlags |= PackFlag.Value;
        if (factionType != default) packFlags |= PackFlag.FactionType;
        if (pkAlwaysTruePermissions != default) packFlags |= PackFlag.PKAlwaysTruePermissions;
        if (pkAlwaysFalsePermissions != default) packFlags |= PackFlag.PKAlwaysFalsePermissions;
        if (physicsTypeLow != default) packFlags |= PackFlag.PhysicsTypeLowDWord;
        if (physicsTypeHigh != default) packFlags |= PackFlag.PhysicsTypeHighDWord;
        if (movementEtherealLow != default) packFlags |= PackFlag.MovementEtherealLowDWord;
        if (movementEtherealHigh != default) packFlags |= PackFlag.MovementEtherealHighDWord;
        if (placementEtherealLow != default) packFlags |= PackFlag.PlacementEtherealLowDWord;
        if (placementEtherealHigh != default) packFlags |= PackFlag.PlacementEtherealHighDWord;
        if (durabilityCurrentLevel != default) packFlags |= PackFlag.Durability_CurrentLevel;
        if (durabilityMaxLevel != default) packFlags |= PackFlag.Durability_MaxLevel;

        data.WriteEnum(packFlags);
        if (packFlags.HasFlag(PackFlag.MyPackageID)) {
            data.WriteEnum(packageType);
        }
        if (packFlags.HasFlag(PackFlag.EntityDID)) {
            data.Write(entityDid);
        }
        if (packFlags.HasFlag(PackFlag.Bitfield)) {
            data.WriteEnum(bitfield);
        }
        if (packFlags.HasFlag(PackFlag.Name)) {
            name.write(data);
        }
        if (packFlags.HasFlag(PackFlag.PluralName)) {
            pluralName.write(data);
        }
        if (packFlags.HasFlag(PackFlag.IconID)) {
            data.Write(iconDid);
        }
        if (packFlags.HasFlag(PackFlag.ContainerID)) {
            data.Write(containerId);
        }
        if (packFlags.HasFlag(PackFlag.WielderID)) {
            data.Write(wielderId);
        }
        if (packFlags.HasFlag(PackFlag.MonarchID)) {
            data.Write(monarchId);
        }
        if (packFlags.HasFlag(PackFlag.OriginatorID)) {
            data.Write(originatorId);
        }
        if (packFlags.HasFlag(PackFlag.ClaimantID)) {
            data.Write(claimantId);
        }
        if (packFlags.HasFlag(PackFlag.KillerID)) {
            data.Write(killerId);
        }
        if (packFlags.HasFlag(PackFlag.PetSummonerID)) {
            data.Write(petSummonerId);
        }
        if (packFlags.HasFlag(PackFlag.Physics_Scale)) {
            data.Write(scale);
        }
        if (packFlags.HasFlag(PackFlag.Quantity)) {
            data.Write(quantity);
        }
        if (packFlags.HasFlag(PackFlag.Value)) {
            data.Write(value);
        }
        if (packFlags.HasFlag(PackFlag.FactionType)) {
            data.WriteEnum(factionType);
        }
        if (packFlags.HasFlag(PackFlag.PKAlwaysTruePermissions)) {
            data.Write(pkAlwaysTruePermissions);
        }
        if (packFlags.HasFlag(PackFlag.PKAlwaysFalsePermissions)) {
            data.Write(pkAlwaysFalsePermissions);
        }
        if (packFlags.HasFlag(PackFlag.PhysicsTypeLowDWord)) {
            data.Write(physicsTypeLow);
        }
        if (packFlags.HasFlag(PackFlag.PhysicsTypeHighDWord)) {
            data.Write(physicsTypeHigh);
        }
        if (packFlags.HasFlag(PackFlag.MovementEtherealLowDWord)) {
            data.Write(movementEtherealLow);
        }
        if (packFlags.HasFlag(PackFlag.MovementEtherealHighDWord)) {
            data.Write(movementEtherealHigh);
        }
        if (packFlags.HasFlag(PackFlag.PlacementEtherealLowDWord)) {
            data.Write(placementEtherealLow);
        }
        if (packFlags.HasFlag(PackFlag.PlacementEtherealHighDWord)) {
            data.Write(placementEtherealHigh);
        }
        if (packFlags.HasFlag(PackFlag.Durability_CurrentLevel)) {
            data.Write(durabilityCurrentLevel);
        }
        if (packFlags.HasFlag(PackFlag.Durability_MaxLevel)) {
            data.Write(durabilityMaxLevel);
        }
    }
}
