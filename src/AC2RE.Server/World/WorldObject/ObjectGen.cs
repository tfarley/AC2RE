using AC2RE.Definitions;
using System;

namespace AC2RE.Server {

    internal static class ObjectGen {

        public static void applyWeenie(WorldObject worldObject, ContentManager contentManager, DataId entityDid) {
            EntityDef entityDef = contentManager.getEntityDef(entityDid);
            if (entityDef.type != EntityType.WEENIE) {
                throw new ArgumentException(entityDef.type.ToString());
            }

            DataId qualitiesDid = new(0x81000000 + entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
            worldObject.qualities = contentManager.getQualities(qualitiesDid);
            worldObject.qualities.packFlags |= CBaseQualities.PackFlag.WEENIE_DESC;

            WeenieDesc weenie = worldObject.qualities.weenieDesc;
            weenie.packageType = entityDef.packageType;
            if (weenie.packageType != PackageType.UNDEF) {
                weenie.packFlags |= WeenieDesc.PackFlag.MY_PACKAGE_ID;
            }
            weenie.entityDid = entityDid;
            weenie.packFlags |= WeenieDesc.PackFlag.ENTITY_DID;

            if (worldObject.qualities.dids.TryGetValue(DataIdStat.PHYSOBJ, out DataId physObjDid)) {
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
