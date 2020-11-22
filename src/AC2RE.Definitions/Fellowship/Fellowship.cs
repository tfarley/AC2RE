using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Fellowship : IPackage {

        public PackageType packageType => PackageType.Fellow;

        public InstanceId lastClaimantId; // m_lastClaimant
        public uint flags; // m_flags
        public uint chatRoomId; // m_chatRoomID
        public Dictionary<InstanceId, Fellow> fellowTable; // m_table
        public InstanceId leaderId; // m_leader
        public WPString name; // m_name

        public Fellowship() {

        }

        public Fellowship(AC2Reader data) {
            lastClaimantId = data.ReadInstanceId();
            flags = data.ReadUInt32();
            chatRoomId = data.ReadUInt32();
            data.ReadPkg<LRHash>(v => fellowTable = v.to<InstanceId, Fellow>());
            leaderId = data.ReadInstanceId();
            data.ReadPkg<WPString>(v => name = v);
        }

        public void write(AC2Writer data) {
            data.Write(lastClaimantId);
            data.Write(flags);
            data.Write(chatRoomId);
            data.WritePkg(LRHash.from(fellowTable));
            data.Write(leaderId);
            data.WritePkg(name);
        }
    }
}
