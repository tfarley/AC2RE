using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum EffectInternalFlag : ulong {
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
}
