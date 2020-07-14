using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class EffectRegistryPkg : IPackage {

        public PackageType packageType => PackageType.EffectRegistry;

        public AAHashPkg m_qualitiesModifiedCount;
        public AAHashPkg m_appliedFX;
        public EffectRegistryPkg m_baseEffectRegistry;
        public uint m_uiEffectIDCounter;
        public ARHashPkg<EffectRecordPkg> m_effectInfo;
        public double m_ttLastPulse;
        public AListPkg m_listEquipperEffectEids;
        public AListPkg m_listAcquirerEffectEids;
        public uint m_flags;
        public AHashSetPkg m_setTrackedEffects;
        public AAHashPkg m_topEffects;
        public AAMultiHashPkg m_effectCategorizationTable;
        public AAHashPkg m_appliedAppearances;

        public EffectRegistryPkg() {

        }

        public EffectRegistryPkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<AAHashPkg>(v => m_qualitiesModifiedCount = v, registry);
            data.ReadPkgRef<AAHashPkg>(v => m_appliedFX = v, registry);
            data.ReadPkgRef<EffectRegistryPkg>(v => m_baseEffectRegistry = v, registry);
            m_uiEffectIDCounter = data.ReadUInt32();
            data.ReadPkgRef<ARHashPkg<IPackage>>(v => m_effectInfo = v.to<EffectRecordPkg>(), registry);
            m_ttLastPulse = data.ReadDouble();
            data.ReadPkgRef<AListPkg>(v => m_listEquipperEffectEids = v, registry);
            data.ReadPkgRef<AListPkg>(v => m_listAcquirerEffectEids = v, registry);
            m_flags = data.ReadUInt32();
            data.ReadPkgRef<AHashSetPkg>(v => m_setTrackedEffects = v, registry);
            data.ReadPkgRef<AAHashPkg>(v => m_topEffects = v, registry);
            data.ReadPkgRef<AAMultiHashPkg>(v => m_effectCategorizationTable = v, registry);
            data.ReadPkgRef<AAHashPkg>(v => m_appliedAppearances = v, registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_qualitiesModifiedCount, registry);
            data.Write(m_appliedFX, registry);
            data.Write(m_baseEffectRegistry, registry);
            data.Write(m_uiEffectIDCounter);
            data.Write(m_effectInfo, registry);
            data.Write(m_ttLastPulse);
            data.Write(m_listEquipperEffectEids, registry);
            data.Write(m_listAcquirerEffectEids, registry);
            data.Write(m_flags);
            data.Write(m_setTrackedEffects, registry);
            data.Write(m_topEffects, registry);
            data.Write(m_effectCategorizationTable, registry);
            data.Write(m_appliedAppearances, registry);
        }
    }
}
