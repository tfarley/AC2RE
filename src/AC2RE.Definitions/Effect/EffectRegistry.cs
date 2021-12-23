using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EffectRegistry : IPackage {

    public PackageType packageType => PackageType.EffectRegistry;

    // WLib EffectRegistry
    [Flags]
    public enum Flag : uint {
        None = 0,
        AreAcquireEffectsAppliedOnlyOnce = 1 << 0, // AreAcquireEffectsAppliedOnlyOnce 0x00000001
        AppliedAcquireEffectsAtLeastOnce = 1 << 1, // HaveAppliedAcquireEffectsAtLeastOnce 0x00000002
        HasHeartbeatEffects = 1 << 2, // HasHeartbeatEffects 0x00000004
        HasPulseEffects = 1 << 3, // HasPulseEffects 0x00000008
    }

    public Dictionary<uint, uint> qualitiesModifiedCount; // m_qualitiesModifiedCount
    public Dictionary<FxId, uint> appliedFx; // m_appliedFX
    public EffectRegistry baseEffectRegistry; // m_baseEffectRegistry
    public uint effectIdCounter; // m_uiEffectIDCounter
    public Dictionary<EffectId, EffectRecord> effectInfo; // m_effectInfo
    public double lastPulseTime; // m_ttLastPulse
    public List<EffectId> equipperEffectIds; // m_listEquipperEffectEids
    public List<EffectId> acquirerEffectIds; // m_listAcquirerEffectEids
    public Flag flags; // m_flags
    public HashSet<uint> trackedEffects; // m_setTrackedEffects
    public Dictionary<uint, EffectId> topEffects; // m_topEffects
    public Dictionary<uint, List<EffectId>> effectCategorizationTable; // m_effectCategorizationTable
    public Dictionary<uint, uint> appliedAppearances; // m_appliedAppearances

    public EffectRegistry() {

    }

    public EffectRegistry(AC2Reader data) {
        data.ReadPkg<AAHash>(v => qualitiesModifiedCount = v);
        data.ReadPkg<AAHash>(v => appliedFx = v.to<FxId, uint>());
        data.ReadPkg<EffectRegistry>(v => baseEffectRegistry = v);
        effectIdCounter = data.ReadUInt32();
        data.ReadPkg<ARHash>(v => effectInfo = v.to<EffectId, EffectRecord>());
        lastPulseTime = data.ReadDouble();
        data.ReadPkg<AList>(v => equipperEffectIds = v.to<EffectId>());
        data.ReadPkg<AList>(v => acquirerEffectIds = v.to<EffectId>());
        flags = (Flag)data.ReadUInt32();
        data.ReadPkg<AHashSet>(v => trackedEffects = v);
        data.ReadPkg<AAHash>(v => topEffects = v.to<uint, EffectId>());
        data.ReadPkg<AAMultiHash>(v => effectCategorizationTable = v.to<uint, EffectId>());
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
        data.Write((uint)flags);
        data.WritePkg(AHashSet.from(trackedEffects));
        data.WritePkg(AAHash.from(topEffects));
        data.WritePkg(AAMultiHash.from(effectCategorizationTable));
        data.WritePkg(AAHash.from(appliedAppearances));
    }
}
