using System.Collections.Generic;

namespace AC2RE.Server.Database {

    internal class WorldSave {

        public readonly List<Character> characters = new();
        public InstanceIdGenerator? idGenerator;
        public readonly List<WorldObject> worldObjects = new();
    }
}
