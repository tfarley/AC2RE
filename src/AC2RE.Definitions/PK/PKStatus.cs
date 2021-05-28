using System;

namespace AC2RE.Definitions {

    public class PKStatus : IPackage {

        public PackageType packageType => PackageType.PKStatus;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            AGENT_DEAD = 1 << 0, // 0x00000001, PKStatus::IsAgentDead
        }

        // WLib
        [Flags]
        public enum Type : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            PLAYER = 1 << 0, // 0x00000001, PKStatus::IsPlayer
            ADMIN = 1 << 1, // 0x00000002, PKStatus::IsAdmin
            CREATURE = 1 << 2, // 0x00000004, PKStatus::IsCreature
            NPC = 1 << 3, // 0x00000008, PKStatus::IsNPC
            ITEM = 1 << 4, // 0x00000010, PKStatus::IsItem
        }

        public FactionStatus factionStatus; // m_factionStatus
        public FactionType faction; // m_faction
        public uint permAlwaysTrue; // m_permAlwaysTrue
        public Flag flags; // m_flags
        public InstanceId petMasterId; // m_petMaster
        public Type pkType; // m_pkType
        public uint permAlwaysFalse; // m_permAlwaysFalse
        public InstanceId id; // m_iid
        public ErrorType errorTypeInvulnerability; // m_etInvulnerability

        public PKStatus(AC2Reader data) {
            factionStatus = (FactionStatus)data.ReadUInt32();
            faction = (FactionType)data.ReadUInt32();
            permAlwaysTrue = data.ReadUInt32();
            flags = (Flag)data.ReadUInt32();
            petMasterId = data.ReadInstanceId();
            pkType = (Type)data.ReadUInt32();
            permAlwaysFalse = data.ReadUInt32();
            id = data.ReadInstanceId();
            errorTypeInvulnerability = (ErrorType)data.ReadUInt32();
        }
    }
}
