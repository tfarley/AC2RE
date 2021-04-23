﻿namespace AC2RE.Definitions {

    // Const *_IntStat and WSL func gmPropertyMapper::constructor
    public enum IntStat : uint {
        UNDEF = 0,
        PLACEMENT = 1,
        PHYSICSSTATE = 2,
        ETHEREALPHYSICSTYPELOW = 3,
        ETHEREALPHYSICSTYPEHIGH = 4,
        ETHEREALPLACEMENTTYPELOW = 5,
        ETHEREALPLACEMENTTYPEHIGH = 6,
        ETHEREALMOVEMENTTYPELOW = 7,
        ETHEREALMOVEMENTTYPEHIGH = 8,
        DETECTIONREPORTTYPELOW = 10,
        DETECTIONREPORTTYPEHIGH = 11,
        WEENIETYPE = 20,
        SEEMEVISIBILITYMASK = 21,
        CANSEEVISIBILITYMASK = 22,
        HEALTH_RAWLEVEL = 256,
        VIGOR_RAWLEVEL = 257,
        HEALTH_CURRENTLEVEL = 258,
        VIGOR_CURRENTLEVEL = 259,
        PLACEMENTPOSITION = 260,
        MAXSTACKSIZE = 261,
        QUANTITY = 262,
        HEALTH_CACHEDMAX = 263,
        VIGOR_CACHEDMAX = 264,
        LASTEQUIPPEDLOCATION = 265,
        FOCUS_CURRENTLEVEL = 266,
        FOCUS_MAX = 267,
        CLOTHINGPRIORITY = 268,
        PK_DAMAGE = 269,
        PK_VIGORLOSS = 270,
        WEAPONLENGTH = 272,
        CURRENTEQUIPPEDLOCATION = 274,
        VALIDINVENTORYLOCATIONS = 275,
        INV_PRIMARYPARENTINGLOCATION = 276,
        INV_SECONDARYPARENTINGLOCATION = 277,
        PARENTINGORIENTATION = 278,
        CONTAINERMAXCAPACITY = 280,
        PREFERREDINVENTORYLOCATION = 282,
        PRECLUDEDINVENTORYLOCATIONS = 283,
        IMPLEMENTTYPEPRIMARY = 284,
        IMPLEMENTTYPESECONDARY = 285,
        SLOTS = 286,
        SPECIES = 290,
        SEX = 291,
        CLASS = 292,
        NATURALARMOR = 293,
        SAFEMODESTATE = 294,
        AGENT_DAMAGEADD = 295,
        AGENT_VIGOR_COST = 296,
        AGENT_FOCUS_COST = 297,
        OBSOLETE_TOTALXP = 300,
        OBSOLETE_AVAILABLEXP = 301,
        LEVEL = 302,
        OBSOLETE_DEATHXP = 312,
        CHALLENGELEVEL = 313,
        DEATHCOUNT = 314,
        DEATHFOCUS = 315,
        OBSOLETE_XPTORAISEVITAE = 316,
        KILLERLEVEL = 317,
        ORIGINATORLEVEL = 318,
        GEN_BEHAVIOR = 370,
        GEN_TOGGLEGAMEEVENT = 371,
        GEN_MINQUANTITYOVERRIDE = 380,
        GEN_MAXQUANTITYOVERRIDE = 381,
        GEN_STATE = 382,
        GEN_EXITWORLDBEHAVIOR = 383,
        GEN_PROFILE = 384,
        GEN_DAYPROFILE = 385,
        GEN_NIGHTPROFILE = 386,
        GEN_QUALITYOVERRIDE = 387,
        GEN_QUALITYVARIANCEOVERRIDE = 388,
        DEATH_LOOTQUALITYOVERRIDE = 389,
        DEATH_LOOTQUALITYVARIANCEOVERRIDE = 390,
        DEATH_LOOTMINQUANTITYOVERRIDE = 397,
        DEATH_LOOTMAXQUANTITYOVERRIDE = 398,
        DEATH_LOOTPROFILE = 399,
        TSYS_COARSEITEMCLASS = 400,
        TSYS_FINEITEMCLASS = 420,
        LUCK = 421,
        TSYS_MUNDANEMUTATION = 422,
        CONFIRMATIONTOKEN = 450,
        FELLOWSHIP_CONFIRMATIONTOKEN = 451,
        ALLEGIANCE_RANK = 500,
        OBSOLETE_ALLEGIANCE_XPPOOL = 501,
        ALLEGIANCE_CONFIRMATIONTOKEN = 502,
        OBSOLETE_ALLEGIANCE_XPINHERITED = 503,
        ALLEGIANCE_RENAMECREDITS = 504,
        TRADE_CONFIRMATIONTOKEN = 525,
        PK_ALWAYSTRUEPERMISSIONS = 550,
        PK_ALWAYSFALSEPERMISSIONS = 551,
        PK_RATING = 552,
        PK_LASTSUBMITTEDRATING = 553,
        PK_CREATORTYPE = 554,
        WEAPON_DAMAGE = 600,
        WEAPON_SPEED = 601,
        WEAPON_SINGLEWEAPONSTANCE = 602,
        WEAPON_WITHSHIELDSTANCE = 603,
        WEAPON_DUALWIELDSTANCE = 604,
        WEAPON_OFFENSEMOD = 605,
        VIGORCOST = 609,
        ARMORLEVEL = 610,
        COMBATDELAY = 611,
        VALUE = 612,
        MAXINSCRIPTIONLENGTH = 613,
        INSCRIBEPERMISSION = 614,
        EQUIPMENTSLIDER = 615,
        RADARBLIP = 616,
        FOCUSCOST = 617,
        DURABILITY_MAXLEVEL = 618,
        DURABILITY_CURRENTLEVEL = 619,
        USAGE_MINLEVEL = 700,
        USAGE_MAXLEVEL = 701,
        USAGE_REQUIREDSKILL1 = 702,
        USAGE_REQUIREDSKILL2 = 703,
        USAGE_REQUIREDSKILL1RATING = 704,
        USAGE_REQUIREDSKILL2RATING = 705,
        USAGE_RESTRICTEDSKILL1 = 706,
        USAGE_RESTRICTEDSKILL2 = 707,
        USAGE_REQUIREDRACE = 708,
        USAGE_REQUIREDFACTION = 709,
        USAGE_VALIDWEENIETYPE = 710,
        USAGE_REQUIREDQUEST = 711,
        USAGE_REQUIREDQUESTSTATUS = 712,
        USAGE_MINIMUMRANK = 713,
        USAGE_MAXIMUMRANK = 714,
        USAGE_REQUIREDQUEST_TAKE = 715,
        USAGE_REQUIREDQUESTSTATUS_TAKE = 716,
        USAGE_REQUIREDCRAFTSKILLRATING = 718,
        USAGE_NUMBEROFENTITIESUSEDWITH = 720,
        USAGE_TARGETTYPE = 721,
        USAGE_VALIDTARGETTYPES = 722,
        USAGE_KEYID = 723,
        USAGE_BEHAVIORNAME = 724,
        USAGE_HEALTHCOST = 725,
        USAGE_VIGORCOST = 726,
        USAGE_USERBEHAVIORNAME = 727,
        USAGE_REQUIREDARCANELORE = 728,
        USAGE_USERBEHAVIORREPEATCOUNT = 729,
        USAGE_REQUIREDLOCATIONFEEDBACKTYPE = 730,
        USAGE_REQUIREDSKILLLEVEL = 731,
        USAGE_TARGETMINLEVEL = 732,
        USAGE_TARGETREQUIREDARCANELORE = 733,
        USAGE_TARGETWEENIETYPE = 734,
        CRAFT_PRIMARYTRAIT = 800,
        CRAFT_PRIMARYTRAITAMOUNT = 801,
        CRAFT_SECONDARYTRAIT = 803,
        CRAFT_SECONDARYTRAITAMOUNT = 804,
        CRAFT_RARETRAIT = 806,
        CRAFT_FLAGS = 807,
        CRAFT_MINEMAXUSES = 830,
        CRAFT_MINEUSAGERESETTIME = 831,
        CRAFT_MINEOBJECTQUANTITY = 832,
        GROOVELEVEL = 950,
        FACTION_MEMBERSHIP = 2000,
        FACTION_STATUS = 2001,
        FACTION_OWNERSHIP = 2002,
        FACTION_LOCALSTATUS = 2003,
        VENDOR_MINBUYVALUE = 3000,
        VENDOR_MAXBUYVALUE = 3001,
        MONEY = 3100,
        IMPLEMENTTYPE = 4000,
        MUSICCHANNEL = 4001,
        MAXUPKEEPPOINTS = 4100,
        CURRENTUPKEEPPOINTS = 4101,
        ENTERWORLDFX = 4102,
        APPEARANCEMUTATIONKEY = 4103,
        SKILLTARGETFLAGS = 4104,
        NPC_DAMAGETYPE = 4105,
        QUEST_BESTOWEDSCENEID = 4200,
        TRAVEL_PORTALFLAGS = 4201,
        TRAVEL_PORTALSCENE = 4202,
        AI_ITEMHINT = 9502,
        AI_DETECTIONTYPELOW = 9503,
        AI_DETECTIONTYPEHIGH = 9504,
        AI_MOVEMENTTYPE = 9505,
        AI_DETECTIONCONTEXT = 9506,
        AI_ATTACKABILITY = 9509,
        AI_CURRENTCHAINCATEGORY = 9510,
        AI_SUPERCLASS = 9600,
        AI_SUBCLASS = 9601,
        AI_PETFLAGS = 9602,
        AI_PETCLASS = 9603,
        AI_LOWARMORLEVEL = 9700,
        AI_HIGHARMORLEVEL = 9701,
        AI_LOWPLAYERLEVEL = 9702,
        AI_HIGHPLAYERLEVEL = 9703,
        SKILL_RESETS = 9800,
        NAMECHANGE_CREDITS = 9801,
        WORLDID_MIGRATEDTO = 9802,
        REALTIMEPLAYERBECAMEHERO = 9803,
        CRAFTSKILL_RESETS = 9804,
        CRAFT_TOOLCRAFTSKILLMOD = 9805,
        ACTIVATION_TYPE = 9806,
        HEROSKILL_RESETS = 9807,
        EXTRASALES = 9808,
    }
}
