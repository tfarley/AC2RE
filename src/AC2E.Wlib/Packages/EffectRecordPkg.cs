using AC2E.Dat;
using AC2E.Def;
using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class EffectRecordPkg : IPackage {

        public PackageType packageType => PackageType.EffectRecord;

        public double m_timeDemotedFromTopLevel;
        public double m_timeCast;
        public InstanceId m_iidCaster;
        public float m_ttTimeout;
        public float m_fApp;
        public float m_fSpellcraft;
        public int m_iApp;
        public bool m_bPK;
        public PkgRef<IPackage> m_rApp;
        public double m_timePromotedToTopLevel;
        public PkgRef<SingletonPkg> m_effect;
        public InstanceId m_iidActingForWhom;
        public DataId m_didSkill;
        public InstanceId m_iidFromItem;
        public uint m_flags;
        public uint m_uiDurabilityLevel;
        public uint m_relatedEID;
        public uint m_effectID;
        public uint m_categories;
        public uint m_uiMaxDurabilityLevel;

        public EffectRecordPkg() {

        }

        public EffectRecordPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            m_timeDemotedFromTopLevel = data.ReadDouble();
            m_timeCast = data.ReadDouble();
            m_iidCaster = data.ReadInstanceId();
            m_ttTimeout = data.ReadSingle();
            m_fApp = data.ReadSingle();
            m_fSpellcraft = data.ReadSingle();
            m_iApp = data.ReadInt32();
            m_bPK = data.ReadUInt32() != 0;
            m_rApp = data.ReadPkgRef<IPackage>(resolvers);
            m_timePromotedToTopLevel = data.ReadDouble();
            m_effect = data.ReadPkgRef<SingletonPkg>(resolvers);
            m_iidActingForWhom = data.ReadInstanceId();
            m_didSkill = data.ReadDataId();
            m_iidFromItem = data.ReadInstanceId();
            m_flags = data.ReadUInt32();
            m_uiDurabilityLevel = data.ReadUInt32();
            m_relatedEID = data.ReadUInt32();
            m_effectID = data.ReadUInt32();
            m_categories = data.ReadUInt32();
            m_uiMaxDurabilityLevel = data.ReadUInt32();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(m_timeDemotedFromTopLevel);
            data.Write(m_timeCast);
            data.Write(m_iidCaster);
            data.Write(m_ttTimeout);
            data.Write(m_fApp);
            data.Write(m_fSpellcraft);
            data.Write(m_iApp);
            data.Write(m_bPK ? (uint)1 : (uint)0);
            data.Write(m_rApp, references);
            data.Write(m_timePromotedToTopLevel);
            data.Write(m_effect, references);
            data.Write(m_iidActingForWhom);
            data.Write(m_didSkill);
            data.Write(m_iidFromItem);
            data.Write(m_flags);
            data.Write(m_uiDurabilityLevel);
            data.Write(m_relatedEID);
            data.Write(m_effectID);
            data.Write(m_categories);
            data.Write(m_uiMaxDurabilityLevel);
        }
    }
}
