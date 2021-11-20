namespace AC2RE.Definitions {

    // Dat file 230000FD
    public enum UsagePermissionType : uint {
        Undef = 0,
        Closed = 1, // ClosedUsagePermission
        Corpse = 2, // CorpseUsagePermission
        Default = 3, // DefaultUsagePermission
        Equippable = 4, // EquippableUsagePermission
        Open = 5, // OpenUsagePermission

        HeroSkillCreditToken = 7, // HeroSkillCreditTokenUsagePermission
        Tool = 8, // ToolUsagePermission
    }
}
