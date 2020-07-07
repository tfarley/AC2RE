using AC2E.Dat;
using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class EffectRecordPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.EffectRecord;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

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
        public EffectPkg m_effect;
        public InstanceId m_iidActingForWhom;
        public DataId m_didSkill;
        public InstanceId m_iidFromItem;
        public uint m_flags;
        public uint m_uiDurabilityLevel;
        public uint m_relatedEID;
        public uint m_effectID;
        public uint m_categories;
        public uint m_uiMaxDurabilityLevel;

        public void write(BinaryWriter data, List<IPackage> references) {
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
