using System.Collections.Generic;

namespace AC2E.Server.Database {

    internal class WorldSave {

        public List<Character> characters = new List<Character>();
        public InstanceIdGenerator? idGenerator;
        public List<WorldObject> worldObjects = new List<WorldObject>();
    }
}
