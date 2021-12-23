using AC2RE.Definitions;
using System;
using System.Collections.Generic;

namespace AC2RE.Server;

internal class LandblockManager {

    private readonly World world;

    private readonly Landblock?[,] landblocks = new Landblock[256, 256];
    private readonly List<Landblock> activeLandblocks = new();

    public LandblockManager(World world) {
        this.world = world;
    }

    public Landblock get(LandblockId id) {
        return get(id.x, id.y);
    }

    public Landblock get(byte x, byte y) {
        Landblock? landblock = landblocks[x, y];
        if (landblock == null) {
            landblock = new(new(x, y), world.serverTime.time);
            activeLandblocks.Add(landblock);
            landblocks[x, y] = landblock;

            foreach (WorldObject worldObject in world.objectManager.loadWithLandblockId(landblock.id)) {
                worldObject.enterWorld();
            }
        }
        return landblock;
    }

    public void enterWorld(WorldObject worldObject) {
        if (worldObject.parentId == InstanceId.NULL && get(worldObject.landblockId).objectIds.Add(worldObject.id)) {
            syncObjectVisibility(worldObject);
        }
    }

    public void leaveWorld(WorldObject worldObject) {
        if (get(worldObject.landblockId).objectIds.Remove(worldObject.id)) {
            syncObjectVisibility(worldObject);
        }
    }

    public void update() {
        List<WorldObject> movedCharacters = new();

        List<Landblock> landblocksToUpdate = new(activeLandblocks);
        foreach (Landblock landblock in landblocksToUpdate) {
            List<InstanceId> removedObjectIds = new();

            foreach (InstanceId objectId in landblock.objectIds) {
                if (world.objectManager.tryGet(objectId, out WorldObject? worldObject)) {
                    // TODO: Invoke worldObject.update(serverTime.time) or maybe pass in deltatime

                    if (worldObject.inWorld && worldObject.containerId == InstanceId.NULL && !worldObject.deleted) {
                        if (worldObject.landblockId != landblock.id) {
                            removedObjectIds.Add(objectId);
                            get(worldObject.landblockId).objectIds.Add(worldObject.id);

                            if (worldObject.packageType == PackageType.PlayerAvatar) {
                                movedCharacters.Add(worldObject);
                            }

                            syncObjectVisibility(worldObject);
                        } else if (worldObject.packageType == PackageType.PlayerAvatar) {
                            landblock.refreshTimeout(world.serverTime.time);
                        }
                    } else {
                        removedObjectIds.Add(objectId);
                    }
                } else {
                    removedObjectIds.Add(objectId);
                }
            }

            landblock.objectIds.ExceptWith(removedObjectIds);
        }

        foreach (WorldObject movedCharacter in movedCharacters) {
            if (world.playerManager.tryGet(movedCharacter.id, out Player? player)) {
                syncPlayerVisibility(player);
            }
        }
    }

    public void syncObjectVisibility(WorldObject worldObject) {
        HashSet<Player> landblockVisiblePlayers = new();

        if (worldObject.inWorld) {
            // Character itself and contained items are always in scope
            // TODO: Try to avoid loop over players by having reference from character object to owning player
            foreach (Player player in world.playerManager.players) {
                if (player.characterId != InstanceId.NULL) {
                    if (worldObject.id == player.characterId || worldObject.containerId == player.characterId) {
                        landblockVisiblePlayers.Add(player);
                        break;
                    }
                }
            }

            if (worldObject.containerId == InstanceId.NULL || worldObject.parentId != InstanceId.NULL) {
                LandblockId landblockId;
                if (worldObject.parentId != InstanceId.NULL && world.objectManager.tryGet(worldObject.parentId, out WorldObject? parentObject)) {
                    landblockId = parentObject.landblockId;
                } else {
                    landblockId = worldObject.landblockId;
                }
                byte landblockX = landblockId.x;
                byte landblockY = landblockId.y;
                for (byte x = (byte)Math.Max(landblockX - 1, 0); x <= Math.Min(landblockX + 1, 255); x++) {
                    for (byte y = (byte)Math.Max(landblockY - 1, 0); y <= Math.Min(landblockY + 1, 255); y++) {
                        Landblock landblock = get(new(x, y));
                        // TODO: This is inefficient - have a better way to get players by landblock without iterating all objects
                        foreach (InstanceId otherObjectId in landblock.objectIds) {
                            if (world.objectManager.tryGet(otherObjectId, out WorldObject? otherObject) && otherObject.packageType == PackageType.PlayerAvatar) {
                                if (world.playerManager.tryGet(otherObject.id, out Player? player)) {
                                    landblockVisiblePlayers.Add(player);
                                }
                            }
                        }
                    }
                }
            }
        }

        // TODO: Consider caching this on the world object itself so that it doesn't have to loop through all players for each sync
        HashSet<Player> currentVisiblePlayers = new();
        foreach (Player player in world.playerManager.players) {
            if (player.visibleObjectIds.Contains(worldObject.id)) {
                currentVisiblePlayers.Add(player);
            }
        }

        foreach (Player landblockVisiblePlayer in landblockVisiblePlayers) {
            if (landblockVisiblePlayer.visibleObjectIds.Add(worldObject.id)) {
                world.playerManager.send(landblockVisiblePlayer, new CreateObjectMsg {
                    id = worldObject.id,
                    visualDesc = worldObject.visual,
                    physicsDesc = worldObject.physics,
                    weenieDesc = worldObject.qualities.weenieDesc,
                });
            }
            foreach (InstanceId childId in worldObject.childIdsEnumerable) {
                if (landblockVisiblePlayer.visibleObjectIds.Add(childId) && world.objectManager.tryGet(childId, out WorldObject? childObject)) {
                    world.playerManager.send(landblockVisiblePlayer, new CreateObjectMsg {
                        id = childObject.id,
                        visualDesc = childObject.visual,
                        physicsDesc = childObject.physics,
                        weenieDesc = childObject.qualities.weenieDesc,
                    });
                }
            }
        }

        HashSet<Player> removedPlayers = new(currentVisiblePlayers);
        removedPlayers.ExceptWith(landblockVisiblePlayers);

        foreach (Player removedPlayer in removedPlayers) {
            if (removedPlayer.visibleObjectIds.Remove(worldObject.id)) {
                world.playerManager.send(removedPlayer, new DestroyObjectMsg {
                    idWithStamp = worldObject.getInstanceIdWithStamp(),
                });
            }
            foreach (InstanceId childId in worldObject.childIdsEnumerable) {
                if (removedPlayer.visibleObjectIds.Remove(childId) && world.objectManager.tryGet(childId, out WorldObject? childObject)) {
                    world.playerManager.send(removedPlayer, new DestroyObjectMsg {
                        idWithStamp = childObject.getInstanceIdWithStamp(),
                    });
                }
            }
        }
    }

    public void syncPlayerVisibility(Player player) {
        if (world.objectManager.tryGet(player.characterId, out WorldObject? character)) {
            // Character itself and contained items are always in scope
            HashSet<InstanceId> landblockVisibleObjectIds = new(character.containedItemIdsEnumerable);
            landblockVisibleObjectIds.Add(character.id);

            LandblockId landblockId = character.landblockId;
            byte landblockX = landblockId.x;
            byte landblockY = landblockId.y;
            for (byte x = (byte)Math.Max(landblockX - 1, 0); x <= Math.Min(landblockX + 1, 255); x++) {
                for (byte y = (byte)Math.Max(landblockY - 1, 0); y <= Math.Min(landblockY + 1, 255); y++) {
                    landblockVisibleObjectIds.UnionWith(get(new(x, y)).objectIds);
                }
            }

            foreach (InstanceId landblockVisibleObjectId in new HashSet<InstanceId>(landblockVisibleObjectIds)) {
                if (world.objectManager.tryGet(landblockVisibleObjectId, out WorldObject? landblockObject)) {
                    if (player.visibleObjectIds.Add(landblockObject.id)) {
                        world.playerManager.send(player, new CreateObjectMsg {
                            id = landblockObject.id,
                            visualDesc = landblockObject.visual,
                            physicsDesc = landblockObject.physics,
                            weenieDesc = landblockObject.qualities.weenieDesc,
                        });
                    }
                    foreach (InstanceId childId in landblockObject.childIdsEnumerable) {
                        if (landblockVisibleObjectIds.Add(childId) && player.visibleObjectIds.Add(childId) && world.objectManager.tryGet(childId, out WorldObject? childObject)) {
                            world.playerManager.send(player, new CreateObjectMsg {
                                id = childObject.id,
                                visualDesc = childObject.visual,
                                physicsDesc = childObject.physics,
                                weenieDesc = childObject.qualities.weenieDesc,
                            });
                        }
                    }
                }
            }

            HashSet<InstanceId> removedObjectIds = new(player.visibleObjectIds);
            removedObjectIds.ExceptWith(landblockVisibleObjectIds);

            foreach (InstanceId removedObjectId in removedObjectIds) {
                if (player.visibleObjectIds.Remove(removedObjectId)) {
                    world.playerManager.send(player, new DestroyObjectMsg {
                        idWithStamp = world.objectManager.getStamp(removedObjectId),
                    });
                }
            }
        }
    }
}
