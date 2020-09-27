using AC2E.Def;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Server {

    internal static class CharacterGen {

        public static WorldObject createCharacterObject(WorldObjectManager objectManager, ContentManager contentManager, Position startPos, string name, SpeciesType species, SexType sex, Dictionary<PhysiqueType, float> physiqueTypeValues) {
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

            WorldObject character = objectManager.create();
            ObjectGen.applyWeenie(character, contentManager, characterGenSystem.playerEntityDid);
            ObjectGen.applyPhysics(character, contentManager, raceSexInfo.physObjDid);
            setCharacterPhysics(character.physics, startPos);
            setCharacterVisual(character.visual, appProfileMap, physiqueTypeValues);
            setCharacterQualities(character.qualities);

            character.weenie.packFlags |= WeenieDesc.PackFlag.NAME;
            character.weenie.name = new StringInfo(name);

            createStartingInventory(objectManager, contentManager, character, charGenMatrix, species);

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

        private static void setCharacterVisual(VisualDesc visual, Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap, Dictionary<PhysiqueType, float> physiqueTypeValues) {
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

            visual.packFlags |= VisualDesc.PackFlag.SCALE | VisualDesc.PackFlag.GLOBALMOD;
            visual.scale = new Vector3(0.9107999f, 0.9107999f, 0.98999995f);
            visual.globalAppearanceModifiers = new PartGroupDataDesc {
                packFlags = PartGroupDataDesc.PackFlag.KEY | PartGroupDataDesc.PackFlag.APPHASH,
                key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
                appearanceInfos = appearanceInfos,
                /*appearanceInfos = new Dictionary<DataId, Dictionary<AppearanceKey, float>> {
                    { new DataId(0x2000004E), new Dictionary<AppearanceKey, float> {
                        { AppearanceKey.CLOTHINGCOLOR, 0.14f },
                        { AppearanceKey.WORN, 1.0f },
                    } },
                    { new DataId(0x20000050), new Dictionary<AppearanceKey, float> {
                        { AppearanceKey.CLOTHINGCOLOR, 0.24f },
                        { AppearanceKey.WORN, 1.0f },
                    } },
                    { new DataId(0x2000000C), new Dictionary<AppearanceKey, float> {
                        { AppearanceKey.SKINCOLOR, physiqueTypeValues[PhysiqueType.SKIN_TONE] },
                    } },
                    { new DataId(0x2000000D), new Dictionary<AppearanceKey, float> {
                        { AppearanceKey.HEADMESH, physiqueTypeValues[PhysiqueType.HEAD_DETAIL] },
                        { AppearanceKey.HEADCOLOR, physiqueTypeValues[PhysiqueType.FRILL_COLOR] },
                        { AppearanceKey.BEARDMESH, physiqueTypeValues[PhysiqueType.HEAD_FRILL] },
                    } },
                    { new DataId(0x2000000E), new Dictionary<AppearanceKey, float> {
                        { AppearanceKey.FACETEXTURE, physiqueTypeValues[PhysiqueType.FACE_DETAIL] },
                    } },
                    { new DataId(0x200000F9), new Dictionary<AppearanceKey, float> {
                        { AppearanceKey.CLOTHINGCOLOR, 0.04f },
                        { AppearanceKey.WORN, 1.0f },
                    } },
                    { new DataId(0x20000016), new Dictionary<AppearanceKey, float> {
                        { AppearanceKey.CLOTHINGCOLOR, 0.2f },
                        { AppearanceKey.WORN, 1.0f },
                    } },
                }*/
            };
        }

        private static void setCharacterQualities(CBaseQualities qualities) {
            qualities.packFlags = CBaseQualities.PackFlag.INT_HASH_TABLE | CBaseQualities.PackFlag.BOOL_HASH_TABLE | CBaseQualities.PackFlag.FLOAT_HASH_TABLE | CBaseQualities.PackFlag.TIMESTAMP_HASH_TABLE | CBaseQualities.PackFlag.DATA_ID_HASH_TABLE | CBaseQualities.PackFlag.LONG_INT_HASH_TABLE;
            //qualities.weenieDesc = character.weenie; // TODO: Do this with CBaseQualities.PackFlag.WEENIE_DESC? Need to assign it every time after un-persisted from database though...
            qualities.ints = new Dictionary<IntStat, int> {
                { IntStat.CONTAINERMAXCAPACITY, 78 },
                { IntStat.SPECIES, 1 },
                { IntStat.SEX, 4096 },
                { IntStat.CLASS, 2 },
                { IntStat.LEVEL, 7 },
                { IntStat.GROOVELEVEL, -1 },
                { IntStat.MONEY, 391 },
                { IntStat.HEALTH_CURRENTLEVEL, 310 },
                { IntStat.VIGOR_CURRENTLEVEL, 280 },
                { IntStat.HEALTH_CACHEDMAX, 280 },
                { IntStat.VIGOR_CACHEDMAX, 280 },
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
                { FloatStat.CURRENTVITAE, 100.0f },
                { FloatStat.HEALTH_REGENRATE, 1.0f },
                { FloatStat.VIGOR_REGENRATE, 1.0f },
                { FloatStat.SKILL_RESETTIMEDURATION, 30.0f },
            };
            qualities.doubles = new Dictionary<TimestampStat, double> {
                { TimestampStat.SKILL_TIMELASTRESET, 121629267.45585053 }
            };
            qualities.dids = new Dictionary<DataIdStat, DataId> {
                { DataIdStat.PHYSOBJ, new DataId(0x470000CD) }
            };
        }

        private static void createStartingInventory(WorldObjectManager objectManager, ContentManager contentManager, WorldObject character, CharGenMatrix charGenMatrix, SpeciesType species) {
            RList<StartInvData> startInvItems = charGenMatrix.startingInventoryTable[(uint)species].to<ARHash<IPackage>>()[0].to<RList<IPackage>>()[0].to<StartInvData>();
            foreach (StartInvData startInvItem in startInvItems) {
                WorldObject item = objectManager.create();
                ObjectGen.applyWeenie(item, contentManager, startInvItem.entityDid);
                item.weenie.containerId = character.id;
            }
        }
    }
}
