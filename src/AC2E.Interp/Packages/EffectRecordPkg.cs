using AC2E.Def.Structs;
using AC2E.Interp.Extensions;
using System.IO;

namespace AC2E.Interp {

    public class EffectRecordPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.EffectRecord;
        public InterpReference reference => new InterpReference(InterpReference.Flag.LOADED | InterpReference.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }
        public IPackage[] references {
            get {
                return new IPackage[] {
                    m_rApp,
                    m_effect,
                };
            }
        }

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
        public IPackage m_effect;
        public InstanceId m_iidActingForWhom;
        public DataId m_didSkill;
        public InstanceId m_iidFromItem;
        public uint m_flags;
        public uint m_uiDurabilityLevel;
        public uint m_relatedEID;
        public uint m_effectID;
        public uint m_categories;
        public uint m_uiMaxDurabilityLevel;

        public void write(BinaryWriter data) {
            data.Write(m_timeDemotedFromTopLevel);
            data.Write(m_timeCast);
            data.Write(m_iidCaster);
            data.Write(m_ttTimeout);
            data.Write(m_fApp);
            data.Write(m_fSpellcraft);
            data.Write(m_iApp);
            data.Write(m_bPK ? (uint)1 : (uint)0);
            data.Write(m_rApp);
            data.Write(m_timePromotedToTopLevel);
            data.Write(m_effect);
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
