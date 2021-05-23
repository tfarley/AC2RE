using AC2RE.Definitions;
using System;
using System.Collections.Generic;

namespace AC2RE.Server.Database {

    internal interface IWorldDatabase : IDisposable {

        public Character? getCharacterWithId(CharacterId id);
        public List<Character> getCharactersWithAccount(AccountId accountId);

        public InstanceIdGenerator? getInstanceIdGenerator(string type);

        public List<WorldObject> getAllWorldObjects(World world);
        public WorldObject? getWorldObjectWithId(World world, InstanceId id);
        public List<WorldObject> getWorldObjectsWithLandblockId(World world, LandblockId landblockId);
        public List<WorldObject> getWorldObjectsWithContainerId(World world, InstanceId containerId);
        public List<WorldObject> getWorldObjectsWithEquipperId(World world, InstanceId equipperId);
        public List<WorldObject> getWorldObjectsWithParentId(World world, InstanceId parentId);
        public void initWorldObject(World world, WorldObject worldObject);

        public bool save(WorldSave worldSave);
    }
}
