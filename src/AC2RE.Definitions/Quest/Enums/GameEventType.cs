﻿namespace AC2RE.Definitions;

// Dat file 23000105
public enum GameEventType : uint {
    Undef = 0,

    May03Trigger = 0x40000001, // May03Trigger
    Event_June_Osteth_1A = 0x40000002, // Event_June_Osteth_1A
    Event_June_Osteth_1B = 0x40000003, // Event_June_Osteth_1B
    Event_June_Osteth_1C = 0x40000004, // Event_June_Osteth_1C
    Event_June_Osteth_2A = 0x40000005, // Event_June_Osteth_2A
    Event_June_Osteth_2B = 0x40000006, // Event_June_Osteth_2B
    Event_June_Osteth_2C = 0x40000007, // Event_June_Osteth_2C
    Event_June_Omishan_1A = 0x40000008, // Event_June_Omishan_1A
    Event_June_Omishan_1B = 0x40000009, // Event_June_Omishan_1B
    Event_June_Omishan_1C = 0x4000000A, // Event_June_Omishan_1C
    Event_June_Omishan_2A = 0x4000000B, // Event_June_Omishan_2A
    Event_June_Omishan_2B = 0x4000000C, // Event_June_Omishan_2B
    Event_June_Omishan_2C = 0x4000000D, // Event_June_Omishan_2C
    Event_June_Linvak_1A = 0x4000000E, // Event_June_Linvak_1A
    Event_June_Linvak_1B = 0x4000000F, // Event_June_Linvak_1B
    Event_June_Linvak_1C = 0x40000010, // Event_June_Linvak_1C
    Event_June_Linvak_2A = 0x40000011, // Event_June_Linvak_2A
    Event_June_Linvak_2B = 0x40000012, // Event_June_Linvak_2B
    Event_June_Linvak_2C = 0x40000013, // Event_June_Linvak_2C
    Event_June_Osteth_0 = 0x40000014, // Event_June_Osteth_0
    Event_June_Omishan_0 = 0x40000015, // Event_June_Omishan_0
    Event_June_Linvak_0 = 0x40000016, // Event_June_Linvak_0
    EventSalvationKeiNPC = 0x40000017, // EventSalvationKeiNPC
    EventSalvationKeiMonster = 0x40000018, // EventSalvationKeiMonster

    EventRadRedSha = 0x4000006E, // EventRadRedSha
    EventRadYelSha = 0x4000006F, // EventRadYelSha
    EventRadGrnSha = 0x40000070, // EventRadGrnSha
    EventRadBluSha = 0x40000071, // EventRadBluSha
    EventRadVioSha = 0x40000072, // EventRadVioSha
    EventRadRedOrd = 0x40000073, // EventRadRedOrd
    EventRadYelOrd = 0x40000074, // EventRadYelOrd
    EventRadGrnOrd = 0x40000075, // EventRadGrnOrd
    EventRadBluOrd = 0x40000076, // EventRadBluOrd
    EventRadVioOrd = 0x40000077, // EventRadVioOrd
    EventRadRedDom = 0x40000078, // EventRadRedDom
    EventRadYelDom = 0x40000079, // EventRadYelDom
    EventRadGrnDom = 0x4000007A, // EventRadGrnDom
    EventRadBluDom = 0x4000007B, // EventRadBluDom
    EventRadVioDom = 0x4000007C, // EventRadVioDom
    EventMonRedSha = 0x4000007D, // EventMonRedSha
    EventMonYelSha = 0x4000007E, // EventMonYelSha
    EventMonGrnSha = 0x4000007F, // EventMonGrnSha
    EventMonBluSha = 0x40000080, // EventMonBluSha
    EventMonVioSha = 0x40000081, // EventMonVioSha
    EventMonRedOrd = 0x40000082, // EventMonRedOrd
    EventMonYelOrd = 0x40000083, // EventMonYelOrd
    EventMonGrnOrd = 0x40000084, // EventMonGrnOrd
    EventMonBluOrd = 0x40000085, // EventMonBluOrd
    EventMonVioOrd = 0x40000086, // EventMonVioOrd
    EventMonRedDom = 0x40000087, // EventMonRedDom
    EventMonYelDom = 0x40000088, // EventMonYelDom
    EventMonGrnDom = 0x40000089, // EventMonGrnDom
    EventMonBluDom = 0x4000008A, // EventMonBluDom
    EventMonVioDom = 0x4000008B, // EventMonVioDom
    EventGrdRedSha = 0x4000008C, // EventGrdRedSha
    EventGrdYelSha = 0x4000008D, // EventGrdYelSha
    EventGrdGrnSha = 0x4000008E, // EventGrdGrnSha
    EventGrdBluSha = 0x4000008F, // EventGrdBluSha
    EventGrdVioSha = 0x40000090, // EventGrdVioSha
    EventGrdRedOrd = 0x40000091, // EventGrdRedOrd
    EventGrdYelOrd = 0x40000092, // EventGrdYelOrd
    EventGrdGrnOrd = 0x40000093, // EventGrdGrnOrd
    EventGrdBluOrd = 0x40000094, // EventGrdBluOrd
    EventGrdVioOrd = 0x40000095, // EventGrdVioOrd
    EventGrdRedDom = 0x40000096, // EventGrdRedDom
    EventGrdYelDom = 0x40000097, // EventGrdYelDom
    EventGrdGrnDom = 0x40000098, // EventGrdGrnDom
    EventGrdBluDom = 0x40000099, // EventGrdBluDom
    EventGrdVioDom = 0x4000009A, // EventGrdVioDom
    EventGrdRedNeu = 0x4000009B, // EventGrdRedNeu
    EventGrdYelNeu = 0x4000009C, // EventGrdYelNeu
    EventGrdGrnNeu = 0x4000009D, // EventGrdGrnNeu
    EventGrdBluNeu = 0x4000009E, // EventGrdBluNeu
    EventGrdVioNeu = 0x4000009F, // EventGrdVioNeu
    EventACryptTrap1 = 0x400000A0, // EventACryptTrap1
    EventACryptTrap2 = 0x400000A1, // EventACryptTrap2
    EventACryptTrap3 = 0x400000A2, // EventACryptTrap3
    EventACryptTrap4 = 0x400000A3, // EventACryptTrap4
    EventACryptTrap5 = 0x400000A4, // EventACryptTrap5
    EventACryptTrap6 = 0x400000A5, // EventACryptTrap6
    EventACryptTrap7 = 0x400000A6, // EventACryptTrap7
    EventACryptTrap8 = 0x400000A7, // EventACryptTrap8
    EventACryptTrap9 = 0x400000A8, // EventACryptTrap9
    EventACryptTrap10 = 0x400000A9, // EventACryptTrap10
    EventACryptTrap11 = 0x400000AA, // EventACryptTrap11
    EventACryptTrap12 = 0x400000AB, // EventACryptTrap12
    EventHSlayerSpawn1A = 0x400000AC, // EventHSlayerSpawn1A
    EventHSlayerSpawn1B = 0x400000AD, // EventHSlayerSpawn1B
    EventHSlayerSpawn1C = 0x400000AE, // EventHSlayerSpawn1C
    EventHSlayerSpawn2A = 0x400000AF, // EventHSlayerSpawn2A
    EventHSlayerSpawn2B = 0x400000B0, // EventHSlayerSpawn2B
    EventHSlayerSpawn2C = 0x400000B1, // EventHSlayerSpawn2C
    EventHSlayerSpawn3A = 0x400000B2, // EventHSlayerSpawn3A
    EventHSlayerSpawn3B = 0x400000B3, // EventHSlayerSpawn3B
    EventHSlayerSpawn3C = 0x400000B4, // EventHSlayerSpawn3C
    EventHSlayerSpawn4A = 0x400000B5, // EventHSlayerSpawn4A
    EventHSlayerSpawn4B = 0x400000B6, // EventHSlayerSpawn4B
    EventHSlayerSpawn4C = 0x400000B7, // EventHSlayerSpawn4C
    EventHSlayerSpawn5A = 0x400000B8, // EventHSlayerSpawn5A
    EventHSlayerSpawn5B = 0x400000B9, // EventHSlayerSpawn5B
    EventHSlayerSpawn5C = 0x400000BA, // EventHSlayerSpawn5C
    EventHSlayerSpawn6A = 0x400000BB, // EventHSlayerSpawn6A
    EventHSlayerSpawn6B = 0x400000BC, // EventHSlayerSpawn6B
    EventHSlayerSpawn6C = 0x400000BD, // EventHSlayerSpawn6C
    EventHSlayerSpawn7A = 0x400000BE, // EventHSlayerSpawn7A
    EventHSlayerSpawn7B = 0x400000BF, // EventHSlayerSpawn7B
    EventHSlayerSpawn7C = 0x400000C0, // EventHSlayerSpawn7C
    EventHSlayerSpawn8A = 0x400000C1, // EventHSlayerSpawn8A
    EventHSlayerSpawn8B = 0x400000C2, // EventHSlayerSpawn8B
    EventHSlayerSpawn8C = 0x400000C3, // EventHSlayerSpawn8C
    EventDSwordSpawn = 0x400000C4, // EventDSwordSpawn

    BugHuntWorkerA1 = 0x400000E3, // BugHuntWorkerA1
    BugHuntWorkerA2 = 0x400000E4, // BugHuntWorkerA2
    BugHuntWorkerA3 = 0x400000E5, // BugHuntWorkerA3
    BugHuntWorkerB1 = 0x400000E6, // BugHuntWorkerB1
    BugHuntWorkerB2 = 0x400000E7, // BugHuntWorkerB2
    BugHuntWorkerB3 = 0x400000E8, // BugHuntWorkerB3
    BugHuntSoldierA1 = 0x400000E9, // BugHuntSoldierA1
    BugHuntSoldierA2 = 0x400000EA, // BugHuntSoldierA2
    BugHuntSoldierA3 = 0x400000EB, // BugHuntSoldierA3
    BugHuntSoldierB1 = 0x400000EC, // BugHuntSoldierB1
    BugHuntSoldierB2 = 0x400000ED, // BugHuntSoldierB2
    BugHuntSoldierB3 = 0x400000EE, // BugHuntSoldierB3
    BugHuntImmatureDroneA1 = 0x400000EF, // BugHuntImmatureDroneA1
    BugHuntImmatureDroneA2 = 0x400000F0, // BugHuntImmatureDroneA2
    BugHuntImmatureDroneA3 = 0x400000F1, // BugHuntImmatureDroneA3
    BugHuntImmatureDroneB1 = 0x400000F2, // BugHuntImmatureDroneB1
    BugHuntImmatureDroneB2 = 0x400000F3, // BugHuntImmatureDroneB2
    BugHuntImmatureDroneB3 = 0x400000F4, // BugHuntImmatureDroneB3
    BugHuntImmatureGrubTenderA1 = 0x400000F5, // BugHuntImmatureGrubTenderA1
    BugHuntImmatureGrubTenderA2 = 0x400000F6, // BugHuntImmatureGrubTenderA2
    BugHuntImmatureGrubTenderA3 = 0x400000F7, // BugHuntImmatureGrubTenderA3
    BugHuntImmatureGrubTenderB1 = 0x400000F8, // BugHuntImmatureGrubTenderB1
    BugHuntImmatureGrubTenderB2 = 0x400000F9, // BugHuntImmatureGrubTenderB2
    BugHuntImmatureGrubTenderB3 = 0x400000FA, // BugHuntImmatureGrubTenderB3
    BugHuntNobleA1 = 0x400000FB, // BugHuntNobleA1
    BugHuntNobleA2 = 0x400000FC, // BugHuntNobleA2
    BugHuntNobleA3 = 0x400000FD, // BugHuntNobleA3
    BugHuntNobleB1 = 0x400000FE, // BugHuntNobleB1
    BugHuntNobleB2 = 0x400000FF, // BugHuntNobleB2
    BugHuntNobleB3 = 0x40000100, // BugHuntNobleB3
    WarCenter = 0x40000101, // WarCenter
    WarCenterA = 0x40000102, // WarCenterA
    WarCenterB = 0x40000103, // WarCenterB
    WarCenterC = 0x40000104, // WarCenterC
    WarCenterD = 0x40000105, // WarCenterD
    WarAer3 = 0x40000106, // WarAer3
    WarAer3A = 0x40000107, // WarAer3A
    WarAer3B = 0x40000108, // WarAer3B
    WarAer3C = 0x40000109, // WarAer3C
    WarAer3Emp = 0x4000010A, // WarAer3Emp
    WarAer2 = 0x4000010B, // WarAer2
    WarAer2A = 0x4000010C, // WarAer2A
    WarAer2B = 0x4000010D, // WarAer2B
    WarAer2C = 0x4000010E, // WarAer2C
    WarAer2Emp = 0x4000010F, // WarAer2Emp
    WarAer1 = 0x40000110, // WarAer1
    WarAer1A = 0x40000111, // WarAer1A
    WarAer1B = 0x40000112, // WarAer1B
    WarAer1C = 0x40000113, // WarAer1C
    WarAer1Emp = 0x40000114, // WarAer1Emp
    WarAer0 = 0x40000115, // WarAer0
    WarAer0A = 0x40000116, // WarAer0A
    WarAer0B = 0x40000117, // WarAer0B
    WarAer0C = 0x40000118, // WarAer0C
    WarAer0Emp = 0x40000119, // WarAer0Emp
    WarArc3 = 0x4000011A, // WarArc3
    WarArc3A = 0x4000011B, // WarArc3A
    WarArc3B = 0x4000011C, // WarArc3B
    WarArc3Emp = 0x4000011D, // WarArc3Emp
    WarArc2 = 0x4000011E, // WarArc2
    WarArc2A = 0x4000011F, // WarArc2A
    WarArc2B = 0x40000120, // WarArc2B
    WarArc2C = 0x40000121, // WarArc2C
    WarArc2Emp = 0x40000122, // WarArc2Emp
    WarArc1 = 0x40000123, // WarArc1
    WarArc1A = 0x40000124, // WarArc1A
    WarArc1B = 0x40000125, // WarArc1B
    WarArc1C = 0x40000126, // WarArc1C
    WarArc1Emp = 0x40000127, // WarArc1Emp
    WarArc0 = 0x40000128, // WarArc0
    WarArc0A = 0x40000129, // WarArc0A
    WarArc0B = 0x4000012A, // WarArc0B
    WarArc0C = 0x4000012B, // WarArc0C
    WarArc0Emp = 0x4000012C, // WarArc0Emp
    WarGate1 = 0x4000012D, // WarGate1
    WarGate2 = 0x4000012E, // WarGate2
    WarGate3 = 0x4000012F, // WarGate3

    WarArcVictory = 0x40000131, // WarArcVictory

    EventIntervention = 0x40000133, // EventIntervention
    UndeadRecruiters = 0x40000134, // UndeadRecruiters
    WarArc3C = 0x40000135, // WarArc3C
    EventBugHuntThorstenA = 0x40000136, // EventBugHuntThorstenA
    EventBugHuntThorstenB = 0x40000137, // EventBugHuntThorstenB
    EventBugHuntThorstenC = 0x40000138, // EventBugHuntThorstenC
    EventBugHuntThorstenD = 0x40000139, // EventBugHuntThorstenD
    EventBugHuntThorstenE = 0x4000013A, // EventBugHuntThorstenE
    EventNFriendWispSpawn = 0x4000013B, // EventNFriendWispSpawn
    EventNFriendRabbitSpawn = 0x4000013C, // EventNFriendRabbitSpawn
    EventAscensionTrophy1 = 0x4000013D, // EventAscensionTrophy1
    EventAscensionTrophy2 = 0x4000013E, // EventAscensionTrophy2
    EventAscensionTrophy3 = 0x4000013F, // EventAscensionTrophy3
    EventAscensionVortex = 0x40000140, // EventAscensionVortex

    EventDestroySaelarGateA = 0x40000145, // EventDestroySaelarGateA
    EventDestroySaelarGateB = 0x40000146, // EventDestroySaelarGateB
    EventDestroySaelarGateC = 0x40000147, // EventDestroySaelarGateC
    EventDestroySaelarFinal = 0x40000148, // EventDestroySaelarFinal
    EventFireworks = 0x40000149, // EventFireworks
    EventSanta1 = 0x4000014A, // EventSanta1
    EventSanta2 = 0x4000014B, // EventSanta2
    EventSanta3 = 0x4000014C, // EventSanta3
    EventSanta4 = 0x4000014D, // EventSanta4
    EventSanta5 = 0x4000014E, // EventSanta5
    EventSanta6 = 0x4000014F, // EventSanta6
    EventSanta7 = 0x40000150, // EventSanta7
    EventSanta8 = 0x40000151, // EventSanta8
    EventSanta9 = 0x40000152, // EventSanta9
    CrystalAmplificationARepairTypeA = 0x40000153, // CrystalAmplificationARepairTypeA
    CrystalAmplificationARepairTypeB = 0x40000154, // CrystalAmplificationARepairTypeB
    CrystalAmplificationARepairTypeC = 0x40000155, // CrystalAmplificationARepairTypeC
    CrystalAmplificationBRepairTypeA = 0x40000156, // CrystalAmplificationBRepairTypeA
    CrystalAmplificationBRepairTypeB = 0x40000157, // CrystalAmplificationBRepairTypeB
    CrystalAmplificationBRepairTypeC = 0x40000158, // CrystalAmplificationBRepairTypeC
    CrystalAmplificationCRepairTypeA = 0x40000159, // CrystalAmplificationCRepairTypeA
    CrystalAmplificationCRepairTypeB = 0x4000015A, // CrystalAmplificationCRepairTypeB
    CrystalAmplificationCRepairTypeC = 0x4000015B, // CrystalAmplificationCRepairTypeC
    RenselmChaosDestroyed = 0x4000015C, // RenselmChaosDestroyed
    CrystalTappingADestroyed = 0x4000015D, // CrystalTappingADestroyed
    CrystalTappingBDestroyed = 0x4000015E, // CrystalTappingBDestroyed
    CrystalTappingCDestroyed = 0x4000015F, // CrystalTappingCDestroyed
    HahnainChaosDestroyed = 0x40000160, // HahnainChaosDestroyed
    CrystalTappingAllDestroyed = 0x40000161, // CrystalTappingAllDestroyed
    EventUncoverPastKingDead = 0x40000162, // EventUncoverPastKingDead
    EventUncoverPastLocA = 0x40000163, // EventUncoverPastLocA
    EventUncoverPastLocB = 0x40000164, // EventUncoverPastLocB
    EventUncoverPastLocC = 0x40000165, // EventUncoverPastLocC
    EventUncoverPastLocD = 0x40000166, // EventUncoverPastLocD
    EventUncoverPastLocE = 0x40000167, // EventUncoverPastLocE
    EventBDCActive = 0x40000168, // EventBDCActive
    EventBDCInactive = 0x40000169, // EventBDCInactive
    EventBDCWeakRocks = 0x4000016A, // EventBDCWeakRocks
    EventBDCStrongRocks = 0x4000016B, // EventBDCStrongRocks
    EventBDCLocationA = 0x4000016C, // EventBDCLocationA
    EventBDCLocationB = 0x4000016D, // EventBDCLocationB
    EventBDCLocationC = 0x4000016E, // EventBDCLocationC
    EventBDCLocationD = 0x4000016F, // EventBDCLocationD
    EventBDCLocationE = 0x40000170, // EventBDCLocationE
    EventBDCLocationF = 0x40000171, // EventBDCLocationF
    EventBDCPillarA = 0x40000172, // EventBDCPillarA
    EventBDCPillarB = 0x40000173, // EventBDCPillarB
    EventBDCPillarC = 0x40000174, // EventBDCPillarC
    EventBDCPillarD = 0x40000175, // EventBDCPillarD
    EventBDCPillarE = 0x40000176, // EventBDCPillarE
    EventBDCPillarF = 0x40000177, // EventBDCPillarF
    EventLostCompanyControlBlood = 0x40000178, // EventLostCompanyControlBlood
    EventLostCompanyControlFlask = 0x40000179, // EventLostCompanyControlFlask
    EventLostCompanyControlSeed = 0x4000017A, // EventLostCompanyControlSeed
    EventBroodMotherGuards = 0x4000017B, // EventBroodMotherGuards
    EventMoundSpiritGuide1 = 0x4000017C, // EventMoundSpiritGuide1
    EventMoundSpiritGuide2 = 0x4000017D, // EventMoundSpiritGuide2
    EventMoundSpiritGuide3 = 0x4000017E, // EventMoundSpiritGuide3
    EventMoundSpiritGuide4 = 0x4000017F, // EventMoundSpiritGuide4
    EventCFRiftSpawn = 0x40000180, // EventCFRiftSpawn
    EventWarCoreOrder = 0x40000181, // EventWarCoreOrder
    EventWarCoreShadow = 0x40000182, // EventWarCoreShadow
    EventWarCoreDominion = 0x40000183, // EventWarCoreDominion
    EventWarActive = 0x40000184, // EventWarActive
    EventWarCoreDoor = 0x40000185, // EventWarCoreDoor
    EventWarBattleground = 0x40000186, // EventWarBattleground
    EventWarOrder1 = 0x40000187, // EventWarOrder1
    EventWarOrder2 = 0x40000188, // EventWarOrder2
    EventWarOrder3 = 0x40000189, // EventWarOrder3
    EventWarOrder4 = 0x4000018A, // EventWarOrder4
    EventWarShadow1 = 0x4000018B, // EventWarShadow1
    EventWarShadow2 = 0x4000018C, // EventWarShadow2
    EventWarShadow3 = 0x4000018D, // EventWarShadow3
    EventWarShadow4 = 0x4000018E, // EventWarShadow4
    EventWarDominion1 = 0x4000018F, // EventWarDominion1
    EventWarDominion2 = 0x40000190, // EventWarDominion2
    EventWarDominion3 = 0x40000191, // EventWarDominion3
    EventWarDominion4 = 0x40000192, // EventWarDominion4
    EventWarMechO = 0x40000193, // EventWarMechO
    EventWarPortal1O = 0x40000194, // EventWarPortal1O
    EventWarPortal2O = 0x40000195, // EventWarPortal2O
    EventWarPortal3O = 0x40000196, // EventWarPortal3O
    EventWarBuff1O = 0x40000197, // EventWarBuff1O
    EventWarBuff2O = 0x40000198, // EventWarBuff2O
    EventWarBuff3O = 0x40000199, // EventWarBuff3O
    EventWarSiege1O = 0x4000019A, // EventWarSiege1O
    EventWarSiege2O = 0x4000019B, // EventWarSiege2O
    EventWarSiege3O = 0x4000019C, // EventWarSiege3O
    EventWarSiege4O = 0x4000019D, // EventWarSiege4O
    EventWarSiege5O = 0x4000019E, // EventWarSiege5O
    EventWarTurret1O = 0x4000019F, // EventWarTurret1O
    EventWarTurret2O = 0x400001A0, // EventWarTurret2O
    EventWarDoorO = 0x400001A1, // EventWarDoorO
    EventWarMechD = 0x400001A2, // EventWarMechD
    EventWarPortal1D = 0x400001A3, // EventWarPortal1D
    EventWarPortal2D = 0x400001A4, // EventWarPortal2D
    EventWarPortal3D = 0x400001A5, // EventWarPortal3D
    EventWarBuff1D = 0x400001A6, // EventWarBuff1D
    EventWarBuff2D = 0x400001A7, // EventWarBuff2D
    EventWarBuff3D = 0x400001A8, // EventWarBuff3D
    EventWarSiege1D = 0x400001A9, // EventWarSiege1D
    EventWarSiege2D = 0x400001AA, // EventWarSiege2D
    EventWarSiege3D = 0x400001AB, // EventWarSiege3D
    EventWarSiege4D = 0x400001AC, // EventWarSiege4D
    EventWarSiege5D = 0x400001AD, // EventWarSiege5D
    EventWarTurret1D = 0x400001AE, // EventWarTurret1D
    EventWarTurret2D = 0x400001AF, // EventWarTurret2D
    EventWarDoorD = 0x400001B0, // EventWarDoorD
    EventWarMechS = 0x400001B1, // EventWarMechS
    EventWarPortal1S = 0x400001B2, // EventWarPortal1S
    EventWarPortal2S = 0x400001B3, // EventWarPortal2S
    EventWarPortal3S = 0x400001B4, // EventWarPortal3S
    EventWarBuff1S = 0x400001B5, // EventWarBuff1S
    EventWarBuff2S = 0x400001B6, // EventWarBuff2S
    EventWarBuff3S = 0x400001B7, // EventWarBuff3S
    EventWarSiege1S = 0x400001B8, // EventWarSiege1S
    EventWarSiege2S = 0x400001B9, // EventWarSiege2S
    EventWarSiege3S = 0x400001BA, // EventWarSiege3S
    EventWarSiege4S = 0x400001BB, // EventWarSiege4S
    EventWarSiege5S = 0x400001BC, // EventWarSiege5S
    EventWarTurret1S = 0x400001BD, // EventWarTurret1S
    EventWarTurret2S = 0x400001BE, // EventWarTurret2S
    EventWarDoorS = 0x400001BF, // EventWarDoorS
    EventWarCamp1S = 0x400001C0, // EventWarCamp1S
    EventWarCamp2S = 0x400001C1, // EventWarCamp2S
    EventWarCamp3S = 0x400001C2, // EventWarCamp3S
    EventWarCamp4S = 0x400001C3, // EventWarCamp4S
    EventWarCamp5S = 0x400001C4, // EventWarCamp5S
    EventWarCamp6S = 0x400001C5, // EventWarCamp6S
    EventWarCamp7S = 0x400001C6, // EventWarCamp7S
    EventWarCamp8S = 0x400001C7, // EventWarCamp8S
    EventWarCamp1O = 0x400001C8, // EventWarCamp1O
    EventWarCamp2O = 0x400001C9, // EventWarCamp2O
    EventWarCamp3O = 0x400001CA, // EventWarCamp3O
    EventWarCamp4O = 0x400001CB, // EventWarCamp4O
    EventWarCamp5O = 0x400001CC, // EventWarCamp5O
    EventWarCamp6O = 0x400001CD, // EventWarCamp6O
    EventWarCamp7O = 0x400001CE, // EventWarCamp7O
    EventWarCamp8O = 0x400001CF, // EventWarCamp8O
    EventWarCamp1D = 0x400001D0, // EventWarCamp1D
    EventWarCamp2D = 0x400001D1, // EventWarCamp2D
    EventWarCamp3D = 0x400001D2, // EventWarCamp3D
    EventWarCamp4D = 0x400001D3, // EventWarCamp4D
    EventWarCamp5D = 0x400001D4, // EventWarCamp5D
    EventWarCamp6D = 0x400001D5, // EventWarCamp6D
    EventWarCamp7D = 0x400001D6, // EventWarCamp7D
    EventWarCamp8D = 0x400001D7, // EventWarCamp8D
    EventWarCorePortal1 = 0x400001D8, // EventWarCorePortal1
    EventWarCorePortal2 = 0x400001D9, // EventWarCorePortal2
    EventWarCorePortal3 = 0x400001DA, // EventWarCorePortal3
    EventWarCorePortal4 = 0x400001DB, // EventWarCorePortal4
    EventWarOrder5 = 0x400001DC, // EventWarOrder5
    EventWarShadow5 = 0x400001DD, // EventWarShadow5
    EventWarDominion5 = 0x400001DE, // EventWarDominion5
    EventCCPActive = 0x400001DF, // EventCCPActive
    EventCCPInactive = 0x400001E0, // EventCCPInactive
    EventCCPBossInactive = 0x400001E1, // EventCCPBossInactive
    EventCCPBossActive = 0x400001E2, // EventCCPBossActive
    EventCCPLocA = 0x400001E3, // EventCCPLocA
    EventCCPLocB = 0x400001E4, // EventCCPLocB
    EventCCPLocC = 0x400001E5, // EventCCPLocC
    EventCCPLocD = 0x400001E6, // EventCCPLocD
    Event_IS_Ball1 = 0x400001E7, // Event_IS_Ball1
    Event_IS_Ball2 = 0x400001E8, // Event_IS_Ball2
    Event_IS_Ball3 = 0x400001E9, // Event_IS_Ball3
    Event_IS_Ball4 = 0x400001EA, // Event_IS_Ball4
    Event_IS_Blight = 0x400001EB, // Event_IS_Blight
    Event_Despair_Trap1 = 0x400001EC, // Event_Despair_Trap1
    Event_Despair_Trap2 = 0x400001ED, // Event_Despair_Trap2
    Event_Despair_Trap3 = 0x400001EE, // Event_Despair_Trap3
    Event_Despair_Trap4 = 0x400001EF, // Event_Despair_Trap4
    Event_Despair_Trap5 = 0x400001F0, // Event_Despair_Trap5
    Event_Despair_Trap6 = 0x400001F1, // Event_Despair_Trap6
    Event_Despair_Trap7 = 0x400001F2, // Event_Despair_Trap7
    Event_Despair_Trap8 = 0x400001F3, // Event_Despair_Trap8
    EventKoreanArwicNPCS = 0x400001F4, // EventKoreanArwicNPCS
    Event_IS_DillosHome = 0x400001F5, // Event_IS_DillosHome
    Event_IS_DillosStolen = 0x400001F6, // Event_IS_DillosStolen
    EventWarMechResetO = 0x400001F7, // EventWarMechResetO
    EventWarMechResetS = 0x400001F8, // EventWarMechResetS
    EventWarMechResetD = 0x400001F9, // EventWarMechResetD
    EventEMOmiInvadeSO = 0x400001FA, // EventEMOmiInvadeSO
    EventEMOmiInvadeDO = 0x400001FB, // EventEMOmiInvadeDO
    EventEMLinInvadeSO = 0x400001FC, // EventEMLinInvadeSO
    EventEMLinInvadeDO = 0x400001FD, // EventEMLinInvadeDO
    EventEMOmiInvadeOS = 0x400001FE, // EventEMOmiInvadeOS
    EventEMOmiInvadeDS = 0x400001FF, // EventEMOmiInvadeDS
    EventEMLinInvadeOS = 0x40000200, // EventEMLinInvadeOS
    EventEMLinInvadeDS = 0x40000201, // EventEMLinInvadeDS
    EventEMOmiInvadeSD = 0x40000202, // EventEMOmiInvadeSD
    EventEMOmiInvadeOD = 0x40000203, // EventEMOmiInvadeOD
    EventEMLinInvadeSD = 0x40000204, // EventEMLinInvadeSD
    EventEMLinInvadeOD = 0x40000205, // EventEMLinInvadeOD
    EventEMFullBuffO = 0x40000206, // EventEMFullBuffO
    EventEM34BuffO = 0x40000207, // EventEM34BuffO
    EventEM12BuffO = 0x40000208, // EventEM12BuffO
    EventEM14BuffO = 0x40000209, // EventEM14BuffO
    EventEMFullBuffS = 0x4000020A, // EventEMFullBuffS
    EventEM34BuffS = 0x4000020B, // EventEM34BuffS
    EventEM12BuffS = 0x4000020C, // EventEM12BuffS
    EventEM14BuffS = 0x4000020D, // EventEM14BuffS
    EventEMFullBuffD = 0x4000020E, // EventEMFullBuffD
    EventEM34BuffD = 0x4000020F, // EventEM34BuffD
    EventEM12BuffD = 0x40000210, // EventEM12BuffD
    EventEM14BuffD = 0x40000211, // EventEM14BuffD
    EventEMBuffO = 0x40000212, // EventEMBuffO
    EventEMBuffS = 0x40000213, // EventEMBuffS
    EventEMBuffD = 0x40000214, // EventEMBuffD
    EventEMBuffResetO = 0x40000215, // EventEMBuffResetO
    EventEMBuffResetS = 0x40000216, // EventEMBuffResetS
    EventEMBuffResetD = 0x40000217, // EventEMBuffResetD
    Event_Rally = 0x40000218, // Event_Rally
    Event_RallyQuest1 = 0x40000219, // Event_RallyQuest1
    Event_RallyQuest2 = 0x4000021A, // Event_RallyQuest2
    Event_RallyQuest3 = 0x4000021B, // Event_RallyQuest3
    Event_RallyQuest4 = 0x4000021C, // Event_RallyQuest4
    Event_RallyQuest5 = 0x4000021D, // Event_RallyQuest5
    Event_RallyQuest6 = 0x4000021E, // Event_RallyQuest6
    Event_RallyQuest7 = 0x4000021F, // Event_RallyQuest7
    Event_RallyQuest8 = 0x40000220, // Event_RallyQuest8
    Event_RallyQuest9 = 0x40000221, // Event_RallyQuest9
    Event_RallyQuest10 = 0x40000222, // Event_RallyQuest10
    Event_RallyQuest11 = 0x40000223, // Event_RallyQuest11
    Event_RallyQuest12 = 0x40000224, // Event_RallyQuest12
    Event_RallyTimer1 = 0x40000225, // Event_RallyTimer1
    Event_RallyTimer2 = 0x40000226, // Event_RallyTimer2
    Event_RallyTimer3 = 0x40000227, // Event_RallyTimer3
    Event_RallyTimer4 = 0x40000228, // Event_RallyTimer4
    Event_RallyTimer5 = 0x40000229, // Event_RallyTimer5
    Event_RallyTimer6 = 0x4000022A, // Event_RallyTimer6
    Event_RallyTimer7 = 0x4000022B, // Event_RallyTimer7
    Event_RallyTimer8 = 0x4000022C, // Event_RallyTimer8
    Event_RallyTimer9 = 0x4000022D, // Event_RallyTimer9
    Event_RallyTimer10 = 0x4000022E, // Event_RallyTimer10
    EventOfficerCameronCragstone = 0x4000022F, // EventOfficerCameronCragstone
    EventOfficerCameronIkeras = 0x40000230, // EventOfficerCameronIkeras
    EventOfficerCameronLinvakTukal = 0x40000231, // EventOfficerCameronLinvakTukal
    EventKoreanTeachingStones = 0x40000232, // EventKoreanTeachingStones
    Event_SHRETHBase_Alarm = 0x40000233, // Event_SHRETHBase_Alarm
    EventGGWArchetype = 0x40000234, // EventGGWArchetype
    EventGGWInvasion = 0x40000235, // EventGGWInvasion
    EventGGWNormal = 0x40000236, // EventGGWNormal
    EventKoreanIndependence = 0x40000237, // EventKoreanIndependence
    EventTempleLightMatron = 0x40000238, // EventTempleLightMatron
    EventKoreanThanksgiving = 0x40000239, // EventKoreanThanksgiving
    EventBlackFerah1 = 0x4000023A, // EventBlackFerah1
    EventBlackFerah2 = 0x4000023B, // EventBlackFerah2
    EventBlackFerah3 = 0x4000023C, // EventBlackFerah3
    EventBlackFerah4 = 0x4000023D, // EventBlackFerah4
    EventBlackFerah5 = 0x4000023E, // EventBlackFerah5
    EventBlackFerah6 = 0x4000023F, // EventBlackFerah6
    EventCIReset = 0x40000240, // EventCIReset
    EventCIOsteth = 0x40000241, // EventCIOsteth
    EventCIOstethNormal = 0x40000242, // EventCIOstethNormal
    EventCIOmishan = 0x40000243, // EventCIOmishan
    EventCIOmishanNormal = 0x40000244, // EventCIOmishanNormal
    EventCILinvak = 0x40000245, // EventCILinvak
    EventCILinvakNormal = 0x40000246, // EventCILinvakNormal
    EventCIArramora = 0x40000247, // EventCIArramora
    EventCIArramoraNormal = 0x40000248, // EventCIArramoraNormal

    EventCISurfaceBoss = 0x4000024A, // EventCISurfaceBoss
    EventCIFacets = 0x4000024B, // EventCIFacets
    EventCIFacet1 = 0x4000024C, // EventCIFacet1
    EventCIFacet2 = 0x4000024D, // EventCIFacet2
    EventCIFacet3 = 0x4000024E, // EventCIFacet3
    EventCIFacet4 = 0x4000024F, // EventCIFacet4
    EventCIDungeonBoss = 0x40000250, // EventCIDungeonBoss
    EventMATNalicanaNormal = 0x40000251, // EventMATNalicanaNormal
    EventMATNalicanaPost = 0x40000252, // EventMATNalicanaPost

    EventCIOstethSurfacePortal = 0x40000254, // EventCIOstethSurfacePortal
    EventCIOstethSurfaceCenter = 0x40000255, // EventCIOstethSurfaceCenter
    EventCIOmishanSurfacePortal = 0x40000256, // EventCIOmishanSurfacePortal
    EventCIOmishanSurfaceCenter = 0x40000257, // EventCIOmishanSurfaceCenter
    EventCILinvakSurfacePortal = 0x40000258, // EventCILinvakSurfacePortal
    EventCILinvakSurfaceCenter = 0x40000259, // EventCILinvakSurfaceCenter
    EventCIArramoraSurfacePortal = 0x4000025A, // EventCIArramoraSurfacePortal
    EventCIArramoraSurfaceCenter = 0x4000025B, // EventCIArramoraSurfaceCenter
    EventWFCamp1 = 0x4000025C, // EventWFCamp1
    EventWFCamp2 = 0x4000025D, // EventWFCamp2
    EventWFCamp3 = 0x4000025E, // EventWFCamp3
    EventWFCamp4 = 0x4000025F, // EventWFCamp4
    EventTBCragstoneConstruction = 0x40000260, // EventTBCragstoneConstruction
    EventTBCragstoneTrait = 0x40000261, // EventTBCragstoneTrait
    EventTBCavendoConstruction = 0x40000262, // EventTBCavendoConstruction
    EventTBCavendoTrait = 0x40000263, // EventTBCavendoTrait
    EventTBIkerasConstruction = 0x40000264, // EventTBIkerasConstruction
    EventTBIkerasTrait = 0x40000265, // EventTBIkerasTrait
    EventTBHakataConstruction = 0x40000266, // EventTBHakataConstruction
    EventTBHakataTrait = 0x40000267, // EventTBHakataTrait
    EventTBLinvakConstruction = 0x40000268, // EventTBLinvakConstruction
    EventTBLinvakTrait = 0x40000269, // EventTBLinvakTrait
    EventTBOndekodoConstruction = 0x4000026A, // EventTBOndekodoConstruction
    EventTBOndekodoTrait = 0x4000026B, // EventTBOndekodoTrait
    EventATLActive = 0x4000026C, // EventATLActive
    EventATLInactive = 0x4000026D, // EventATLInactive
    EventTBCragstoneConstruction2 = 0x4000026E, // EventTBCragstoneConstruction2
    EventTBCragstoneConstruction3 = 0x4000026F, // EventTBCragstoneConstruction3
    EventTBCragstoneConstruction4 = 0x40000270, // EventTBCragstoneConstruction4
    EventTBCragstoneWeapon = 0x40000271, // EventTBCragstoneWeapon
    EventTBCragstoneArmor = 0x40000272, // EventTBCragstoneArmor
    EventTBCragstoneTools = 0x40000273, // EventTBCragstoneTools
    EventTBCragstoneCuriosity = 0x40000274, // EventTBCragstoneCuriosity
    EventTBCavendoConstruction2 = 0x40000275, // EventTBCavendoConstruction2
    EventTBCavendoConstruction3 = 0x40000276, // EventTBCavendoConstruction3
    EventTBCavendoConstruction4 = 0x40000277, // EventTBCavendoConstruction4
    EventTBCavendoWeapon = 0x40000278, // EventTBCavendoWeapon
    EventTBCavendoArmor = 0x40000279, // EventTBCavendoArmor
    EventTBCavendoTools = 0x4000027A, // EventTBCavendoTools
    EventTBCavendoCuriosity = 0x4000027B, // EventTBCavendoCuriosity
    EventTBIkerasConstruction2 = 0x4000027C, // EventTBIkerasConstruction2
    EventTBIkerasConstruction3 = 0x4000027D, // EventTBIkerasConstruction3
    EventTBIkerasConstruction4 = 0x4000027E, // EventTBIkerasConstruction4
    EventTBIkerasWeapon = 0x4000027F, // EventTBIkerasWeapon
    EventTBIkerasArmor = 0x40000280, // EventTBIkerasArmor
    EventTBIkerasTools = 0x40000281, // EventTBIkerasTools
    EventTBIkerasCuriosity = 0x40000282, // EventTBIkerasCuriosity
    EventTBHakataConstruction2 = 0x40000283, // EventTBHakataConstruction2
    EventTBHakataConstruction3 = 0x40000284, // EventTBHakataConstruction3
    EventTBHakataConstruction4 = 0x40000285, // EventTBHakataConstruction4
    EventTBHakataWeapon = 0x40000286, // EventTBHakataWeapon
    EventTBHakataArmor = 0x40000287, // EventTBHakataArmor
    EventTBHakataTools = 0x40000288, // EventTBHakataTools
    EventTBHakataCuriosity = 0x40000289, // EventTBHakataCuriosity
    EventTBLinvakConstruction2 = 0x4000028A, // EventTBLinvakConstruction2
    EventTBLinvakConstruction3 = 0x4000028B, // EventTBLinvakConstruction3
    EventTBLinvakConstruction4 = 0x4000028C, // EventTBLinvakConstruction4
    EventTBLinvakWeapon = 0x4000028D, // EventTBLinvakWeapon
    EventTBLinvakArmor = 0x4000028E, // EventTBLinvakArmor
    EventTBLinvakTools = 0x4000028F, // EventTBLinvakTools
    EventTBLinvakCuriosity = 0x40000290, // EventTBLinvakCuriosity
    EventTBOndekodoConstruction2 = 0x40000291, // EventTBOndekodoConstruction2
    EventTBOndekodoConstruction3 = 0x40000292, // EventTBOndekodoConstruction3
    EventTBOndekodoConstruction4 = 0x40000293, // EventTBOndekodoConstruction4
    EventTBOndekodoWeapon = 0x40000294, // EventTBOndekodoWeapon
    EventTBOndekodoArmor = 0x40000295, // EventTBOndekodoArmor
    EventTBOndekodoTools = 0x40000296, // EventTBOndekodoTools
    EventTBOndekodoCuriosity = 0x40000297, // EventTBOndekodoCuriosity
    EventOFCFalatacotA = 0x40000298, // EventOFCFalatacotA
    EventOFCFalatacotB = 0x40000299, // EventOFCFalatacotB
    EventOFCFalatacotC = 0x4000029A, // EventOFCFalatacotC
    EventOFCFalatacotD = 0x4000029B, // EventOFCFalatacotD
    EventOFCOlthoiA = 0x4000029C, // EventOFCOlthoiA
    EventOFCOlthoiB = 0x4000029D, // EventOFCOlthoiB
    EventOFCOlthoiC = 0x4000029E, // EventOFCOlthoiC
    EventOFCOlthoiD = 0x4000029F, // EventOFCOlthoiD
    EventOFCOlthoiEpic = 0x400002A0, // EventOFCOlthoiEpic
    EventOFCFalatacotEpic = 0x400002A1, // EventOFCFalatacotEpic
    EventOFCFalatacotInvasionA = 0x400002A2, // EventOFCFalatacotInvasionA
    EventOFCFalatacotInvasionB = 0x400002A3, // EventOFCFalatacotInvasionB
    EventOFCFalatacotInvasionC = 0x400002A4, // EventOFCFalatacotInvasionC
    EventOFCOlthoiInvasionA = 0x400002A5, // EventOFCOlthoiInvasionA
    EventOFCOlthoiInvasionB = 0x400002A6, // EventOFCOlthoiInvasionB
    EventOFCOlthoiInvasionC = 0x400002A7, // EventOFCOlthoiInvasionC
    EventOFCDoCount = 0x400002A8, // EventOFCDoCount
    EventOFCFinal = 0x400002A9, // EventOFCFinal
    EventOQ1Consort3 = 0x400002AA, // EventOQ1Consort3
    EventOQ1Consort2 = 0x400002AB, // EventOQ1Consort2
    EventOQ1Consort1 = 0x400002AC, // EventOQ1Consort1
    EventOQ2Consort3 = 0x400002AD, // EventOQ2Consort3
    EventOQ2Consort2 = 0x400002AE, // EventOQ2Consort2
    EventOQ2Consort1 = 0x400002AF, // EventOQ2Consort1
    EventOQ3Consort3 = 0x400002B0, // EventOQ3Consort3
    EventOQ3Consort2 = 0x400002B1, // EventOQ3Consort2
    EventOQ3Consort1 = 0x400002B2, // EventOQ3Consort1
    EventOQ4Consort3 = 0x400002B3, // EventOQ4Consort3
    EventOQ4Consort2 = 0x400002B4, // EventOQ4Consort2
    EventOQ4Consort1 = 0x400002B5, // EventOQ4Consort1
    EventOQ5Consort3 = 0x400002B6, // EventOQ5Consort3
    EventOQ5Consort2 = 0x400002B7, // EventOQ5Consort2
    EventOQ5Consort1 = 0x400002B8, // EventOQ5Consort1
    EventOQ1Portal = 0x400002B9, // EventOQ1Portal
    EventOQ2Portal = 0x400002BA, // EventOQ2Portal
    EventOQ3Portal = 0x400002BB, // EventOQ3Portal
    EventOQ4Portal = 0x400002BC, // EventOQ4Portal
    EventOQ5Portal = 0x400002BD, // EventOQ5Portal
    EventSHA = 0x400002BE, // EventSHA
    EventSHB = 0x400002BF, // EventSHB
    EventSHC = 0x400002C0, // EventSHC
    EventSHD = 0x400002C1, // EventSHD
    EventIRA = 0x400002C2, // EventIRA
    EventIRB = 0x400002C3, // EventIRB
    EventIRC = 0x400002C4, // EventIRC
    EventIRD = 0x400002C5, // EventIRD
    EventOFCFalatacotInvasionA1 = 0x400002C6, // EventOFCFalatacotInvasionA1
    EventOFCFalatacotInvasionA2 = 0x400002C7, // EventOFCFalatacotInvasionA2
    EventOFCFalatacotInvasionB1 = 0x400002C8, // EventOFCFalatacotInvasionB1
    EventOFCFalatacotInvasionB2 = 0x400002C9, // EventOFCFalatacotInvasionB2
    EventOFCFalatacotInvasionC1 = 0x400002CA, // EventOFCFalatacotInvasionC1
    EventOFCFalatacotInvasionC2 = 0x400002CB, // EventOFCFalatacotInvasionC2
    EventOFCOlthoiInvasionA1 = 0x400002CC, // EventOFCOlthoiInvasionA1
    EventOFCOlthoiInvasionA2 = 0x400002CD, // EventOFCOlthoiInvasionA2
    EventOFCOlthoiInvasionB1 = 0x400002CE, // EventOFCOlthoiInvasionB1
    EventOFCOlthoiInvasionB2 = 0x400002CF, // EventOFCOlthoiInvasionB2
    EventOFCOlthoiInvasionC1 = 0x400002D0, // EventOFCOlthoiInvasionC1
    EventOFCOlthoiInvasionC2 = 0x400002D1, // EventOFCOlthoiInvasionC2
    EventLODPortal = 0x400002D2, // EventLODPortal
    EventGrievverFolly = 0x400002D3, // EventGrievverFolly
    EventFoundryIgnitionDoor = 0x400002D4, // EventFoundryIgnitionDoor
    EventFoundryIgnitionRoom = 0x400002D5, // EventFoundryIgnitionRoom
    EventForgePortalDoor = 0x400002D6, // EventForgePortalDoor
    EventKvKAnywhere = 0x400002D7, // EventKvKAnywhere
    ShinokoPeddler = 0x400002D8, // ShinokoPeddler
    EventBlazingDrudgeFestival = 0x400002D9, // EventBlazingDrudgeFestival
    ShinokoFestival = 0x400002DA, // ShinokoFestival
}
