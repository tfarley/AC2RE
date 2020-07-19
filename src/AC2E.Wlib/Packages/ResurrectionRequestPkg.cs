﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ResurrectionRequestPkg : IPackage {

        public PackageType packageType => PackageType.ResurrectionRequest;

        public InstanceId m_rezzerID;
        public StringInfo m_rezzerName;
        public float m_focusLossMod;
        public uint m_fx;

        public ResurrectionRequestPkg() {

        }

        public ResurrectionRequestPkg(BinaryReader data, PackageRegistry registry) {
            m_rezzerID = data.ReadInstanceId();
            data.ReadPkgRef<StringInfo>(v => m_rezzerName = v, registry);
            m_focusLossMod = data.ReadSingle();
            m_fx = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_rezzerID);
            data.Write(m_rezzerName, registry);
            data.Write(m_focusLossMod);
            data.Write(m_fx);
        }
    }
}
