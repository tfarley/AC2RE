namespace AC2E.Def {

    public class EffectRegistry : IPackage {

        public PackageType packageType => PackageType.EffectRegistry;

        public AAHash qualitiesModifiedCount; // m_qualitiesModifiedCount
        public AAHash appliedFx; // m_appliedFX
        public EffectRegistry baseEffectRegistry; // m_baseEffectRegistry
        public uint effectIdCounter; // m_uiEffectIDCounter
        public ARHash<EffectRecord> effectInfo; // m_effectInfo
        public double lastPulseTime; // m_ttLastPulse
        public AList equipperEffectIds; // m_listEquipperEffectEids
        public AList acquirerEffectIds; // m_listAcquirerEffectEids
        public uint flags; // m_flags
        public AHashSet trackedEffects; // m_setTrackedEffects
        public AAHash topEffects; // m_topEffects
        public AAMultiHash effectCategorizationTable; // m_effectCategorizationTable
        public AAHash appliedAppearances; // m_appliedAppearances

        public EffectRegistry() {

        }

        public EffectRegistry(AC2Reader data) {
            data.ReadPkg<AAHash>(v => qualitiesModifiedCount = v);
            data.ReadPkg<AAHash>(v => appliedFx = v);
            data.ReadPkg<EffectRegistry>(v => baseEffectRegistry = v);
            effectIdCounter = data.ReadUInt32();
            data.ReadPkg<ARHash<IPackage>>(v => effectInfo = v.to<EffectRecord>());
            lastPulseTime = data.ReadDouble();
            data.ReadPkg<AList>(v => equipperEffectIds = v);
            data.ReadPkg<AList>(v => acquirerEffectIds = v);
            flags = data.ReadUInt32();
            data.ReadPkg<AHashSet>(v => trackedEffects = v);
            data.ReadPkg<AAHash>(v => topEffects = v);
            data.ReadPkg<AAMultiHash>(v => effectCategorizationTable = v);
            data.ReadPkg<AAHash>(v => appliedAppearances = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(qualitiesModifiedCount);
            data.WritePkg(appliedFx);
            data.WritePkg(baseEffectRegistry);
            data.Write(effectIdCounter);
            data.WritePkg(effectInfo);
            data.Write(lastPulseTime);
            data.WritePkg(equipperEffectIds);
            data.WritePkg(acquirerEffectIds);
            data.Write(flags);
            data.WritePkg(trackedEffects);
            data.WritePkg(topEffects);
            data.WritePkg(effectCategorizationTable);
            data.WritePkg(appliedAppearances);
        }
    }
}
