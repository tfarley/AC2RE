using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class TradePkg : IPackage {

        public PackageType packageType => PackageType.Trade;

        public InstanceIdAHash m_slave_table;
        public InstanceId m_master;
        public InstanceId m_slave;
        public bool m_master_accepted;
        public bool m_slave_accepted;
        public uint m_status;
        public InstanceIdAHash m_master_table;

        public TradePkg() {

        }

        public TradePkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<LAHash>(v => m_slave_table = new InstanceIdAHash(v), registry);
            m_master = data.ReadInstanceId();
            m_slave = data.ReadInstanceId();
            m_master_accepted = data.ReadUInt32() != 0;
            m_slave_accepted = data.ReadUInt32() != 0;
            m_status = data.ReadUInt32();
            data.ReadPkgRef<LAHash>(v => m_master_table = new InstanceIdAHash(v), registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_slave_table, registry);
            data.Write(m_master);
            data.Write(m_slave);
            data.Write(m_master_accepted ? (uint)1 : (uint)0);
            data.Write(m_slave_accepted ? (uint)1 : (uint)0);
            data.Write(m_status);
            data.Write(m_master_table, registry);
        }
    }
}
