using System.Collections.Generic;

namespace AC2RE.Server.Database {

    internal class WorldSave {

        public List<Character> characters = new();
        public InstanceIdGenerator? idGenerator;
        public List<WorldObject> worldObjects = new();
    }
}
