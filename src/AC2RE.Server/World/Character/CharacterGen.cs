using AC2RE.Definitions;
using System;
using System.Collections.Generic;

namespace AC2RE.Server;

internal static class CharacterGen {

    //TODO: Not sure how this mapping is actually determined
    private static readonly Dictionary<SpeciesType, uint> SPECIES_TO_UNK_SKILLS = new() {
        { SpeciesType.Lugian, (1 << 1) },
        { SpeciesType.Human, (1 << 2) },
        { SpeciesType.Tumerok, (1 << 3) },
        { SpeciesType.Drudge, (1 << 15) },
        { SpeciesType.Empyrean, (1 << 20) },
    };

    public static WorldObject createCharacterObject(World world, Position startPos, string name, SpeciesType species, SexType sex, Dictionary<PhysiqueType, float> physiqueTypeValues) {
        Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap = new();

        CharacterGenSystem characterGenSystem = world.contentManager.getCharacterGenSystem();
        CharGenMatrix charGenMatrix = world.contentManager.getCharGenMatrix();

        foreach (KeyValuePair<PhysiqueType, List<AppearanceProfile>> physiqueAndAppProfiles in charGenMatrix.physiqueTypeModifierTable[species][sex]) {
            PhysiqueType physiqueType = physiqueAndAppProfiles.Key;
            List<AppearanceProfile> appProfiles = physiqueAndAppProfiles.Value;

            if (!appProfileMap.TryGetValue(physiqueType, out Dictionary<float, Tuple<AppearanceKey, DataId>>? modifierToApp)) {
                modifierToApp = new();
                appProfileMap[physiqueType] = modifierToApp;
            }

            foreach (AppearanceProfile appProfile in appProfiles) {
                modifierToApp[appProfile.modifier] = new(appProfile.appKey, appProfile.aprDid);
            }
        }

        GMRaceSexInfo raceSexInfo = charGenMatrix.raceSexInfoTable[new(species, sex)];

        Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos = new();
        foreach (KeyValuePair<PhysiqueType, float> physiqueTypeValue in physiqueTypeValues) {
            if (appProfileMap.TryGetValue(physiqueTypeValue.Key, out Dictionary<float, Tuple<AppearanceKey, DataId>>? modifierToAppProfiles)) {
                (AppearanceKey appKey, DataId appDid) = modifierToAppProfiles[physiqueTypeValue.Value];
                if (appDid != DataId.NULL) {
                    if (!appearanceInfos.TryGetValue(appDid, out Dictionary<AppearanceKey, float>? appToModfier)) {
                        appToModfier = new();
                        appearanceInfos[appDid] = appToModfier;
                    }
                    appToModfier[appKey] = physiqueTypeValue.Value;
                }
            }
        }

        WorldObject character = world.objectManager.create(InstanceId.IdType.Player, characterGenSystem.playerEntityDid, raceSexInfo.physObjDid, true);
        setCharacterPhysics(character, startPos);
        setCharacterVisual(character, appProfileMap, appearanceInfos);
        setCharacterQualities(character, species, sex);

        character.name = new(name + (sex == SexType.Male ? " [M]" : " [F]"));

        addStartingSkills(world, character, charGenMatrix, species);

        createStartingInventory(world, character, charGenMatrix, species, appearanceInfos);

        return character;
    }

    private static void setCharacterPhysics(WorldObject character, Position startPos) {
        character.setSliderValue(1073741834, 1.0f, 0.0f);
        character.pos = startPos;
    }

    private static void setCharacterVisual(WorldObject character, Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap, Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos) {
        character.visualScale = new(0.9107999f, 0.9107999f, 0.98999995f);
        character.globalAppearanceModifiers = new() {
            key = PartGroupDataDesc.PartGroupKey.EntireTree,
            appearanceInfos = appearanceInfos,
        };
    }

    private static void setCharacterQualities(WorldObject character, SpeciesType species, SexType sex) {
        character.capacity = 78;
        character.species = species;
        character.sex = sex;
        character.playerClass = 2;
        character.level = 7;
        character.money = 321;
        character.health = 100;
        character.healthMax = 100;
        character.healthRegen = 1.0f;
        character.vigor = 100;
        character.vigorMax = 100;
        character.vigorRegen = 1.0f;
        character.xp = 902;
        character.xpAvailable = 902;
        character.craftXp = 80;
        character.craftXpAvailable = 80;
    }

    private static void addStartingSkills(World world, WorldObject character, CharGenMatrix charGenMatrix, SpeciesType species) {
        List<SkillProfile> startingSkillProfiles = charGenMatrix.startingSkillsTable[SPECIES_TO_UNK_SKILLS[species]];
        foreach (SkillProfile startingSkillProfile in startingSkillProfiles) {
            // TODO: Remove starting credits
            character.skillRepo.skillCredits = 5;
            Skill skill = world.contentManager.getSkill(startingSkillProfile.skillId);
            character.skillRepo.skills[startingSkillProfile.skillId] = new() {
                levelCached = (uint)startingSkillProfile.level,
                timeCached = world.serverTime.time,
                xpAllocated = world.contentManager.getAdvancementTable(skill.advTableDid).map[startingSkillProfile.level],
                flags = SkillInfo.Flag.IsTrained,
                skillId = startingSkillProfile.skillId,
            };
        }
    }

    private static void createStartingInventory(World world, WorldObject character, CharGenMatrix charGenMatrix, SpeciesType species, Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos) {
        List<StartInvData> startInvItems = charGenMatrix.startingInventoryTable[species][0][0];
        foreach (StartInvData startInvItem in startInvItems) {
            WorldObject item = world.objectManager.create(InstanceId.IdType.Dynamic, startInvItem.entityDid, DataId.NULL, true);

            WState clothingWeenieState = world.contentManager.getWeenieStateFromEntityDid(item.entityDid);
            if (clothingWeenieState.package is Clothing clothing) {
                foreach (DataId appearanceDid in clothing.wornAppearanceDidHash.Values) {
                    if (appearanceInfos.TryGetValue(appearanceDid, out Dictionary<AppearanceKey, float>? appearanceModifiers)) {
                        item.globalAppearanceModifiers = new() {
                            key = PartGroupDataDesc.PartGroupKey.EntireTree,
                            appearanceInfos = new() { { appearanceDid, new(appearanceModifiers) } },
                        };
                    }
                }
            }

            item.setContainer(character);

            if (startInvItem.equipped) {
                WorldObject.autoEquip(world, character, item);
            }
        }

        WorldObject mountItem = world.objectManager.create(InstanceId.IdType.Dynamic, new(0x470014CB), DataId.NULL, true);

        mountItem.setContainer(character);
    }
}
