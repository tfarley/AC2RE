namespace AC2RE.Definitions {

    // Const *_PropertyGroupName
    public enum PropertyGroupName : uint {
        Invalid = 0, // Invalid_PropertyGroupName

        Physics = 0x40000001, // Physics_PropertyGroupName
        Render = 0x40000002, // Render_PropertyGroupName
        Ethereal = 0x40000003, // Ethereal_PropertyGroupName

        Light = 0x40000006, // Light_PropertyGroupName
        Environment = 0x40000007, // Environment_PropertyGroupName

        Clearance = 0x40000009, // Clearance_PropertyGroupName
        Camera = 0x4000000A, // Camera_PropertyGroupName
        ContentDB = 0x4000000B, // ContentDB_PropertyGroupName
        Generator = 0x4000000C, // Generator_PropertyGroupName
        Pushers = 0x4000000D, // Pushers_PropertyGroupName

        Usage = 0x4000000F, // Usage_PropertyGroupName
        Cell = 0x40000010, // Cell_PropertyGroupName
        Portal = 0x40000011, // Portal_PropertyGroupName
        Block = 0x40000012, // Block_PropertyGroupName
        Interior = 0x40000013, // Interior_PropertyGroupName

        GameplayLock = 0x40000015, // GameplayLock_PropertyGroupName
        TriggerEffect = 0x40000016, // TriggerEffect_PropertyGroupName
        Generic = 0x40000017, // Generic_PropertyGroupName
        Quest = 0x40000018, // Quest_PropertyGroupName
        AI = 0x40000019, // AI_PropertyGroupName
        AICombat = 0x4000001A, // AICombat_PropertyGroupName
        Mine = 0x4000001B, // Mine_PropertyGroupName
        Craft = 0x4000001C, // Craft_PropertyGroupName
        GameplayStatistics = 0x4000001D, // GameplayStatistics_PropertyGroupName
        Maintenance = 0x4000001E, // Maintenance_PropertyGroupName
        Forge = 0x4000001F, // Forge_PropertyGroupName
        Weapon = 0x40000020, // Weapon_PropertyGroupName
        Tools = 0x40000021, // Tools_PropertyGroupName
        PKPermission = 0x40000022, // PKPermission_PropertyGroupName
        Book = 0x40000023, // Book_PropertyGroupName
        Link = 0x40000024, // Link_PropertyGroupName
        Activator = 0x40000025, // Activator_PropertyGroupName
    }
}
