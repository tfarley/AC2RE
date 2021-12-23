namespace AC2RE.Definitions;

// Const *_EntityType
public enum EntityType : uint {
    Invalid = 0, // Invalid_EntityType

    Physics = 0x40000001, // Physics_EntityType
    Weenie = 0x40000002, // Weenie_EntityType
    EntityGroup = 0x40000003, // EntityGroup_EntityType
    Cell = 0x40000004, // Cell_EntityType
    Camera = 0x40000005, // Camera_EntityType
    Light = 0x40000006, // Light_EntityType
    EntityDesc = 0x40000007, // EntityDesc_EntityType
    Landblock = 0x40000008, // Landblock_EntityType
    Volatile = 0x40000009, // Volatile_EntityType
    Link = 0x4000000A, // Link_EntityType
    Avatar = 0x4000000B, // Avatar_EntityType
    Visual = 0x4000000C, // Visual_EntityType
    UIScene = 0x4000000D, // UIScene_EntityType
    DBObj = 0x4000000E, // DBObj_EntityType
    Particle = 0x4000000F, // Particle_EntityType
    SceneFile = 0x40000010, // SceneFile_EntityType
    LinkDesc = 0x40000011, // LinkDesc_EntityType
    TeleportDestination = 0x40000012, // TeleportDestination_EntityType
    PathNodeHint = 0x40000013, // PathNodeHint_EntityType
}
