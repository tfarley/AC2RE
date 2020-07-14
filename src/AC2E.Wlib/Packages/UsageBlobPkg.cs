using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class UsageBlobPkg : IPackage {

        public PackageType packageType => PackageType.UsageBlob;

        public StringInfoPkg m_criticalSuccessMessage;
        public StringInfoPkg m_successMessage;
        public uint m_userBehaviorRepeatCount;
        public float m_userBehaviorTimeScale;
        public uint m_userBehavior;
        public bool m_userBehaviorFadeChildren;

        public UsageBlobPkg() {

        }

        public UsageBlobPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            data.ReadPkgRef<StringInfoPkg>(v => m_criticalSuccessMessage = v, resolvers);
            data.ReadPkgRef<StringInfoPkg>(v => m_successMessage = v, resolvers);
            m_userBehaviorRepeatCount = data.ReadUInt32();
            m_userBehaviorTimeScale = data.ReadSingle();
            m_userBehavior = data.ReadUInt32();
            m_userBehaviorFadeChildren = data.ReadUInt32() != 0;
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_criticalSuccessMessage, references);
            data.Write(m_successMessage, references);
            data.Write(m_userBehaviorRepeatCount);
            data.Write(m_userBehaviorTimeScale);
            data.Write(m_userBehavior);
            data.Write(m_userBehaviorFadeChildren ? (uint)1 : (uint)0);
        }
    }
}
