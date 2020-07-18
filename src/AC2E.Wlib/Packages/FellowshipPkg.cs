﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class FellowshipPkg : IPackage {

        public PackageType packageType => PackageType.Fellow;

        public InstanceId m_lastClaimant;
        public uint m_flags;
        public uint m_chatRoomID;
        public InstanceIdRHash<FellowPkg> m_table;
        public InstanceId m_leader;
        public WPString m_name;

        public FellowshipPkg() {

        }

        public FellowshipPkg(BinaryReader data, PackageRegistry registry) {
            m_lastClaimant = data.ReadInstanceId();
            m_flags = data.ReadUInt32();
            m_chatRoomID = data.ReadUInt32();
            data.ReadPkgRef<LRHash<IPackage>>(v => m_table = new InstanceIdRHash<FellowPkg>(v.to<FellowPkg>()), registry);
            m_leader = data.ReadInstanceId();
            data.ReadPkgRef<WPString>(v => m_name = v, registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_lastClaimant);
            data.Write(m_flags);
            data.Write(m_chatRoomID);
            data.Write(m_table, registry);
            data.Write(m_leader);
            data.Write(m_name, registry);
        }
    }
}
