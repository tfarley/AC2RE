using System;

namespace AC2RE.Definitions {

    // Const *_WeenieType
    [Flags]
    public enum WeenieType : uint {
        Undef = 0, // Undef_WeenieType
        Entity = 1 << 0, // Entity_WeenieType 0x1
        Container = (1 << 1) | Entity, // Container_WeenieType 0x3
        Inventory = (1 << 2) | Container, // Inventory_WeenieType 0x7
        Agent = (1 << 3) | Inventory, // Agent_WeenieType 0xF
        Player = (1 << 4) | Agent, // Player_WeenieType 0x1F
        Admin = (1 << 5) | Player, // Admin_WeenieType 0x3F
        NPC = (1 << 6) | Agent, // NPC_WeenieType 0x4F
        Item = (1 << 7) | Entity, // Item_WeenieType 0x81
        GameplayContainer = (1 << 8) | Container, // GameplayContainer_WeenieType 0x103

        MajorTypeMask = 0xFFFF, // MajorTypeMask_WeenieType

        Generator = 0x10000 | Entity, // Generator_WeenieType 0x10001
        VendorInventory = 0x10000 | Container, // VendorInventory_WeenieType 0x10003
        Monster = 0x10000 | NPC, // Monster_WeenieType 0x1004F
        Shield = 0x10000 | Item, // Shield_WeenieType 0x10081
        LandblockEntity = 0x20000 | Entity, // LandblockEntity_WeenieType 0x20001
        RealNPC = 0x20000 | NPC, // RealNPC_WeenieType 0x2004F
        Weapon = 0x20000 | Item, // Weapon_WeenieType 0x20081
        Corpse = 0x20000 | GameplayContainer, // Corpse_WeenieType 0x20103
        Clothing = 0x30000 | Item, // Clothing_WeenieType 0x30081
        Armor = 0x40000 | Item, // Armor_WeenieType 0x40081
        Lifestone = 0x50000 | Item, // Lifestone_WeenieType 0x50081
        Missile = 0x60000 | Item, // Missile_WeenieType 0x60081
        Portal = 0x70000 | Item, // Portal_WeenieType 0x70081
        Potion = 0x80000 | Item, // Potion_WeenieType 0x80081
        Door = 0x90000 | Item, // Door_WeenieType 0x90081
        SceneryObject = 0xA0000 | Item, // SceneryObject_WeenieType 0xA0081
        Saddle = 0xB0000 | Item, // Saddle_WeenieType 0xB0081
        Key = 0xC0000 | Item, // Key_WeenieType 0xC0081
        Mine = 0xD0000 | Item, // Mine_WeenieType 0xD0081
        Tool = 0xE0000 | Item, // Tool_WeenieType 0xE0081
        Coin = 0xF0000 | Item, // Coin_WeenieType 0xF0081
        Ingot = 0x100000 | Item, // Ingot_WeenieType 0x100081
        Jewelry = 0x200000 | Item, // Jewelry_WeenieType 0x200081
        Shard = 0x300000 | Item, // Shard_WeenieType 0x300081
        TreasureMap = 0x400000 | Item, // TreasureMap_WeenieType 0x400081
        Book = 0x500000 | Item, // Book_WeenieType 0x500081
        Forge = 0x600000 | Item, // Forge_WeenieType 0x600081
        Activator = 0x700000 | Item, // Activator_WeenieType 0x700081
    }
}
