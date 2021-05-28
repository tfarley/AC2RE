using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Effect : IPackage {

        public virtual PackageType packageType => PackageType.Effect;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            FORCE_CONSTANT_MAGNITUDE = 1 << 20, // 0x00100000, Effect::IsForceConstantMagnitude
            FORCE_VARIABLE_MAGNITUDE = 1 << 21, // 0x00200000, Effect::IsForceVariableMagnitude
        }

        // WLib
        [Flags]
        public enum InternalFlag : ulong {
            NONE = 0,
            ALL = ulong.MaxValue,

            MONSTER_ONLY = 1 << 0, // 0x00000001, Effect::IsMonsterOnly
            CONSTANT_CLASS_PRIORITY = 1 << 1, // 0x00000002, Effect::SetConstantClassPriority
            VARIABLE_CLASS_PRIORITY = 1 << 2, // 0x00000004, Effect::SetVariableClassPriority
            DISPLAY_TIMER = 1 << 3, // 0x00000008, Effect::GetShouldDisplayTimer

            CLIENT_VISIBLE = 1ul << 32, // 0x0000000100000000, Effect::IsClientVisible
            CONSTANT_APPLICATION_PROBABILITY = 1ul << 33, // 0x0000000200000000, Effect::SetConstantApplicationProbability
            SPECIAL_APPLICATION_PROBABILITY = 1ul << 34, // 0x0000000400000000, Effect::SetSpecialApplicationProbability
            VARIABLE_APPLICATION_PROBABILITY = 1ul << 35, // 0x0000000800000000, Effect::SetVariableApplicationProbability
            CONSTANT_DURATION = 1ul << 36, // 0x0000001000000000, Effect::SetConstantDuration
            SPECIAL_DURATION = 1ul << 37, // 0x0000002000000000, Effect::SetSpecialDuration
            VARIABLE_DURATION_ON_SKILL_LEVEL = 1ul << 38, // 0x0000004000000000, Effect::SetVariableDurationOnSkillLevel
            PERMANENT_DURATION = 1ul << 39, // 0x0000008000000000, Effect::SetPermanentDuration
            APPLICATION_PROBABILITY = 1ul << 40, // 0x0000010000000000, Effect::Set*ApplicationProbability
            DURATION = 1ul << 41, // 0x0000020000000000, Effect::Set*Duration

            CLIENT_NO_UI = 1ul << 43, // 0x0000080000000000, Effect::IsClientNoUI
            EFFECT_VITAL_CHANGE_INTERESTED = 1ul << 44, // 0x0000100000000000, Effect::IsEffectVitalChangeInterested
            LOGIN_INTERESTED = 1ul << 45, // 0x0000200000000000, Effect::IsLoginInterested
            SELF_TARGETED_SPELLCRAFT_CAP = 1ul << 46, // 0x0000400000000000, Effect::HasSelfTargetedSpellcraftCap
            RELATIVE_CASTER_LEVEL_SPELLCRAFT_CAP = 1ul << 47, // 0x0000800000000000, Effect::HasRelativeCasterLevelSpellcraftCap
            FORCE_EFFECT = 1ul << 48, // 0x0001000000000000, Effect::IsForceEffect
            PULSE = 1ul << 49, // 0x0002000000000000, Effect::IsPulseEffect
            EXTRACTABLE = 1ul << 50, // 0x0004000000000000, Effect::IsExtractable
            PRIMARY_TARGET_ONLY = 1ul << 51, // 0x0008000000000000, Effect::IsPrimaryTargetOnly
            INCIDENTAL_TARGET_ONLY = 1ul << 52, // 0x0010000000000000, Effect::IsIncidentalTargetOnly
            FACTION = 1ul << 53, // 0x0020000000000000, Effect::IsFactionEffect
            REMOVE_ON_DEATH = 1ul << 54, // 0x0040000000000000, Effect::IsRemoveOnDeath

            NOTIFY_CLIENT = 1ul << 56, // 0x0100000000000000, Effect::IsNotifyClient
            HARMFUL = 1ul << 57, // 0x0200000000000000, Effect::IsHarmful

            EFFECT_APPLICATION_INTERESTED = 1ul << 59, // 0x0800000000000000, Effect::IsEffectApplicationInterested
            VITAL_CHANGE_INTERESTED = 1ul << 60, // 0x1000000000000000, Effect::IsVitalChangeInterested
            HEARTBEAT = 1ul << 61, // 0x2000000000000000, Effect::IsHeartbeat
            INSTANTANEOUS = 1ul << 62, // 0x4000000000000000, Effect::IsInstantaneous
            CHAIN_BREAKER = 1ul << 63, // 0x8000000000000000, Effect::IsChainBreaker
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
            data.ReadPkg<RArray>(v => durationData = v.to<FloatScaleDuple>());
            fxId = (FxId)data.ReadUInt32();
            appValue = data.ReadSingle();
            flags = (Flag)data.ReadUInt32();
            tracked = data.ReadBoolean();
            enumVal = data.ReadUInt32();
            minTsysSpellcraft = data.ReadSingle();
            internalFlags = (InternalFlag)data.ReadUInt64();
            probVariance = data.ReadSingle();
            data.ReadPkg<StringInfo>(v => tsysItemName = v);
            data.ReadPkg<StringInfo>(v => description = v);
            appKey = (AppearanceKey)data.ReadUInt32();
            data.ReadPkg<RArray>(v => forceData = v.to<FloatScaleDuple>());
            examinationFlags = data.ReadUInt32();
            variance = data.ReadSingle();
            data.ReadPkg<DefaultPermissionBlob>(v => usagePermissions = v);
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
            data.ReadPkg<RArray>(v => priData = v.to<FloatScaleDuple>());
            data.ReadPkg<StringInfo>(v => name = v);
            data.ReadPkg<AList>(v => statList = v);
            data.ReadPkg<RArray>(v => probData = v.to<FloatScaleDuple>());
            tsysAppKey = (AppearanceKey)data.ReadUInt32();
            selfTargetedSpellcraftCap = data.ReadSingle();
            eqClass = data.ReadUInt32();
        }
    }
}
