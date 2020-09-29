using AC2E.Def;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Server {

    internal static class CharacterGen {

        public static WorldObject createCharacterObject(WorldObjectManager objectManager, ContentManager contentManager, InventoryManager inventoryManager, Position startPos, string name, SpeciesType species, SexType sex, Dictionary<PhysiqueType, float> physiqueTypeValues) {
            Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap = new Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>>();

            CharacterGenSystem characterGenSystem = contentManager.getCharacterGenSystem();
            CharGenMatrix charGenMatrix = contentManager.getCharGenMatrix();

            foreach (KeyValuePair<uint, RList<IPackage>> physiqueAndAppProfiles in charGenMatrix.physiqueTypeModifierTable[(uint)species].to<ARHash<IPackage>>()[(uint)sex].to<RList<IPackage>>()) {
                PhysiqueType physiqueType = (PhysiqueType)physiqueAndAppProfiles.Key;
                RList<AppearanceProfile> appProfiles = physiqueAndAppProfiles.Value.to<AppearanceProfile>();

                if (!appProfileMap.TryGetValue(physiqueType, out Dictionary<float, Tuple<AppearanceKey, DataId>> modifierToApp)) {
                    modifierToApp = new Dictionary<float, Tuple<AppearanceKey, DataId>>();
                    appProfileMap[physiqueType] = modifierToApp;
                }

                foreach (AppearanceProfile appProfile in appProfiles) {
                    modifierToApp[appProfile.modifier] = new Tuple<AppearanceKey, DataId>(appProfile.appKey, appProfile.aprDid);
                }
            }

            GMRaceSexInfo raceSexInfo = charGenMatrix.raceSexInfoTable[(uint)species | (uint)sex];

            Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos = new Dictionary<DataId, Dictionary<AppearanceKey, float>>();
            foreach (KeyValuePair<PhysiqueType, float> physiqueTypeValue in physiqueTypeValues) {
                if (appProfileMap.TryGetValue(physiqueTypeValue.Key, out Dictionary<float, Tuple<AppearanceKey, DataId>> modifierToAppProfiles)) {
                    (AppearanceKey appKey, DataId appDid) = modifierToAppProfiles[physiqueTypeValue.Value];
                    if (appDid != DataId.NULL) {
                        if (!appearanceInfos.TryGetValue(appDid, out Dictionary<AppearanceKey, float> appToModfier)) {
                            appToModfier = new Dictionary<AppearanceKey, float>();
                            appearanceInfos[appDid] = appToModfier;
                        }
                        appToModfier[appKey] = physiqueTypeValue.Value;
                    }
                }
            }

            WorldObject character = objectManager.create();
            ObjectGen.applyWeenie(character, contentManager, characterGenSystem.playerEntityDid);
            ObjectGen.applyPhysics(character, contentManager, raceSexInfo.physObjDid);
            setCharacterPhysics(character.physics, startPos);
            setCharacterVisual(character.visual, appProfileMap, appearanceInfos);
            setCharacterQualities(character.qualities, species, sex, raceSexInfo.physObjDid);

            character.qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.NAME;
            character.qualities.weenieDesc.name = new StringInfo(name);

            createStartingInventory(objectManager, contentManager, inventoryManager, character, charGenMatrix, species, appearanceInfos);

            return character;
        }

        private static void setCharacterPhysics(PhysicsDesc physics, Position startPos) {
            physics.packFlags |= PhysicsDesc.PackFlag.SLIDERS | PhysicsDesc.PackFlag.MODE | PhysicsDesc.PackFlag.POSITION | PhysicsDesc.PackFlag.VELOCITY_SCALE;
            physics.sliders = new Dictionary<uint, PhysicsDesc.SliderData> {
                { 1073741834, new PhysicsDesc.SliderData {
                    value = 1.0f,
                    velocity = 0.0f,
                } }
            };
            physics.modeId = 1073741825;
            physics.pos = startPos;
            physics.velScale = 2.0f;
        }

        private static void setCharacterVisual(VisualDesc visual, Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap, Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos) {
            visual.packFlags |= VisualDesc.PackFlag.SCALE | VisualDesc.PackFlag.GLOBALMOD;
            visual.scale = new Vector3(0.9107999f, 0.9107999f, 0.98999995f);
            visual.globalAppearanceModifiers = new PartGroupDataDesc {
                packFlags = PartGroupDataDesc.PackFlag.KEY | PartGroupDataDesc.PackFlag.APPHASH,
                key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
                appearanceInfos = appearanceInfos,
            };
        }

        private static void setCharacterQualities(CBaseQualities qualities, SpeciesType species, SexType sex, DataId physObjDid) {
            qualities.packFlags |= CBaseQualities.PackFlag.INT_HASH_TABLE | CBaseQualities.PackFlag.BOOL_HASH_TABLE | CBaseQualities.PackFlag.FLOAT_HASH_TABLE | CBaseQualities.PackFlag.TIMESTAMP_HASH_TABLE | CBaseQualities.PackFlag.DATA_ID_HASH_TABLE | CBaseQualities.PackFlag.LONG_INT_HASH_TABLE;
            qualities.ints = new Dictionary<IntStat, int> {
                { IntStat.CONTAINERMAXCAPACITY, 78 },
                { IntStat.SPECIES, (int)species  },
                { IntStat.SEX, (int)sex },
                { IntStat.CLASS, 2 },
                { IntStat.LEVEL, 7 },
                { IntStat.GROOVELEVEL, -1 },
                { IntStat.MONEY, 321 },
                { IntStat.HEALTH_CURRENTLEVEL, 100 },
                { IntStat.VIGOR_CURRENTLEVEL, 100 },
                { IntStat.HEALTH_CACHEDMAX, 100 },
                { IntStat.VIGOR_CACHEDMAX, 100 },
            };
            qualities.longs = new Dictionary<LongIntStat, long> {
                { LongIntStat.TOTALXP, 902 },
                { LongIntStat.AVAILABLEXP, 722 },
                { LongIntStat.TOTALCRAFTXP, 80 },
                { LongIntStat.AVAILABLECRAFTXP, 40 },
            };
            qualities.bools = new Dictionary<BoolStat, bool> {
                { BoolStat.PLAYER_ISONMOUNT, false }
            };
            qualities.floats = new Dictionary<FloatStat, float> {
                { FloatStat.HEALTH_REGENRATE, 1.0f },
                { FloatStat.VIGOR_REGENRATE, 1.0f },
            };
            qualities.dids = new Dictionary<DataIdStat, DataId> {
                { DataIdStat.PHYSOBJ, physObjDid }
            };
        }

        private static void createStartingInventory(WorldObjectManager objectManager, ContentManager contentManager, InventoryManager inventoryManager, WorldObject character, CharGenMatrix charGenMatrix, SpeciesType species, Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos) {
            RList<StartInvData> startInvItems = charGenMatrix.startingInventoryTable[(uint)species].to<ARHash<IPackage>>()[0].to<RList<IPackage>>()[0].to<StartInvData>();
            foreach (StartInvData startInvItem in startInvItems) {
                WorldObject item = objectManager.create();
                ObjectGen.applyWeenie(item, contentManager, startInvItem.entityDid);

                DataId weenieStateDid = new DataId(0x71000000 + item.qualities.weenieDesc.entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
                WState clothingWeenieState = contentManager.getWeenieState(weenieStateDid);
                Clothing clothing = clothingWeenieState.package as Clothing;
                if (clothing != null) {
                    DataId appearanceDid = clothing.wornAppearanceDidHash[(uint)(character.qualities.ints[IntStat.SPECIES] | character.qualities.ints[IntStat.SEX])];
                    Dictionary<DataId, Dictionary<AppearanceKey, float>> itemAppearanceInfos = new Dictionary<DataId, Dictionary<AppearanceKey, float>>();
                    if (appearanceInfos.TryGetValue(appearanceDid, out Dictionary<AppearanceKey, float> appearances)) {
                        itemAppearanceInfos[appearanceDid] = appearances;
                    }
                    item.visual.globalAppearanceModifiers = new PartGroupDataDesc {
                        packFlags = PartGroupDataDesc.PackFlag.KEY | PartGroupDataDesc.PackFlag.APPHASH,
                        key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
                        appearanceInfos = itemAppearanceInfos,
                    };
                }

                item.qualities.weenieDesc.containerId = character.id;
                item.qualities.weenieDesc.packFlags |= WeenieDesc.PackFlag.CONTAINER_ID;

                inventoryManager.giveItem(character, item);

                if (startInvItem.equipped) {
                    InvLoc equipLoc = (InvLoc)item.qualities.ints[IntStat.PREFERREDINVENTORYLOCATION];
                    inventoryManager.setItemEquipped(character, item, equipLoc);
                }
            }
        }
    }
}
