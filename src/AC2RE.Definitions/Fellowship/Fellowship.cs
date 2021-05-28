using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Fellowship : IPackage {

        public PackageType packageType => PackageType.Fellow;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            SOCIAL = 1 << 0, // 0x00000001, Fellowship::IsSocial
            LOOT_SHARING = 1 << 1, // 0x00000002, Fellowship::IsLootSharing
            LOOT_ROTATING = 1 << 2, // 0x00000004, Fellowship::IsLootRotating
            LOOT_GROUP_ROTATING = 1 << 3, // 0x00000008, Fellowship::IsLootGroupRotating
            LOOT_RANDOM = 1 << 4, // 0x00000010, Fellowship::IsLootRandom
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
            data.ReadPkg<LRHash>(v => fellowTable = v.to<InstanceId, Fellow>());
            leaderId = data.ReadInstanceId();
            data.ReadPkg<WPString>(v => name = v);
        }

        public void write(AC2Writer data) {
            data.Write(lastClaimantId);
            data.Write((uint)flags);
            data.Write(chatRoomId);
            data.WritePkg(LRHash.from(fellowTable));
            data.Write(leaderId);
            data.WritePkg(name);
        }
    }
}
