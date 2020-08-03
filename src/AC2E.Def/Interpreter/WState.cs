namespace AC2E.Def {

    public class WState {

        public DataId did; // m_DID
        public byte[] packageStateData; // m_pData // TODO: What data format? How to make this usable?
        public PackageType packageType; // m_pid

        public WState(AC2Reader data) {
            did = data.ReadDataId();
            uint stateDataLen = data.ReadUInt32();
            packageStateData = data.ReadBytes((int)stateDataLen);
            packageType = (PackageType)data.ReadUInt32();
        }
    }
}
