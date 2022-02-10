using System;

namespace AC2RE.Definitions;

public class PKStatus : IHeapObject {

    public PackageType packageType => PackageType.PKStatus;

    // WLib PKStatus
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsAgentDead = 1 << 0, // IsAgentDead 0x00000001
    }

    // WLib PKStatus
    [Flags]
    public enum Type : uint {
        None = 0,
        IsPlayer = 1 << 0, // IsPlayer 0x00000001
        IsAdmin = 1 << 1, // IsAdmin 0x00000002
        IsCreature = 1 << 2, // IsCreature 0x00000004
        IsNPC = 1 << 3, // IsNPC 0x00000008
        IsItem = 1 << 4, // IsItem 0x00000010
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
