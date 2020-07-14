using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class EffectRegistryPkg : IPackage {

        public PackageType packageType => PackageType.EffectRegistry;

        public PkgRef<AAHashPkg> m_qualitiesModifiedCount;
        public PkgRef<AAHashPkg> m_appliedFX;
        public PkgRef<EffectRegistryPkg> m_baseEffectRegistry;
        public uint m_uiEffectIDCounter;
        public PkgRef<ARHashPkg<EffectRecordPkg>> m_effectInfo;
        public double m_ttLastPulse;
        public PkgRef<AListPkg> m_listEquipperEffectEids;
        public PkgRef<AListPkg> m_listAcquirerEffectEids;
        public uint m_flags;
        public PkgRef<AHashSetPkg> m_setTrackedEffects;
        public PkgRef<AAHashPkg> m_topEffects;
        public PkgRef<AAMultiHashPkg> m_effectCategorizationTable;
        public PkgRef<AAHashPkg> m_appliedAppearances;

        public EffectRegistryPkg() {

        }

        public EffectRegistryPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            m_qualitiesModifiedCount = data.ReadPkgRef<AAHashPkg>(resolvers);
            m_appliedFX = data.ReadPkgRef<AAHashPkg>(resolvers);
            m_baseEffectRegistry = data.ReadPkgRef<EffectRegistryPkg>(resolvers);
            m_uiEffectIDCounter = data.ReadUInt32();
            m_effectInfo = data.ReadPkgRef<ARHashPkg<IPackage>, ARHashPkg<EffectRecordPkg>>(resolvers, v => v.to<EffectRecordPkg>());
            m_ttLastPulse = data.ReadDouble();
            m_listEquipperEffectEids = data.ReadPkgRef<AListPkg>(resolvers);
            m_listAcquirerEffectEids = data.ReadPkgRef<AListPkg>(resolvers);
            m_flags = data.ReadUInt32();
            m_setTrackedEffects = data.ReadPkgRef<AHashSetPkg>(resolvers);
            m_topEffects = data.ReadPkgRef<AAHashPkg>(resolvers);
            m_effectCategorizationTable = data.ReadPkgRef<AAMultiHashPkg>(resolvers);
            m_appliedAppearances = data.ReadPkgRef<AAHashPkg>(resolvers);
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
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
