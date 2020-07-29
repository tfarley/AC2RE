namespace AC2E.Def {

    public class Trade : IPackage {

        public PackageType packageType => PackageType.Trade;

        public InstanceIdAHash slaveItemIdToAmount; // m_slave_table
        public InstanceId masterId; // m_master
        public InstanceId slaveId; // m_slave
        public bool masterAccepted; // m_master_accepted
        public bool slaveAccepted; // m_slave_accepted
        public uint status; // m_status
        public InstanceIdAHash masterItemIdToAmount; // m_master_table

        public Trade() {

        }

        public Trade(AC2Reader data) {
            data.ReadPkg<LAHash>(v => slaveItemIdToAmount = new InstanceIdAHash(v));
            masterId = data.ReadInstanceId();
            slaveId = data.ReadInstanceId();
            masterAccepted = data.ReadBoolean();
            slaveAccepted = data.ReadBoolean();
            status = data.ReadUInt32();
            data.ReadPkg<LAHash>(v => masterItemIdToAmount = new InstanceIdAHash(v));
        }

        public void write(AC2Writer data) {
            data.WritePkg(slaveItemIdToAmount);
            data.Write(masterId);
            data.Write(slaveId);
            data.Write(masterAccepted);
            data.Write(slaveAccepted);
            data.Write(status);
            data.WritePkg(masterItemIdToAmount);
        }
    }
}
