using AC2E.Def;
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
        public IPackage m_rApp;
        public double m_timePromotedToTopLevel;
        public SingletonPkg m_effect;
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

        public EffectRecordPkg(BinaryReader data, PackageRegistry registry) {
            m_timeDemotedFromTopLevel = data.ReadDouble();
            m_timeCast = data.ReadDouble();
            m_iidCaster = data.ReadInstanceId();
            m_ttTimeout = data.ReadSingle();
            m_fApp = data.ReadSingle();
            m_fSpellcraft = data.ReadSingle();
            m_iApp = data.ReadInt32();
            m_bPK = data.ReadUInt32() != 0;
            data.ReadPkgRef<IPackage>(v => m_rApp = v, registry);
            m_timePromotedToTopLevel = data.ReadDouble();
            data.ReadPkgRef<SingletonPkg>(v => m_effect = v, registry);
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

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_timeDemotedFromTopLevel);
            data.Write(m_timeCast);
            data.Write(m_iidCaster);
            data.Write(m_ttTimeout);
            data.Write(m_fApp);
            data.Write(m_fSpellcraft);
            data.Write(m_iApp);
            data.Write(m_bPK ? (uint)1 : (uint)0);
            data.Write(m_rApp, registry);
            data.Write(m_timePromotedToTopLevel);
            data.Write(m_effect, registry);
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
