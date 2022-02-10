using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Fellowship : IHeapObject {

    public PackageType packageType => PackageType.Fellow;

    // WLib Fellowship
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsSocial = 1 << 0, // IsSocial 0x00000001
        IsLootSharing = 1 << 1, // IsLootSharing 0x00000002
        IsLootRotating = 1 << 2, // IsLootRotating 0x00000004
        IsLootGroupRotating = 1 << 3, // IsLootGroupRotating 0x00000008
        IsLootRandom = 1 << 4, // IsLootRandom 0x00000010
    }

    public InstanceId lastClaimantId; // m_lastClaimant
    public Flag flags; // m_flags
    public uint chatRoomId; // m_chatRoomID
    public Dictionary<InstanceId, Fellow> fellowTable; // m_table
    public InstanceId leaderId; // m_leader
    public WPString name; // m_name

    public Fellowship() {

    }

    public Fellowship(AC2Reader data) {
        lastClaimantId = data.ReadInstanceId();
        flags = (Flag)data.ReadUInt32();
        chatRoomId = data.ReadUInt32();
        data.ReadHO<LRHash>(v => fellowTable = v.to<InstanceId, Fellow>());
        leaderId = data.ReadInstanceId();
        data.ReadHO<WPString>(v => name = v);
    }

    public void write(AC2Writer data) {
        data.Write(lastClaimantId);
        data.Write((uint)flags);
        data.Write(chatRoomId);
        data.WriteHO(LRHash.from(fellowTable));
        data.Write(leaderId);
        data.WriteHO(name);
    }
}
