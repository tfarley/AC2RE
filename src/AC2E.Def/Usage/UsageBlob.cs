﻿namespace AC2E.Def {

    public class UsageBlob : IPackage {

        public PackageType packageType => PackageType.UsageBlob;

        public StringInfo m_criticalSuccessMessage;
        public StringInfo m_successMessage;
        public uint m_userBehaviorRepeatCount;
        public float m_userBehaviorTimeScale;
        public uint m_userBehavior;
        public bool m_userBehaviorFadeChildren;

        public UsageBlob() {

        }

        public UsageBlob(AC2Reader data, PackageRegistry registry) {
            data.ReadPkgRef<StringInfo>(v => m_criticalSuccessMessage = v, registry);
            data.ReadPkgRef<StringInfo>(v => m_successMessage = v, registry);
            m_userBehaviorRepeatCount = data.ReadUInt32();
            m_userBehaviorTimeScale = data.ReadSingle();
            m_userBehavior = data.ReadUInt32();
            m_userBehaviorFadeChildren = data.ReadBoolean();
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_criticalSuccessMessage, registry);
            data.Write(m_successMessage, registry);
            data.Write(m_userBehaviorRepeatCount);
            data.Write(m_userBehaviorTimeScale);
            data.Write(m_userBehavior);
            data.Write(m_userBehaviorFadeChildren);
        }
    }
}
