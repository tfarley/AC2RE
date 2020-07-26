namespace AC2E.Def {

    public class ResurrectionRequest : IPackage {

        public PackageType packageType => PackageType.ResurrectionRequest;

        public InstanceId rezzerId; // m_rezzerID
        public StringInfo rezzerName; // m_rezzerName
        public float focusLossMod; // m_focusLossMod
        public uint fx; // m_fx

        public ResurrectionRequest() {

        }

        public ResurrectionRequest(AC2Reader data) {
            rezzerId = data.ReadInstanceId();
            data.ReadPkg<StringInfo>(v => rezzerName = v);
            focusLossMod = data.ReadSingle();
            fx = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(rezzerId);
            data.WritePkg(rezzerName);
            data.Write(focusLossMod);
            data.Write(fx);
        }
    }
}
