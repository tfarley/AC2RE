using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Effect : IHeapObject {

    public virtual PackageType packageType => PackageType.Effect;

    // WLib Effect
    [Flags]
    public enum Flag : uint {
        None = 0,

        IsForceConstantMagnitude = 1 << 20, // IsForceConstantMagnitude 0x00100000
        IsForceVariableMagnitude = 1 << 21, // IsForceVariableMagnitude 0x00200000
    }

    // WLib Effect
    [Flags]
    public enum InternalFlag : ulong {
        None = 0,
        IsMonsterOnly = 1 << 0, // IsMonsterOnly 0x00000001
        HasConstantClassPriority = 1 << 1, // SetConstantClassPriority 0x00000002
        HasVariableClassPriority = 1 << 2, // SetVariableClassPriority 0x00000004
        ShouldDisplayTimer = 1 << 3, // GetShouldDisplayTimer 0x00000008

        IsClientVisible = 1ul << 32, // IsClientVisible 0x0000000100000000
        HasConstantApplicationProbability = 1ul << 33, // SetConstantApplicationProbability 0x0000000200000000
        HasSpecialApplicationProbability = 1ul << 34, // SetSpecialApplicationProbability 0x0000000400000000
        HasVariableApplicationProbability = 1ul << 35, // SetVariableApplicationProbability 0x0000000800000000
        HasConstantDuration = 1ul << 36, // SetConstantDuration 0x0000001000000000
        HasSpecialDuration = 1ul << 37, // SetSpecialDuration 0x0000002000000000
        HasVariableDurationOnSkillLevel = 1ul << 38, // SetVariableDurationOnSkillLevel 0x0000004000000000
        HasPermanentDuration = 1ul << 39, // SetPermanentDuration 0x0000008000000000
        HasApplicationProbability = 1ul << 40, // Set*ApplicationProbability 0x0000010000000000
        HasDuration = 1ul << 41, // Set*Duration 0x0000020000000000

        IsClientNoUI = 1ul << 43, // IsClientNoUI 0x0000080000000000
        IsEffectVitalChangeInterested = 1ul << 44, // IsEffectVitalChangeInterested 0x0000100000000000
        IsLoginInterested = 1ul << 45, // IsLoginInterested 0x0000200000000000
        HasSelfTargetedSpellcraftCap = 1ul << 46, // HasSelfTargetedSpellcraftCap 0x0000400000000000
        HasRelativeCasterLevelSpellcraftCap = 1ul << 47, // HasRelativeCasterLevelSpellcraftCap 0x0000800000000000
        IsForceEffect = 1ul << 48, // IsForceEffect 0x0001000000000000
        IsPulseEffect = 1ul << 49, // IsPulseEffect 0x0002000000000000
        IsExtractable = 1ul << 50, // IsExtractable 0x0004000000000000
        IsPrimaryTargetOnly = 1ul << 51, // IsPrimaryTargetOnly 0x0008000000000000
        IsIncidentalTargetOnly = 1ul << 52, // IsIncidentalTargetOnly 0x0010000000000000
        IsFactionEffect = 1ul << 53, // IsFactionEffect 0x0020000000000000
        IsRemoveOnDeath = 1ul << 54, // IsRemoveOnDeath 0x0040000000000000

        IsNotifyClient = 1ul << 56, // IsNotifyClient 0x0100000000000000
        IsHarmful = 1ul << 57, // IsHarmful 0x0200000000000000

        IsEffectApplicationInterested = 1ul << 59, // IsEffectApplicationInterested 0x0800000000000000
        IsVitalChangeInterested = 1ul << 60, // IsVitalChangeInterested 0x1000000000000000
        IsHeartbeat = 1ul << 61, // IsHeartbeat 0x2000000000000000
        IsInstantaneous = 1ul << 62, // IsInstantaneous 0x4000000000000000
        IsChainBreaker = 1ul << 63, // IsChainBreaker 0x8000000000000000
    }

    public List<FloatScaleDuple> durationData; // m_durationData
    public FxId fxId; // m_fxID
    public float appValue; // m_fAprValue
    public Flag flags; // m_uiExternalFlags
    public bool tracked; // m_tracked
    public uint enumVal; // m_enum
    public float minTsysSpellcraft; // m_fMinTsysSpellcraft
    public InternalFlag internalFlags; // m_uiInternalFlags
    public float probVariance; // m_fProbVariance
    public StringInfo tsysItemName; // m_strTsysItemName
    public StringInfo description; // m_strDescription
    public AppearanceKey appKey; // m_aprKey
    public List<FloatScaleDuple> forceData; // m_forceData
    public uint examinationFlags; // m_ExaminationFlags
    public float variance; // m_fVariance
    public DefaultPermissionBlob usagePermissions; // m_usagePermissions
    public bool removeOnLogout; // m_removeOnLogout
    public bool removeOnTeleport; // m_removeOnTeleport
    public DataId iconDid; // m_didIcon
    public int tsysValue; // m_iTsysValue
    public uint forceModifyVital; // m_forceModifyVital
    public uint durabilityLevel; // m_uiDurabilityLevel
    public float relativeCasterLevelSpellcraftCap; // m_fRelativeCasterLevelSpellcraftCap
    public float maxTsysSpellcraft; // m_fMaxTsysSpellcraft
    public float tsysAppValue; // m_tsysAprValue
    public uint endingFxId; // m_endingFxID
    public bool clearOnUse; // m_clearOnUse
    public List<FloatScaleDuple> priData; // m_priData
    public StringInfo name; // m_strName
    public List<uint> statList; // m_statList
    public List<FloatScaleDuple> probData; // m_probData
    public AppearanceKey tsysAppKey; // m_tsysAprKey
    public float selfTargetedSpellcraftCap; // m_fSelfTargetedSpellcraftCap
    public uint eqClass; // m_eqClass

    public Effect(AC2Reader data) {
        data.ReadHO<RArray>(v => durationData = v.to<FloatScaleDuple>());
        fxId = (FxId)data.ReadUInt32();
        appValue = data.ReadSingle();
        flags = (Flag)data.ReadUInt32();
        tracked = data.ReadBoolean();
        enumVal = data.ReadUInt32();
        minTsysSpellcraft = data.ReadSingle();
        internalFlags = (InternalFlag)data.ReadUInt64();
        probVariance = data.ReadSingle();
        data.ReadHO<StringInfo>(v => tsysItemName = v);
        data.ReadHO<StringInfo>(v => description = v);
        appKey = (AppearanceKey)data.ReadUInt32();
        data.ReadHO<RArray>(v => forceData = v.to<FloatScaleDuple>());
        examinationFlags = data.ReadUInt32();
        variance = data.ReadSingle();
        data.ReadHO<DefaultPermissionBlob>(v => usagePermissions = v);
        removeOnLogout = data.ReadBoolean();
        removeOnTeleport = data.ReadBoolean();
        iconDid = data.ReadDataId();
        tsysValue = data.ReadInt32();
        forceModifyVital = data.ReadUInt32();
        durabilityLevel = data.ReadUInt32();
        relativeCasterLevelSpellcraftCap = data.ReadSingle();
        maxTsysSpellcraft = data.ReadSingle();
        tsysAppValue = data.ReadSingle();
        endingFxId = data.ReadUInt32();
        clearOnUse = data.ReadBoolean();
        data.ReadHO<RArray>(v => priData = v.to<FloatScaleDuple>());
        data.ReadHO<StringInfo>(v => name = v);
        data.ReadHO<AList>(v => statList = v);
        data.ReadHO<RArray>(v => probData = v.to<FloatScaleDuple>());
        tsysAppKey = (AppearanceKey)data.ReadUInt32();
        selfTargetedSpellcraftCap = data.ReadSingle();
        eqClass = data.ReadUInt32();
    }
}
