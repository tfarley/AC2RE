using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EffectDesc : IHeapObject {

    public PackageType packageType => PackageType.EffectDesc;

    // WLib EffectDesc
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsConsidering = 1 << 0, // IsConsidering 0x00000001
        IsIgnoreProbability = 1 << 1, // IsIgnoreProbability 0x00000002
        IsIgnoreConsideration = 1 << 2, // IsIgnoreConsideration 0x00000004
        IsInfiniteTimeout = 1 << 3, // IsInfiniteTimeout 0x00000008
        IsNormalTimeout = 1 << 4, // IsNormalTimeout 0x00000010
        IsSpecifiedTimeout = 1 << 5, // IsSpecifiedTimeout 0x00000020
        IsRemoveOnLogout = 1 << 6, // IsRemoveOnLogout 0x00000040
        IsEquipperEffect = 1 << 7, // IsEquipperEffect 0x00000080
        IsToggled = 1 << 8, // IsToggled 0x00000100
        IsPulsed = 1 << 9, // IsPulsed 0x00000200
        IsClientNoUI = 1 << 10, // IsClientNoUI 0x00000400
    }

    public Dictionary<uint, IHeapObject> considerationHash; // m_hashConsideration
    public uint eid; // m_eid
    public int projectedTargetHealthChange; // m_iProjectedTargetHealthChange
    public int sourceVigorChange; // m_iSourceVigorChange
    public float timeout; // m_ttTimeout
    public InstanceId itemId; // m_iidItem
    public int targetFocusChange; // m_iTargetFocusChange
    public InstanceId actingForId; // m_iidActingFor
    public float spellcraft; // m_fSpellcraft
    public bool pk; // m_bPK
    public int targetHealthChange; // m_iTargetHealthChange
    public int casterLevel; // m_casterLevel
    public int targetHealth; // m_iTargetHealth
    public int sourceHealthChange; // m_iSourceHealthChange
    public SingletonPkg<Effect> effect; // m_effect
    public int targetVigor; // m_iTargetVigor
    public Flag flags; // m_flags
    public uint durabilityLevel; // m_uiDurabilityLevel
    public List<IHeapObject> sourceEffects; // m_listSourceEffect
    public int targetFocus; // m_iTargetFocus
    public InstanceId targetId; // m_iidTarget
    public uint result; // m_result
    public InstanceId sourceId; // m_iidSource
    public int sourceFocusChange; // m_iSourceFocusChange
    public bool hasProjectedHealth; // m_bHasProjectedHealth
    public int targetVigorChange; // m_iTargetVigorChange
    public DataId skillFromDid; // m_didSkillFrom

    public EffectDesc(AC2Reader data) {
        data.ReadHO<ARHash>(v => considerationHash = v);
        eid = data.ReadUInt32();
        projectedTargetHealthChange = data.ReadInt32();
        sourceVigorChange = data.ReadInt32();
        timeout = data.ReadSingle();
        itemId = data.ReadInstanceId();
        targetFocusChange = data.ReadInt32();
        actingForId = data.ReadInstanceId();
        spellcraft = data.ReadSingle();
        pk = data.ReadBoolean();
        targetHealthChange = data.ReadInt32();
        casterLevel = data.ReadInt32();
        targetHealth = data.ReadInt32();
        sourceHealthChange = data.ReadInt32();
        data.ReadHO<Effect>(v => effect = v);
        targetVigor = data.ReadInt32();
        flags = data.ReadEnum<Flag>();
        durabilityLevel = data.ReadUInt32();
        data.ReadHO<RList>(v => sourceEffects = v);
        targetFocus = data.ReadInt32();
        targetId = data.ReadInstanceId();
        result = data.ReadUInt32();
        sourceId = data.ReadInstanceId();
        sourceFocusChange = data.ReadInt32();
        hasProjectedHealth = data.ReadBoolean();
        targetVigorChange = data.ReadInt32();
        skillFromDid = data.ReadDataId();
    }
}
