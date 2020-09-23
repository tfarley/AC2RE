using System;

namespace AC2E.Def {

    public class WeenieDesc {

        // Enum gmWDDataMask
        [Flags]
        public enum PackFlag : uint {
            UNDEF = 0,
            MY_PACKAGE_ID = 1 << 0, // 0x00000001
            BITFIELD = 1 << 1, // 0x00000002
            NAME = 1 << 2, // 0x00000004
            PLURAL_NAME = 1 << 3, // 0x00000008
            ICON_ID = 1 << 4, // 0x00000010
            CONTAINER_ID = 1 << 5, // 0x00000020
            WIELDER_ID = 1 << 6, // 0x00000040
            PHYSICS_SCALE = 1 << 7, // 0x00000080
            MONARCH_ID = 1 << 8, // 0x00000100
            ORIGINATOR_ID = 1 << 9, // 0x00000200
            OPEN = 1 << 10, // 0x00000400
            QUANTITY = 1 << 11, // 0x00000800
            VALUE = 1 << 12, // 0x00001000
            FACTION_TYPE = 1 << 13, // 0x00002000
            DEAD = 1 << 14, // 0x00004000
            PHYSICS_TYPE_LOW_DWORD = 1 << 15, // 0x00008000
            PHYSICS_TYPE_HIGH_DWORD = 1 << 16, // 0x00010000
            MOVEMENT_ETHEREAL_LOW_DWORD = 1 << 17, // 0x00020000
            MOVEMENT_ETHEREAL_HIGH_DWORD = 1 << 18, // 0x00040000
            PLACEMENT_ETHEREAL_LOW_DWORD = 1 << 19, // 0x00080000
            PLACEMENT_ETHEREAL_HIGH_DWORD = 1 << 20, // 0x00100000
            PK_ALWAYS_TRUE_PERMISSIONS = 1 << 21, // 0x00200000
            PK_ALWAYS_FALSE_PERMISSIONS = 1 << 22, // 0x00400000
            SELECTABLE = 1 << 23, // 0x00800000
            NO_DRAW = 1 << 24, // 0x01000000
            ENTITY_DID = 1 << 25, // 0x02000000
            PET_SUMMONER_ID = 1 << 26, // 0x04000000
            DURABILITY_CURRENT_LEVEL = 1 << 27, // 0x08000000
            DURABILITY_MAX_LEVEL = 1 << 28, // 0x10000000
            CLAIMANT_ID = 1 << 29, // 0x20000000
            KILLER_ID = 1 << 30, // 0x40000000
        }

        // Enum gmWeenieDesc::BitfieldIndex
        [Flags]
        public enum Bitfield : uint {
            UNDEF = 0,
            OPEN = 1 << 0, // 0x00000001
            SELECTABLE = 1 << 1, // 0x00000002
            NO_DRAW = 1 << 2, // 0x00000004
            DEAD = 1 << 3, // 0x00000008
        }

        public Bitfield bitfield; // _bitfield
        public PackageType packageType; // _my_pkgid
        public DataId entityDid; // m_entityDID
        public PackFlag packFlags; // _data_mask
        public StringInfo name; // _name
        public StringInfo pluralName; // _plural_name
        public DataId iconDid; // _icon // TODO: Guessed this is a DID
        public InstanceId containerId; // _container
        public InstanceId wielderId; // _wielder
        public InstanceId monarchId; // _monarch
        public InstanceId originatorId; // _originator
        public InstanceId claimantId; // _claimant
        public InstanceId killerId; // _killer
        public InstanceId petSummonerId; // _petSummoner
        public uint quantity; // _quantity // TODO: Many of these are signed?
        public uint value; // _value
        public FactionType factionType; // _factionType
        public bool pkAlwaysTrue; // _pkAlwaysTrue // TODO: Really a bool?
        public bool pkAlwaysFalse; // _pkAlwaysFalse // TODO: Really a bool?
        public uint physicsTypeLow; // _physicsTypeLowDWord // TODO: How to combine?
        public uint physicsTypeHigh; // _physicsTypeHighDWord // TODO: How to combine?
        public uint movementEtherealLow; // _movementEtherealLowDWord // TODO: How to combine?
        public uint movementEtherealHigh; // _movementEtherealHighDWord // TODO: How to combine?
        public uint placementEtherealLow; // _placementEtherealLowDWord // TODO: How to combine?
        public uint placementEtherealHigh; // _placementEtherealHighDWord // TODO: How to combine?
        public uint durabilityCurrentLevel; // _durability_CurrentLevel
        public uint durabilityMaxLevel; // _durability_MaxLevel
        public float scale; // _scale

        public WeenieDesc() {

        }

        public WeenieDesc(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.MY_PACKAGE_ID)) {
                packageType = (PackageType)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.ENTITY_DID)) {
                entityDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.BITFIELD)) {
                bitfield = (Bitfield)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.NAME)) {
                name = new StringInfo(data);
            }
            if (packFlags.HasFlag(PackFlag.PLURAL_NAME)) {
                pluralName = new StringInfo(data);
            }
            if (packFlags.HasFlag(PackFlag.ICON_ID)) {
                iconDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.CONTAINER_ID)) {
                containerId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.WIELDER_ID)) {
                wielderId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.MONARCH_ID)) {
                monarchId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.ORIGINATOR_ID)) {
                originatorId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.CLAIMANT_ID)) {
                claimantId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.KILLER_ID)) {
                killerId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.PET_SUMMONER_ID)) {
                petSummonerId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.PHYSICS_SCALE)) {
                scale = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.QUANTITY)) {
                quantity = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.VALUE)) {
                value = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.FACTION_TYPE)) {
                factionType = (FactionType)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.PK_ALWAYS_TRUE_PERMISSIONS)) {
                pkAlwaysTrue = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.PK_ALWAYS_FALSE_PERMISSIONS)) {
                pkAlwaysFalse = data.ReadBoolean();
            }
            if (packFlags.HasFlag(PackFlag.PHYSICS_TYPE_LOW_DWORD)) {
                physicsTypeLow = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.PHYSICS_TYPE_HIGH_DWORD)) {
                physicsTypeHigh = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD)) {
                movementEtherealLow = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD)) {
                movementEtherealHigh = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD)) {
                placementEtherealLow = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD)) {
                placementEtherealHigh = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.DURABILITY_CURRENT_LEVEL)) {
                durabilityCurrentLevel = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.DURABILITY_MAX_LEVEL)) {
                durabilityMaxLevel = data.ReadUInt32();
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.MY_PACKAGE_ID)) {
                data.Write((uint)packageType);
            }
            if (packFlags.HasFlag(PackFlag.ENTITY_DID)) {
                data.Write(entityDid);
            }
            if (packFlags.HasFlag(PackFlag.BITFIELD)) {
                data.Write((uint)bitfield);
            }
            if (packFlags.HasFlag(PackFlag.NAME)) {
                name.write(data);
            }
            if (packFlags.HasFlag(PackFlag.PLURAL_NAME)) {
                pluralName.write(data);
            }
            if (packFlags.HasFlag(PackFlag.ICON_ID)) {
                data.Write(iconDid);
            }
            if (packFlags.HasFlag(PackFlag.CONTAINER_ID)) {
                data.Write(containerId);
            }
            if (packFlags.HasFlag(PackFlag.WIELDER_ID)) {
                data.Write(wielderId);
            }
            if (packFlags.HasFlag(PackFlag.MONARCH_ID)) {
                data.Write(monarchId);
            }
            if (packFlags.HasFlag(PackFlag.ORIGINATOR_ID)) {
                data.Write(originatorId);
            }
            if (packFlags.HasFlag(PackFlag.CLAIMANT_ID)) {
                data.Write(claimantId);
            }
            if (packFlags.HasFlag(PackFlag.KILLER_ID)) {
                data.Write(killerId);
            }
            if (packFlags.HasFlag(PackFlag.PET_SUMMONER_ID)) {
                data.Write(petSummonerId);
            }
            if (packFlags.HasFlag(PackFlag.PHYSICS_SCALE)) {
                data.Write(scale);
            }
            if (packFlags.HasFlag(PackFlag.QUANTITY)) {
                data.Write(quantity);
            }
            if (packFlags.HasFlag(PackFlag.VALUE)) {
                data.Write(value);
            }
            if (packFlags.HasFlag(PackFlag.FACTION_TYPE)) {
                data.Write((uint)factionType);
            }
            if (packFlags.HasFlag(PackFlag.PK_ALWAYS_TRUE_PERMISSIONS)) {
                data.Write(pkAlwaysTrue);
            }
            if (packFlags.HasFlag(PackFlag.PK_ALWAYS_FALSE_PERMISSIONS)) {
                data.Write(pkAlwaysFalse);
            }
            if (packFlags.HasFlag(PackFlag.PHYSICS_TYPE_LOW_DWORD)) {
                data.Write(physicsTypeLow);
            }
            if (packFlags.HasFlag(PackFlag.PHYSICS_TYPE_HIGH_DWORD)) {
                data.Write(physicsTypeHigh);
            }
            if (packFlags.HasFlag(PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD)) {
                data.Write(movementEtherealLow);
            }
            if (packFlags.HasFlag(PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD)) {
                data.Write(movementEtherealHigh);
            }
            if (packFlags.HasFlag(PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD)) {
                data.Write(placementEtherealLow);
            }
            if (packFlags.HasFlag(PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD)) {
                data.Write(placementEtherealHigh);
            }
            if (packFlags.HasFlag(PackFlag.DURABILITY_CURRENT_LEVEL)) {
                data.Write(durabilityCurrentLevel);
            }
            if (packFlags.HasFlag(PackFlag.DURABILITY_MAX_LEVEL)) {
                data.Write(durabilityMaxLevel);
            }
        }
    }
}
