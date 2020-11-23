﻿namespace AC2RE.Definitions {

    // Const *_EntityType
    public enum EntityType : uint {
        INVALID = 0,

        PHYSICS = 0x40000001,
        WEENIE = 0x40000002,
        ENTITY_GROUP = 0x40000003,
        CELL = 0x40000004,
        CAMERA = 0x40000005,
        LIGHT = 0x40000006,
        ENTITY_DESC = 0x40000007,
        LANDBLOCK = 0x40000008,
        VOLATILE = 0x40000009,
        LINK = 0x4000000A,
        AVATAR = 0x4000000B,
        VISUAL = 0x4000000C,
        UI_SCENE = 0x4000000D,
        DB_OBJ = 0x4000000E,
        PARTICLE = 0x4000000F,
        SCENE_FILE = 0x40000010,
        LINK_DESC = 0x40000011,
        TELEPORT_DESTINATION = 0x40000012,
        PATH_NODE_HINT = 0x40000013,
    }
}