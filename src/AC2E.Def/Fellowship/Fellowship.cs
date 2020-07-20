namespace AC2E.Def {

    public class Fellowship : IPackage {

        public PackageType packageType => PackageType.Fellow;

        public InstanceId m_lastClaimant;
        public uint m_flags;
        public uint m_chatRoomID;
        public InstanceIdRHash<Fellow> m_table;
        public InstanceId m_leader;
        public WPString m_name;

        public Fellowship() {

        }

        public Fellowship(AC2Reader data) {
            m_lastClaimant = data.ReadInstanceId();
            m_flags = data.ReadUInt32();
            m_chatRoomID = data.ReadUInt32();
            data.ReadPkg<LRHash<IPackage>>(v => m_table = new InstanceIdRHash<Fellow>(v.to<Fellow>()));
            m_leader = data.ReadInstanceId();
            data.ReadPkg<WPString>(v => m_name = v);
        }

        public void write(AC2Writer data) {
            data.Write(m_lastClaimant);
            data.Write(m_flags);
            data.Write(m_chatRoomID);
            data.WritePkg(m_table);
            data.Write(m_leader);
            data.WritePkg(m_name);
        }
    }
}
