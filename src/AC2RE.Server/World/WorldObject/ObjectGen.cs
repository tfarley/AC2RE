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
            CBaseQualities baseQualities = contentManager.getQualities(qualitiesDid);
            WeenieDesc baseWeenie = baseQualities.weenieDesc;
            worldObject.qualities = new CBaseQualities {
                packFlags = baseQualities.packFlags,
                did = baseQualities.did,
                weenieDesc = new WeenieDesc {
                    bitfield = baseWeenie.bitfield,
                    packageType = baseWeenie.packageType,
                    entityDid = baseWeenie.entityDid,
                    packFlags = baseWeenie.packFlags,
                    name = baseWeenie.name,
                    pluralName = baseWeenie.pluralName,
                    iconDid = baseWeenie.iconDid,
                    containerId = baseWeenie.containerId,
                    wielderId = baseWeenie.wielderId,
                    monarchId = baseWeenie.monarchId,
                    originatorId = baseWeenie.originatorId,
                    claimantId = baseWeenie.claimantId,
                    killerId = baseWeenie.killerId,
                    petSummonerId = baseWeenie.petSummonerId,
                    quantity = baseWeenie.quantity,
                    value = baseWeenie.value,
                    factionType = baseWeenie.factionType,
                    pkAlwaysTruePermissions = baseWeenie.pkAlwaysTruePermissions,
                    pkAlwaysFalsePermissions = baseWeenie.pkAlwaysFalsePermissions,
                    physicsTypeLow = baseWeenie.physicsTypeLow,
                    physicsTypeHigh = baseWeenie.physicsTypeHigh,
                    movementEtherealLow = baseWeenie.movementEtherealLow,
                    movementEtherealHigh = baseWeenie.movementEtherealHigh,
                    placementEtherealLow = baseWeenie.placementEtherealLow,
                    placementEtherealHigh = baseWeenie.placementEtherealHigh,
                    durabilityCurrentLevel = baseWeenie.durabilityCurrentLevel,
                    durabilityMaxLevel = baseWeenie.durabilityMaxLevel,
                    scale = baseWeenie.scale,
                },
                ints = baseQualities.ints != null ? new(baseQualities.ints) : null,
                longs = baseQualities.longs != null ? new(baseQualities.longs) : null,
                bools = baseQualities.bools != null ? new(baseQualities.bools) : null,
                floats = baseQualities.floats != null ? new(baseQualities.floats) : null,
                doubles = baseQualities.doubles != null ? new(baseQualities.doubles) : null,
                strings = baseQualities.strings != null ? new(baseQualities.strings) : null,
                dids = baseQualities.dids != null ? new(baseQualities.dids) : null,
                ids = baseQualities.ids != null ? new(baseQualities.ids) : null,
                poss = baseQualities.poss != null ? new(baseQualities.poss) : null,
                stringInfos = baseQualities.stringInfos != null ? new(baseQualities.stringInfos) : null, // TODO: Need to do a deep copy of the StringInfos?
                packageIds = baseQualities.packageIds != null ? new(baseQualities.packageIds) : null,
            };

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
