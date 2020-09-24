using AC2E.Def;
using System;
using System.Collections.Generic;

namespace AC2E.Server {

    internal static class ObjectGen {

        public static void applyWeenie(WorldObject worldObject, ContentManager contentManager, DataId entityDid) {
            EntityDef entityDef = contentManager.getEntityDef(entityDid);
            if (entityDef.type != EntityType.WEENIE) {
                throw new ArgumentException(entityDef.type.ToString());
            }

            WeenieDesc weenie = worldObject.weenie;
            weenie.packFlags = WeenieDesc.PackFlag.PHYSICS_TYPE_LOW_DWORD | WeenieDesc.PackFlag.PHYSICS_TYPE_HIGH_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.ENTITY_DID;
            weenie.packageType = entityDef.packageType;
            weenie.entityDid = entityDid;
            weenie.name = entityDef.strings.GetValueOrDefault(PropertyName.NAME);
            weenie.physicsTypeLow = 75497504;
            weenie.movementEtherealLow = 1042284560;
            weenie.placementEtherealLow = 65011856;
            if (weenie.packageType != PackageType.UNDEF) {
                weenie.packFlags |= WeenieDesc.PackFlag.MY_PACKAGE_ID;
            }
            if (weenie.name != null) {
                weenie.packFlags |= WeenieDesc.PackFlag.NAME;
            }
            if (entityDef.dids.TryGetValue(PropertyName.PHYSOBJ, out DataId physObjDid)) {
                applyPhysics(worldObject, contentManager, physObjDid);
            }
        }

        public static void applyPhysics(WorldObject worldObject, ContentManager contentManager, DataId entityDid) {
            EntityDef entityDef = contentManager.getEntityDef(entityDid);
            if (entityDef.type != EntityType.PHYSICS) {
                throw new ArgumentException(entityDef.type.ToString());
            }

            worldObject.visual.packFlags |= VisualDesc.PackFlag.PARENT;
            worldObject.visual.parentDid = entityDef.dataId;
        }
    }
}
