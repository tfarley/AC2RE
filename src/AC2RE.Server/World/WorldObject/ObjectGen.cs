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

            WeenieDesc weenie = worldObject.qualities.weenieDesc;
            weenie.packageType = entityDef.packageType;
            weenie.entityDid = entityDid;
            weenie.name = worldObject.name;
            weenie.pluralName = worldObject.pluralName;
            weenie.iconDid = worldObject.iconDid;
            weenie.containerId = worldObject.containerId;
            weenie.wielderId = worldObject.wielderId;
            weenie.monarchId = worldObject.monarchId;
            weenie.originatorId = worldObject.originatorId;
            weenie.claimantId = worldObject.claimantId;
            weenie.killerId = worldObject.killerId;
            weenie.petSummonerId = worldObject.summonerId;
            weenie.quantity = worldObject.quantity;
            weenie.value = worldObject.value;
            weenie.factionType = worldObject.faction;
            weenie.pkAlwaysTruePermissions = worldObject.pkAlwaysTruePermissions;
            weenie.pkAlwaysFalsePermissions = worldObject.pkAlwaysFalsePermissions;
            weenie.physicsTypeLow = worldObject.physicsTypeLow;
            weenie.physicsTypeHigh = worldObject.physicsTypeHigh;
            weenie.movementEtherealLow = worldObject.movementEtherealLow;
            weenie.movementEtherealHigh = worldObject.movementEtherealHigh;
            weenie.placementEtherealLow = worldObject.placementEtherealLow;
            weenie.placementEtherealHigh = worldObject.placementEtherealHigh;
            weenie.durabilityCurrentLevel = worldObject.durability;
            weenie.durabilityMaxLevel = worldObject.durabilityMax;
            weenie.scale = worldObject.scale;

            DataId? physObjDid = worldObject.physObjDid;
            if (physObjDid != null) {
                applyPhysics(worldObject, contentManager, physObjDid.Value);
            }
        }

        public static void applyPhysics(WorldObject worldObject, ContentManager contentManager, DataId entityDid) {
            EntityDef entityDef = contentManager.getEntityDef(entityDid);
            if (entityDef.type != EntityType.PHYSICS) {
                throw new ArgumentException(entityDef.type.ToString());
            }

            worldObject.visual.parentDid = entityDef.dataId;
        }
    }
}
