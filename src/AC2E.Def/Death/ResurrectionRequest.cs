namespace AC2E.Def {

    public class ResurrectionRequest : IPackage {

        public PackageType packageType => PackageType.ResurrectionRequest;

        public InstanceId m_rezzerID;
        public StringInfo m_rezzerName;
        public float m_focusLossMod;
        public uint m_fx;

        public ResurrectionRequest() {

        }

        public ResurrectionRequest(AC2Reader data) {
            m_rezzerID = data.ReadInstanceId();
            data.ReadPkg<StringInfo>(v => m_rezzerName = v);
            m_focusLossMod = data.ReadSingle();
            m_fx = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(m_rezzerID);
            data.WritePkg(m_rezzerName);
            data.Write(m_focusLossMod);
            data.Write(m_fx);
        }
    }
}
