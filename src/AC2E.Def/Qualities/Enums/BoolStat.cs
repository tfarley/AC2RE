﻿namespace AC2E.Def {

    // Const *_BoolStat
    public enum BoolStat : uint {
        UNDEF = 0,
        IGNORECOLLISIONS = 1,
        REPORTCOLLISIONS = 2,
        ETHEREAL = 3,
        GRAVITYSTATUS = 4,
        LIGHTSSTATUS = 5,
        INELASTIC = 6,
        ALLOWEDGESLIDE = 7,
        ISMOBILE = 8,
        PLACEABLE = 9,
        NODRAW = 10,
        DOESIMPACTDAMAGE = 12,
        OBSOLETE_ISITEMHOUSEKEEPINGMSGPENDING = 13,
        NEVERHOUSEKEEP = 14,
        REAPPLYPROPERTIESONVERSIONCHANGE = 15,
        CS_INITTED = 30,
        CS_VERIFIED = 31,
        SO_INITTED = 32,
        SO_VERIFIED = 33,
        ISCLOAKING = 256,
        ISUNCLOAKING = 257,
        ISCLOAKED = 258,
        HEARTBEATTOGGLE = 270,
        DEAD = 279,
        DESTROYALLITEMSONROT = 280,
        DESTROYONCORPSEROT = 281,
        DESTROYONDEATH = 282,
        LOSEONDEATH = 283,
        NEVERLOSEONDEATH = 284,
        LOSEALLINVENTORYONDEATH = 285,
        LOOTPROOF = 286,
        UNLOCKAFTERFIRSTLOOT = 287,
        DEATH_COPYINVENTORYTOCORPSE = 288,
        DEATH_CANRESURRECT = 289,
        DEATH_NEVERSAYDIE = 290,
        DEATH_NEVERLEAVECORPSE = 291,
        DEATH_LASTKILLEDBYPLAYER = 292,
        DEATH_ISBEINGBUTCHERED = 293,
        USAGE_USEWHENCOLLIDED = 300,
        USAGE_USEWHILEMOVING = 301,
        USAGE_DESTROYONUSE = 302,
        USAGE_LOCKONUSE = 303,
        USAGE_AGENTDESTROYONUSE = 304,
        USAGE_INVENTORYREQUIRED = 305,
        USAGE_MOVETOTARGET = 306,
        USAGE_CANCELSLIFESTONEPROTECTION = 307,
        USAGE_LOCKABLE = 308,
        USAGE_LOCKED = 309,
        USAGE_OPENONUNLOCK = 310,
        USAGE_LOCKONCLOSE = 311,
        USAGE_UNOPENABLE = 312,
        USAGE_UNCLOSEABLE = 313,
        USAGE_AICANIGNORELOCK = 314,
        USAGE_AICANUSEDOORS = 315,
        USAGE_LANDBLOCKFACTIONREQUIRED = 316,
        USAGE_NONALLEGIANCEONLY = 317,
        USAGE_MONARCHONLY = 318,
        USAGE_SHOULDDELEGATEUSAGE = 319,
        USAGE_SHOULDAPPLYEFFECTSTOTARGET = 320,
        USAGE_CRAFTERONLY = 321,
        USAGE_HEROONLY = 322,
        USAGE_SHOULDUNLOCKUSERFORUSAGEEFFECTS = 323,
        USAGE_SUMMONERONLY = 324,
        USAGE_DURABILITYLOSTONUSE = 325,
        WEAPON_HARMLESS = 600,
        ISCRAFTED = 601,
        ISQUESTITEM = 602,
        ISRAREITEM = 603,
        ISINCOMPARABLEITEM = 604,
        ISEXTRACTABLE = 605,
        EFFECT_ISENCHANTABLE = 620,
        ISUSABLE = 623,
        ISSELECTABLE = 624,
        ISTAKEABLE = 625,
        STACKABLE = 700,
        ATTUNABLE = 701,
        OPEN = 702,
        INVENTORY_IGNORESATTUNEMENT = 703,
        INVENTORY_IGNORESTAKEPERMISSIONS = 704,
        DEATH_LOOTABSOLUTEOVERRIDE = 800,
        GEN_NONPERSONALIZABLE = 801,
        GEN_ISAGENERATOR = 810,
        GEN_CONTAINEDWAITONOPEN = 812,
        GEN_MANAGED = 813,
        GEN_REGENALLIFUNBOUND = 814,
        GEN_INTERNAL = 815,
        GEN_ENTERWORLDPRESERVE = 816,
        GEN_MUNGE = 817,
        GEN_DONTMUNGE = 818,
        GEN_ABSOLUTEQUANTITY = 819,
        GEN_CHECKPOINT = 820,
        GEN_DONTCHECKPOINT = 821,
        GEN_REGENONOPENCONTAINER = 822,
        GEN_ISALINKEDOBJECT = 830,
        COMBAT_AUTOMATICALLYMOVE = 1000,
        COMBAT_NOTATTACKABLE = 1001,
        PLAYER_HASSTARTEDCHARACTERSESSIONFORTHEFIRSTTIME = 2000,
        PLAYER_HASLEVELEDUPFORTHEFIRSTTIME = 2001,
        PLAYER_ISONMOUNT = 2002,
        VENDOR_PURCHASES = 3000,
        VENDOR_PURCHASESMAGIC = 3001,
        VENDOR_DESTROYONSELL = 3002,
        ISHERO = 4000,
        CRAFT_ISCRAFTSKILLRESETTING = 5000,
        AI_INVCREATED = 9501,
        AI_TELEPORT = 9502,
        AI_WANDERING = 9503,
        AI_FREEATTACKING = 9504,
        AI_CANJOINCLIQUES = 9505,
        AI_HASDETECTIONSPHERES = 9506,
        AI_USETARGETEDDETECTION = 9507,
        AI_WIELDIMPLEMENTS = 9510,
        AI_WEAPONANDSHIELD = 9511,
        AI_DUALWIELD = 9512,
        AI_WIELDTWOHANDED = 9513,
        AI_GROUPMONSTER = 9515,
        AI_CHAMPIONMONSTER = 9516,
        AI_UNIQUEMONSTER = 9517,
        AI_SPECIALEFFECTMONSTER = 9518,
        AI_QUESTMONSTER = 9519,
        AI_FACTIONBASEDONLANDBLOCK = 9520,
        AI_FACTIONOWNERSHIPBASEDONDEATH = 9521,
        AI_UNWIELDITEMSONIDLE = 9522,
        AI_IDLEONLY = 9523,
        BOOK_SHOWCONTROLS = 9524,
    }
}
