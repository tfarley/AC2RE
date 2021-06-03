﻿using AC2RE.Definitions;
using System;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal static class CharacterGen {

        //TODO: Not sure how this mapping is actually determined
        private static readonly Dictionary<SpeciesType, uint> SPECIES_TO_UNK_SKILLS = new() {
            { SpeciesType.LUGIAN, (1 << 1) },
            { SpeciesType.HUMAN, (1 << 2) },
            { SpeciesType.TUMEROK, (1 << 3) },
            { SpeciesType.MOSSWART, (1 << 15) },
            { SpeciesType.EMPYREAN, (1 << 20) },
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

            WorldObject character = world.objectManager.create(characterGenSystem.playerEntityDid, raceSexInfo.physObjDid, true);
            setCharacterPhysics(character, startPos);
            setCharacterVisual(character, appProfileMap, appearanceInfos);
            setCharacterQualities(character, species, sex);

            character.name = new(name);

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
                key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
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
                    flags = SkillInfo.Flag.TRAINED,
                    skillId = startingSkillProfile.skillId,
                };
            }
        }

        private static void createStartingInventory(World world, WorldObject character, CharGenMatrix charGenMatrix, SpeciesType species, Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos) {
            List<StartInvData> startInvItems = charGenMatrix.startingInventoryTable[species][0][0];
            foreach (StartInvData startInvItem in startInvItems) {
                WorldObject item = world.objectManager.create(startInvItem.entityDid, DataId.NULL, true);

                DataId weenieStateDid = new(0x71000000 + item.entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
                WState clothingWeenieState = world.contentManager.getWeenieState(weenieStateDid);
                if (clothingWeenieState.package is Clothing clothing) {
                    DataId appearanceDid = clothing.wornAppearanceDidHash[new(character.species!, character.sex!)];
                    Dictionary<DataId, Dictionary<AppearanceKey, float>> itemAppearanceInfos = new();
                    if (appearanceInfos.TryGetValue(appearanceDid, out Dictionary<AppearanceKey, float>? appearances)) {
                        itemAppearanceInfos[appearanceDid] = appearances;
                    }
                    item.globalAppearanceModifiers = new() {
                        key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
                        appearanceInfos = itemAppearanceInfos,
                    };
                }

                item.setContainer(character);

                if (startInvItem.equipped) {
                    character.equip(item.preferredInvLoc, item);
                }
            }
        }
    }
}
