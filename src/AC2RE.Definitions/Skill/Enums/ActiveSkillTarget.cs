using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum ActiveSkillTarget : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        SELF = 1 << 0, // 0x00000001, ActiveSkill::ValidTargetSelf
        SELF_ITEM = 1 << 1, // 0x00000002, ActiveSkill::ValidTargetSelfItem
        EXTERNAL_ITEM = 1 << 2, // 0x00000004, ActiveSkill::ValidTargetExternalItem
        MONSTER_ITEM = 1 << 3, // 0x00000008, ActiveSkill::ValidTargetMonsterItem
        PLAYER_ITEM = 1 << 4, // 0x00000010, ActiveSkill::ValidTargetPlayerItem
        PLAYER_CORPSE = 1 << 5, // 0x00000020, ActiveSkill::ValidTargetPlayerCorpse
        MONSTER_CORPSE = 1 << 6, // 0x00000040, ActiveSkill::ValidTargetMonsterCorpse
        MONSTER = 1 << 7, // 0x00000080, ActiveSkill::ValidTargetMonster
        PLAYER = 1 << 8, // 0x00000100, ActiveSkill::ValidTargetPlayer
        NPC = 1 << 9, // 0x00000200, ActiveSkill::ValidTargetNPC
        SAME_FELLOWSHIP_ONLY = 1 << 10, // 0x00000400, ActiveSkill::ValidTargetSameFellowshipOnly
        NOT_SAME_FELLOWSHIP_ONLY = 1 << 11, // 0x00000800, ActiveSkill::ValidTargetNotSameFellowshipOnly
        SAME_ALLEGIANCE_ONLY = 1 << 12, // 0x00001000, ActiveSkill::ValidTargetSameAllegianceOnly
        NOT_SAME_ALLEGIANCE_ONLY = 1 << 13, // 0x00002000, ActiveSkill::ValidTargetNotSameAllegianceOnly
        GREATER_THAN_OR_EQUAL_TO_ALLEGIANCE_RANK = 1 << 14, // 0x00004000, ActiveSkill::ValidTargetGreaterThanOrEqualToAllegianceRank
        SAME_FACTION_ONLY = 1 << 15, // 0x00008000, ActiveSkill::ValidTargetSameFactionOnly
        NOT_SAME_FACTION_ONLY = 1 << 16, // 0x00010000, ActiveSkill::ValidTargetNotSameFactionOnly
        FACTION_ONLY = 1 << 17, // 0x00020000, ActiveSkill::ValidTargetFactionOnly
        MIN_LEVEL = 1 << 18, // 0x00040000,  ActiveSkill::ValidTargetMinLevel
        MAX_LEVEL = 1 << 19, // 0x00080000, ActiveSkill::ValidTargetMaxLevel
        UNOWNED_PET = 1 << 20, // 0x00100000, ActiveSkill::ValidTargetUnownedPet
        OWNED_PET = 1 << 21, // 0x00200000, ActiveSkill::ValidTargetOwnedPet
        PET_ONLY = 1 << 22, // 0x00400000, ActiveSkill::ValidTargetPetOnly
        WEENIE_TYPE = 1 << 23, // 0x00800000, ActiveSkill::ValidTargetWeenieType
        PET_SUMMONER = 1 << 24, // 0x01000000, ActiveSkill::ValidTargetPetSummoner
        RESURRECTABLE_CORPSE = 1 << 25, // 0x02000000, ActiveSkill::ValidTargetResurrectableCorpse
        NOT_AI_SUPER_CLASS = 1 << 26, // 0x04000000, ActiveSkill::ValidTargetNotAISuperClass
        SKILL_TARGET_FLAGS_ONLY = 1 << 27, // 0x08000000, ActiveSkill::ValidTargetSkillTargetFlagsOnly
        EQUIPPED_ITEM = 1 << 28, // 0x10000000, ActiveSkill::ValidTargetEquippedItem
    }
}
