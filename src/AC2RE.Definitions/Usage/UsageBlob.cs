namespace AC2RE.Definitions {

    public class UsageBlob : IPackage {

        public virtual PackageType packageType => PackageType.UsageBlob;

        public StringInfo criticalSuccessText; // m_criticalSuccessMessage
        public StringInfo successText; // m_successMessage
        public uint userBehaviorRepeatCount; // m_userBehaviorRepeatCount
        public float userBehaviorTimeScale; // m_userBehaviorTimeScale
        public uint userBehavior; // m_userBehavior
        public bool userBehaviorFadeChildren; // m_userBehaviorFadeChildren

        public UsageBlob() {

        }

        public UsageBlob(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => criticalSuccessText = v);
            data.ReadPkg<StringInfo>(v => successText = v);
            userBehaviorRepeatCount = data.ReadUInt32();
            userBehaviorTimeScale = data.ReadSingle();
            userBehavior = data.ReadUInt32();
            userBehaviorFadeChildren = data.ReadBoolean();
        }

        public virtual void write(AC2Writer data) {
            data.WritePkg(criticalSuccessText);
            data.WritePkg(successText);
            data.Write(userBehaviorRepeatCount);
            data.Write(userBehaviorTimeScale);
            data.Write(userBehavior);
            data.Write(userBehaviorFadeChildren);
        }
    }
}
