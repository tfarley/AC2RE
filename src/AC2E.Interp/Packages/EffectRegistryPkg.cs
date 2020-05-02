using AC2E.Interp.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class EffectRegistryPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.EffectRegistry;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public AAHashPkg m_qualitiesModifiedCount;
        public AAHashPkg m_appliedFX;
        public EffectRegistryPkg m_baseEffectRegistry;
        public uint m_uiEffectIDCounter;
        public ARHashPkg<IPackage> m_effectInfo; // TODO: EffectInfo pkg type?
        public double m_ttLastPulse;
        public AListPkg m_listEquipperEffectEids;
        public AListPkg m_listAcquirerEffectEids;
        public uint m_flags;
        public AHashSetPkg m_setTrackedEffects;
        public AAHashPkg m_topEffects;
        public AAMultiHashPkg m_effectCategorizationTable;
        public AAHashPkg m_appliedAppearances;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_qualitiesModifiedCount, references);
            data.Write(m_appliedFX, references);
            data.Write(m_baseEffectRegistry, references);
            data.Write(m_uiEffectIDCounter);
            data.Write(m_effectInfo, references);
            data.Write(m_ttLastPulse);
            data.Write(m_listEquipperEffectEids, references);
            data.Write(m_listAcquirerEffectEids, references);
            data.Write(m_flags);
            data.Write(m_setTrackedEffects, references);
            data.Write(m_topEffects, references);
            data.Write(m_effectCategorizationTable, references);
            data.Write(m_appliedAppearances, references);
        }
    }
}
