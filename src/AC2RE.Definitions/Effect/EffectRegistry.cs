using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EffectRegistry : IHeapObject {

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
        data.ReadHO<AAHash>(v => qualitiesModifiedCount = v);
        data.ReadHO<AAHash>(v => appliedFx = v.to<FxId, uint>());
        data.ReadHO<EffectRegistry>(v => baseEffectRegistry = v);
        effectIdCounter = data.ReadUInt32();
        data.ReadHO<ARHash>(v => effectInfo = v.to<EffectId, EffectRecord>());
        lastPulseTime = data.ReadDouble();
        data.ReadHO<AList>(v => equipperEffectIds = v.to<EffectId>());
        data.ReadHO<AList>(v => acquirerEffectIds = v.to<EffectId>());
        flags = (Flag)data.ReadUInt32();
        data.ReadHO<AHashSet>(v => trackedEffects = v);
        data.ReadHO<AAHash>(v => topEffects = v.to<uint, EffectId>());
        data.ReadHO<AAMultiHash>(v => effectCategorizationTable = v.to<uint, EffectId>());
        data.ReadHO<AAHash>(v => appliedAppearances = v);
    }

    public void write(AC2Writer data) {
        data.WriteHO(AAHash.from(qualitiesModifiedCount));
        data.WriteHO(AAHash.from(appliedFx));
        data.WriteHO(baseEffectRegistry);
        data.Write(effectIdCounter);
        data.WriteHO(ARHash.from(effectInfo));
        data.Write(lastPulseTime);
        data.WriteHO(AList.from(equipperEffectIds));
        data.WriteHO(AList.from(acquirerEffectIds));
        data.Write((uint)flags);
        data.WriteHO(AHashSet.from(trackedEffects));
        data.WriteHO(AAHash.from(topEffects));
        data.WriteHO(AAMultiHash.from(effectCategorizationTable));
        data.WriteHO(AAHash.from(appliedAppearances));
    }
}
