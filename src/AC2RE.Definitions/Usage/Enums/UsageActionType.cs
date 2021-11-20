namespace AC2RE.Definitions {

    // Dat file 230000FC
    public enum UsageActionType : uint {
        Undef = 0,

        Coin = 29, // CoinUsageAction
        Container = 30, // ContainerUsageAction
        DoNothing = 31, // DoNothingUsageAction
        Door = 32, // DoorUsageAction
        Equippable = 33, // EquippableUsageAction
        Examine = 34, // ExamineUsageAction
        Key = 35, // KeyUsageAction
        Lifestone = 36, // LifestoneUsageAction
        Mine = 37, // MineUsageAction
        Monster = 38, // MonsterUsageAction

        NPC = 40, // NPCUsageAction
        PKRankBoard = 41, // PKRankBoardUsageAction
        Portal = 42, // PortalUsageAction
        Potion = 43, // PotionUsageAction
        Shard = 44, // ShardUsageAction
        Vault = 45, // VaultUsageAction
        Vendor = 46, // VendorUsageAction
        Weapon = 47, // WeaponUsageAction
        Saddle = 48, // SaddleUsageAction
        InscriptionControlledPortal = 49, // InscriptionControlledPortalUsageAction
        AllegianceHallBindingStone = 50, // AllegianceHallBindingStoneUsageAction
        Ball = 51, // BallUsageAction
        ItemInteraction = 52, // ItemInteractionUsageAction
        Book = 53, // BookUsageAction
        ButcheryTool = 54, // ButcheryToolUsageAction
        Totem = 55, // TotemUsageAction
        Activator = 56, // ActivatorUsageAction
        PortalDoor = 57, // PortalDoorUsageAction
        HeroSkillCreditToken = 58, // HeroSkillCreditTokenUsageAction
    }
}
