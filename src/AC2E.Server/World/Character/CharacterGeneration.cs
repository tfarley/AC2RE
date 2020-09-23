using AC2E.Def;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Server {

    internal static class CharacterGeneration {

        public static WorldObject createCharacterObject(WorldObjectManager objectManager, DatReader portalDatReader, Position startPos, string name, SpeciesType species, SexType sex, Dictionary<PhysiqueType, float> physiqueTypeValues) {
            Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>> appProfileMap = new Dictionary<PhysiqueType, Dictionary<float, Tuple<AppearanceKey, DataId>>>();

            CharGenMatrix charGenMatrix;
            using (AC2Reader data = portalDatReader.getFileReader(new DataId(0x70000390))) {
                WState wState = new WState(data);
                charGenMatrix = (CharGenMatrix)wState.package;
            }

            foreach (KeyValuePair<uint, RList<IPackage>> physiqueAndAppProfiles in charGenMatrix.physiqueTypeModifierTable.contents[(uint)species].to<ARHash<IPackage>>().contents[(uint)sex].to<RList<IPackage>>().contents) {
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

            GMRaceSexInfo raceSexInfo = charGenMatrix.raceSexInfoTable.contents[(uint)species | (uint)sex];

            DataId visualDescDid;
            using (AC2Reader data = portalDatReader.getFileReader(raceSexInfo.physObjDid)) {
                EntityDesc entityDesc = new EntityDesc(data);
                visualDescDid = entityDesc.dataId;
            }

            WorldObject character = objectManager.create();
            character.physics = createCharacterPhysics(startPos);
            character.visual = createCharacterVisual(visualDescDid, appProfileMap, physiqueTypeValues);
            character.weenie = createCharacterWeenie(new StringInfo(name));

            createStartingInventory(objectManager, portalDatReader, character, charGenMatrix, species);

            return character;
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
                velScale = 2.0f,
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
                packFlags = WeenieDesc.PackFlag.MY_PACKAGE_ID | WeenieDesc.PackFlag.NAME | WeenieDesc.PackFlag.PHYSICS_TYPE_LOW_DWORD | WeenieDesc.PackFlag.PHYSICS_TYPE_HIGH_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.ENTITY_DID,
                packageType = PackageType.PlayerAvatar,
                entityDid = new DataId(0x47000530),
                name = name,
                physicsTypeLow = 75497504,
                physicsTypeHigh = 0,
                movementEtherealLow = 1042284560,
                movementEtherealHigh = 0,
                placementEtherealLow = 65011856,
                placementEtherealHigh = 0,
            };
        }

        private static void createStartingInventory(WorldObjectManager objectManager, DatReader portalDatReader, WorldObject character, CharGenMatrix charGenMatrix, SpeciesType species) {
            RList<StartInvData> startInvItems = charGenMatrix.startingInventoryTable.contents[(uint)species].to<ARHash<IPackage>>().contents[0].to<RList<IPackage>>().contents[0].to<StartInvData>();
            foreach (StartInvData startInvItem in startInvItems.contents) {
                WorldObject item = objectManager.create();
                item.physics = new PhysicsDesc();
                item.visual = new VisualDesc();
                using (AC2Reader data = portalDatReader.getFileReader(startInvItem.entityDid)) {
                    EntityDesc entityDesc = new EntityDesc(data);
                    item.weenie = new WeenieDesc {
                        packFlags = WeenieDesc.PackFlag.PHYSICS_TYPE_LOW_DWORD | WeenieDesc.PackFlag.PHYSICS_TYPE_HIGH_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.ENTITY_DID,
                        packageType = entityDesc.packageType,
                        entityDid = startInvItem.entityDid,
                        physicsTypeLow = 75497504,
                        physicsTypeHigh = 0,
                        movementEtherealLow = 1042284560,
                        movementEtherealHigh = 0,
                        placementEtherealLow = 65011856,
                        placementEtherealHigh = 0,
                        containerId = character.id,
                    };
                    if (entityDesc.packFlags.HasFlag(EntityDesc.PackFlag.DATAID)) {
                        item.weenie.packageType = entityDesc.packageType;
                        item.weenie.packFlags |= WeenieDesc.PackFlag.MY_PACKAGE_ID;
                    }
                    if (entityDesc.packFlags.HasFlag(EntityDesc.PackFlag.PROPERTIES)) {
                        foreach (PropertyGroup propertyGroup in entityDesc.properties.groups) {
                            foreach (BaseProperty property in propertyGroup.properties) {
                                switch (property.name) {
                                    case PropertyName.PHYSOBJ:
                                        using (AC2Reader physicsEntityData = portalDatReader.getFileReader((DataId)property.value)) {
                                            EntityDesc physicsEntityDesc = new EntityDesc(physicsEntityData);
                                            if (physicsEntityDesc.packFlags.HasFlag(EntityDesc.PackFlag.DATAID)) {
                                                using (AC2Reader visualDescData = portalDatReader.getFileReader(physicsEntityDesc.dataId)) {
                                                    item.visual = new VisualDesc(visualDescData);
                                                }
                                            }
                                        }
                                        break;
                                    case PropertyName.NAME:
                                        item.weenie.name = (StringInfo)property.value;
                                        item.weenie.packFlags |= WeenieDesc.PackFlag.NAME;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    // TODO: Temp hack, remove
                    if (item.visual == null || item.visual.globalAppearanceModifiers == null) {
                        item.visual.packFlags |= VisualDesc.PackFlag.GLOBALMOD;
                        item.visual.globalAppearanceModifiers = new PartGroupDataDesc {
                            packFlags = PartGroupDataDesc.PackFlag.KEY | PartGroupDataDesc.PackFlag.APPHASH,
                            key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
                            appearanceInfos = new Dictionary<DataId, Dictionary<AppearanceKey, float>> {
                                { new DataId(0x2000011B), new Dictionary<AppearanceKey, float> {
                                    { AppearanceKey.CLOTHINGCOLOR, 0.04f },
                                    { AppearanceKey.CLOTHINGCOLORSECONDARY, 0.23f },
                                    { AppearanceKey.WORN, 1.0f },
                                    { AppearanceKey.CLOTHINGCOLORTERTIARY, 0.13f },
                                } },
                            }
                        };
                    }
                }
            }
        }
    }
}
