using AC2E.Interp;
using System;
using System.Collections.Generic;
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

        public EffectRegistryPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            data.ReadPkgRef<AAHashPkg>(v => m_qualitiesModifiedCount = v, resolvers);
            data.ReadPkgRef<AAHashPkg>(v => m_appliedFX = v, resolvers);
            data.ReadPkgRef<EffectRegistryPkg>(v => m_baseEffectRegistry = v, resolvers);
            m_uiEffectIDCounter = data.ReadUInt32();
            data.ReadPkgRef<ARHashPkg<IPackage>>(v => m_effectInfo = v.to<EffectRecordPkg>(), resolvers);
            m_ttLastPulse = data.ReadDouble();
            data.ReadPkgRef<AListPkg>(v => m_listEquipperEffectEids = v, resolvers);
            data.ReadPkgRef<AListPkg>(v => m_listAcquirerEffectEids = v, resolvers);
            m_flags = data.ReadUInt32();
            data.ReadPkgRef<AHashSetPkg>(v => m_setTrackedEffects = v, resolvers);
            data.ReadPkgRef<AAHashPkg>(v => m_topEffects = v, resolvers);
            data.ReadPkgRef<AAMultiHashPkg>(v => m_effectCategorizationTable = v, resolvers);
            data.ReadPkgRef<AAHashPkg>(v => m_appliedAppearances = v, resolvers);
        }

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
