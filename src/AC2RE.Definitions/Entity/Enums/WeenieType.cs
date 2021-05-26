using System;

namespace AC2RE.Definitions {

    // Const *_WeenieType
    [Flags]
    public enum WeenieType : uint {
        UNDEF = 0,
        ALL = uint.MaxValue,

        ENTITY = 1 << 0, // 0xF
        CONTAINER = (1 << 1) | ENTITY, // 0x3
        INVENTORY = (1 << 2) | CONTAINER, // 0x7
        AGENT = (1 << 3) | INVENTORY, // 0xF
        PLAYER = (1 << 4) | AGENT, // 0x1F
        ADMIN = (1 << 5) | PLAYER, // 0x3F
        NPC = (1 << 6) | AGENT, // 0x4F
        ITEM = (1 << 7) | ENTITY, // 0x81
        GAMEPLAYCONTAINER = (1 << 8) | CONTAINER, // 0x103

        MAJORTYPEMASK = 0xFFFF,

        GENERATOR = 0x10000 | ENTITY, // 0x10001
        VENDORINVENTORY = 0x10000 | CONTAINER, // 0x10003
        MONSTER = 0x10000 | NPC, // 0x1004F
        SHIELD = 0x10000 | ITEM, // 0x10081
        LANDBLOCKENTITY = 0x20000 | ENTITY, // 0x20001
        REALNPC = 0x20000 | NPC, // 0x2004F
        WEAPON = 0x20000 | ITEM, // 0x20081
        CORPSE = 0x20000 | GAMEPLAYCONTAINER, // 0x20103
        CLOTHING = 0x30000 | ITEM, // 0x30081
        ARMOR = 0x40000 | ITEM, // 0x40081
        LIFESTONE = 0x50000 | ITEM, // 0x50081
        MISSILE = 0x60000 | ITEM, // 0x60081
        PORTAL = 0x70000 | ITEM, // 0x70081
        POTION = 0x80000 | ITEM, // 0x80081
        DOOR = 0x90000 | ITEM, // 0x90081
        SCENERYOBJECT = 0xA0000 | ITEM, // 0xA0081
        SADDLE = 0xB0000 | ITEM, // 0xB0081
        KEY = 0xC0000 | ITEM, // 0xC0081
        MINE = 0xD0000 | ITEM, // 0xD0081
        TOOL = 0xE0000 | ITEM, // 0xE0081
        COIN = 0xF0000 | ITEM, // 0xF0081
        INGOT = 0x100000 | ITEM, // 0x100081
        JEWELRY = 0x200000 | ITEM, // 0x200081
        SHARD = 0x300000 | ITEM, // 0x300081
        TREASUREMAP = 0x400000 | ITEM, // 0x400081
        BOOK = 0x500000 | ITEM, // 0x500081
        FORGE = 0x600000 | ITEM, // 0x600081
        ACTIVATOR = 0x700000 | ITEM, // 0x700081
    }
}
