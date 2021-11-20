namespace AC2RE.Definitions {

    // From RepositoryRepository::GetRawLongInt and gmEntity::GetLongInt calls in wlib / WSL func gmPropertyMapper::constructor
    public enum LongIntStat : uint {
        Undef = 0,

        TotalXP = 300, // Agent::GetTotalExperience
        AvailableXP = 301, // Agent::GetAvailableExperience

        DeathXP = 312, // _ / GameplayStatistics_DeathExperience

        XPToRaiseVitae = 316, // Agent::GetExperienceNeededToRaiseVitae

        AllegianceXPPool = 501, // Player::GetAllegianceXPPool

        AllegianceXPInherited = 503, // Player::GetAllegianceXPInherited

        RadarColor = 601, // _ / RadarColor

        TotalCraftXP = 1000, // Player::GetTotalCraftExperience
        AvailableCraftXP = 1001, // Player::GetAvailableCraftExperience
    }
}
