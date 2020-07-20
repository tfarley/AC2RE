namespace AC2E.Def {

    public class EffectRegistry : IPackage {

        public PackageType packageType => PackageType.EffectRegistry;

        public AAHash m_qualitiesModifiedCount;
        public AAHash m_appliedFX;
        public EffectRegistry m_baseEffectRegistry;
        public uint m_uiEffectIDCounter;
        public ARHash<EffectRecord> m_effectInfo;
        public double m_ttLastPulse;
        public AList m_listEquipperEffectEids;
        public AList m_listAcquirerEffectEids;
        public uint m_flags;
        public AHashSet m_setTrackedEffects;
        public AAHash m_topEffects;
        public AAMultiHash m_effectCategorizationTable;
        public AAHash m_appliedAppearances;

        public EffectRegistry() {

        }

        public EffectRegistry(AC2Reader data) {
            data.ReadPkg<AAHash>(v => m_qualitiesModifiedCount = v);
            data.ReadPkg<AAHash>(v => m_appliedFX = v);
            data.ReadPkg<EffectRegistry>(v => m_baseEffectRegistry = v);
            m_uiEffectIDCounter = data.ReadUInt32();
            data.ReadPkg<ARHash<IPackage>>(v => m_effectInfo = v.to<EffectRecord>());
            m_ttLastPulse = data.ReadDouble();
            data.ReadPkg<AList>(v => m_listEquipperEffectEids = v);
            data.ReadPkg<AList>(v => m_listAcquirerEffectEids = v);
            m_flags = data.ReadUInt32();
            data.ReadPkg<AHashSet>(v => m_setTrackedEffects = v);
            data.ReadPkg<AAHash>(v => m_topEffects = v);
            data.ReadPkg<AAMultiHash>(v => m_effectCategorizationTable = v);
            data.ReadPkg<AAHash>(v => m_appliedAppearances = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_qualitiesModifiedCount);
            data.WritePkg(m_appliedFX);
            data.WritePkg(m_baseEffectRegistry);
            data.Write(m_uiEffectIDCounter);
            data.WritePkg(m_effectInfo);
            data.Write(m_ttLastPulse);
            data.WritePkg(m_listEquipperEffectEids);
            data.WritePkg(m_listAcquirerEffectEids);
            data.Write(m_flags);
            data.WritePkg(m_setTrackedEffects);
            data.WritePkg(m_topEffects);
            data.WritePkg(m_effectCategorizationTable);
            data.WritePkg(m_appliedAppearances);
        }
    }
}
