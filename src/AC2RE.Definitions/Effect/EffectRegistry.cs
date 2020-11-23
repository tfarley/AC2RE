﻿using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class EffectRegistry : IPackage {

        public PackageType packageType => PackageType.EffectRegistry;

        public Dictionary<uint, uint> qualitiesModifiedCount; // m_qualitiesModifiedCount
        public Dictionary<uint, uint> appliedFx; // m_appliedFX
        public EffectRegistry baseEffectRegistry; // m_baseEffectRegistry
        public uint effectIdCounter; // m_uiEffectIDCounter
        public Dictionary<uint, EffectRecord> effectInfo; // m_effectInfo
        public double lastPulseTime; // m_ttLastPulse
        public List<uint> equipperEffectIds; // m_listEquipperEffectEids
        public List<uint> acquirerEffectIds; // m_listAcquirerEffectEids
        public uint flags; // m_flags
        public HashSet<uint> trackedEffects; // m_setTrackedEffects
        public Dictionary<uint, uint> topEffects; // m_topEffects
        public Dictionary<uint, List<uint>> effectCategorizationTable; // m_effectCategorizationTable
        public Dictionary<uint, uint> appliedAppearances; // m_appliedAppearances

        public EffectRegistry() {

        }

        public EffectRegistry(AC2Reader data) {
            data.ReadPkg<AAHash>(v => qualitiesModifiedCount = v);
            data.ReadPkg<AAHash>(v => appliedFx = v);
            data.ReadPkg<EffectRegistry>(v => baseEffectRegistry = v);
            effectIdCounter = data.ReadUInt32();
            data.ReadPkg<ARHash>(v => effectInfo = v.to<uint, EffectRecord>());
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
            data.WritePkg(AAHash.from(qualitiesModifiedCount));
            data.WritePkg(AAHash.from(appliedFx));
            data.WritePkg(baseEffectRegistry);
            data.Write(effectIdCounter);
            data.WritePkg(ARHash.from(effectInfo));
            data.Write(lastPulseTime);
            data.WritePkg(AList.from(equipperEffectIds));
            data.WritePkg(AList.from(acquirerEffectIds));
            data.Write(flags);
            data.WritePkg(AHashSet.from(trackedEffects));
            data.WritePkg(AAHash.from(topEffects));
            data.WritePkg(AAMultiHash.from(effectCategorizationTable));
            data.WritePkg(AAHash.from(appliedAppearances));
        }
    }
}