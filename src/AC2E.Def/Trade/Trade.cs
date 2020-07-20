namespace AC2E.Def {

    public class Trade : IPackage {

        public PackageType packageType => PackageType.Trade;

        public InstanceIdAHash m_slave_table;
        public InstanceId m_master;
        public InstanceId m_slave;
        public bool m_master_accepted;
        public bool m_slave_accepted;
        public uint m_status;
        public InstanceIdAHash m_master_table;

        public Trade() {

        }

        public Trade(AC2Reader data) {
            data.ReadPkg<LAHash>(v => m_slave_table = new InstanceIdAHash(v));
            m_master = data.ReadInstanceId();
            m_slave = data.ReadInstanceId();
            m_master_accepted = data.ReadBoolean();
            m_slave_accepted = data.ReadBoolean();
            m_status = data.ReadUInt32();
            data.ReadPkg<LAHash>(v => m_master_table = new InstanceIdAHash(v));
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_slave_table);
            data.Write(m_master);
            data.Write(m_slave);
            data.Write(m_master_accepted);
            data.Write(m_slave_accepted);
            data.Write(m_status);
            data.WritePkg(m_master_table);
        }
    }
}
