using AC2E.Def;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Server {

    internal static class CharacterGeneration {

        public static WorldObject createCharacterObject(WorldObjectManager objectManager, DatReader portalDatReader, Position startPos, string name, SpeciesType species, SexType sex, Dictionary<PhysiqueType, float> physiqueTypeValues) {
            Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap = new Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>>();
            GMRaceSexInfo raceSexInfo;

            using (AC2Reader data = portalDatReader.getFileReader(new DataId(0x70000390))) {
                WState wState = new WState(data);

                CharGenMatrix charGenMatrix = (CharGenMatrix)wState.package;

                foreach (KeyValuePair<uint, RList<IPackage>> physiqueAndAppProfiles in ((ARHash<IPackage>)charGenMatrix.physiqueTypeModifierTable.contents[(uint)species].contents[(uint)sex]).to<RList<IPackage>>().contents) {
                    PhysiqueType physiqueType = (PhysiqueType)physiqueAndAppProfiles.Key;
                    RList<AppearanceProfile> appProfiles = physiqueAndAppProfiles.Value.to<AppearanceProfile>();

                    if (!appProfileMap.TryGetValue(physiqueType, out Dictionary<float, Tuple<AppearanceKey, DataId>> modifierToApp)) {
                        modifierToApp = new Dictionary<float, Tuple<AppearanceKey, DataId>>();
                        appProfileMap[physiqueType] = modifierToApp;
                    }

                    foreach (AppearanceProfile appProfile in appProfiles.contents) {
                        modifierToApp[appProfile.modifier] = new Tuple<AppearanceKey, DataId>(appProfile.appKey, appProfile.aprDid);
                    }
                }

                raceSexInfo = charGenMatrix.raceSexInfoTable.contents[(uint)species | (uint)sex];
            }

            DataId visualDescDid;

            using (AC2Reader data = portalDatReader.getFileReader(raceSexInfo.physObjDid)) {
                EntityDesc entityDesc = new EntityDesc(data);

                visualDescDid = entityDesc.dataId;
            }

            WorldObject characterObject = objectManager.create();
            characterObject.physics = createCharacterPhysics(startPos);
            characterObject.visual = createCharacterVisual(visualDescDid, appProfileMap, physiqueTypeValues);
            characterObject.weenie = createCharacterWeenie(new StringInfo(name));

            return characterObject;
        }

        private static PhysicsDesc createCharacterPhysics(Position startPos) {
            return new PhysicsDesc {
                packFlags = PhysicsDesc.PackFlag.SLIDERS | PhysicsDesc.PackFlag.MODE | PhysicsDesc.PackFlag.POSITION | PhysicsDesc.PackFlag.VELOCITY_SCALE,
                sliders = new Dictionary<uint, PhysicsDesc.SliderData> {
                    { 1073741834, new PhysicsDesc.SliderData {
                        value = 1.0f,
                        velocity = 0.0f,
                    } }
                },
                modeId = 1073741825,
                pos = startPos,
                velScale = 20.0f,
                timestamps = new ushort[] { 1, 0, 0, 0 },
                instanceStamp = 5,
                visualOrderStamp = 8,
            };
        }

        private static VisualDesc createCharacterVisual(DataId visualDescDid, Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap, Dictionary<PhysiqueType, float> physiqueTypeValues) {
            Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos = new Dictionary<DataId, Dictionary<AppearanceKey, float>>();
            foreach (KeyValuePair<PhysiqueType, float> physiqueTypeValue in physiqueTypeValues) {
                if (appProfileMap.TryGetValue(physiqueTypeValue.Key, out Dictionary<float, Tuple<AppearanceKey, DataId>> modifierToAppProfiles)) {
                    Tuple<AppearanceKey, DataId> app = modifierToAppProfiles[physiqueTypeValue.Value];
                    if (app.Item2.id != 0) {
                        if (!appearanceInfos.TryGetValue(app.Item2, out Dictionary<AppearanceKey, float> appToModfier)) {
                            appToModfier = new Dictionary<AppearanceKey, float>();
                            appearanceInfos[app.Item2] = appToModfier;
                        }
                        appToModfier[app.Item1] = physiqueTypeValue.Value;
                    }
                }
            }

            // TODO: Temporary addition of clothing - this creates some QUITE cursed visuals when used on anything besides human male...
            appearanceInfos[new DataId(0x2000004E)] = new Dictionary<AppearanceKey, float> {
                { AppearanceKey.CLOTHINGCOLOR, 0.14f },
                { AppearanceKey.WORN, 1.0f },
            };
            appearanceInfos[new DataId(0x20000050)] = new Dictionary<AppearanceKey, float> {
                { AppearanceKey.CLOTHINGCOLOR, 0.24f },
                { AppearanceKey.WORN, 1.0f },
            };
            appearanceInfos[new DataId(0x200000F9)] = new Dictionary<AppearanceKey, float> {
                { AppearanceKey.CLOTHINGCOLOR, 0.04f },
                { AppearanceKey.WORN, 1.0f },
            };
            appearanceInfos[new DataId(0x20000016)] = new Dictionary<AppearanceKey, float> {
                { AppearanceKey.CLOTHINGCOLOR, 0.2f },
                { AppearanceKey.WORN, 1.0f },
            };

            return new VisualDesc {
                packFlags = VisualDesc.PackFlag.PARENT | VisualDesc.PackFlag.SCALE | VisualDesc.PackFlag.GLOBALMOD,
                parentDid = visualDescDid,
                scale = new Vector3(0.9107999f, 0.9107999f, 0.98999995f),
                globalAppearanceModifiers = new PartGroupDataDesc {
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
                }
            };
        }

        private static WeenieDesc createCharacterWeenie(StringInfo name) {
            return new WeenieDesc {
                packFlags = WeenieDesc.PackFlag.MY_PACKAGE_ID | WeenieDesc.PackFlag.NAME | WeenieDesc.PackFlag.MONARCH_ID | WeenieDesc.PackFlag.PHYSICS_TYPE_LOW_DWORD | WeenieDesc.PackFlag.PHYSICS_TYPE_HIGH_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.ENTITY_DID,
                packageId = new PackageId(895),
                name = name,
                monarchId = new InstanceId(0x2130000000003B2D),
                physicsTypeLow = 75497504,
                physicsTypeHigh = 0,
                movementEtherealLow = 1042284560,
                movementEtherealHigh = 0,
                placementEtherealLow = 65011856,
                placementEtherealHigh = 0,
                entityDid = new DataId(0x47000530),
            };
        }
    }
}
