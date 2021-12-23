namespace AC2RE.Definitions;

// Const *_TSStat
public enum TimestampStat : uint {
    Undef = 0, // Undef_TSStat
    CheckPoint = 1, // CheckPoint_TSStat
    ItemHousekeepingDeleteTime = 2, // ItemHousekeepingDeleteTime_TSStat

    LastDeathTime = 256, // LastDeathTime_TSStat

    Usage_LastUsageTime = 300, // Usage_LastUsageTime_TSStat
    DataMiner_LoginTime = 301, // DataMiner_LoginTime_TSStat
    LastAttackTime = 302, // LastAttackTime_TSStat
    Skill_TimeLastReset = 303, // Skill_TimeLastReset_TSStat
    Skill_LastUntrain = 304, // Skill_LastUntrain_TSStat
    Combat_LastCombatInteraction = 305, // Combat_LastCombatInteraction_TSStat
    Skill_LastCompleteReset = 306, // Skill_LastCompleteReset_TSStat
    CraftSkill_LastCompleteReset = 307, // CraftSkill_LastCompleteReset_TSStat
    Skill_LastCompleteHeroReset = 308, // Skill_LastCompleteHeroReset_TSStat

    AI_ImplementChange = 400, // AI_ImplementChange_TSStat
    AI_PetLastCommandTime = 401, // AI_PetLastCommandTime_TSStat
    AI_Timer = 402, // AI_Timer_TSStat

    Faction_LastChangeTime = 500, // Faction_LastChangeTime_TSStat
    Faction_LandblockLastChangeTime = 501, // Faction_LandblockLastChangeTime_TSStat

    Allegiance_LastProfileSync = 600, // Allegiance_LastProfileSync_TSStat

    Trade_LastOpenTrade = 700, // Trade_LastOpenTrade_TSStat

    Heartbeat_LastHeartbeatTime = 800, // Heartbeat_LastHeartbeatTime_TSStat

    ExtraSalesUpdated = 850, // ExtraSalesUpdated_TSStat

    CreationDate = 900, // CreationDate_TSStat
}
