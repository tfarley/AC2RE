using AC2E.Def;

namespace AC2E.Server {

    internal class WorldObject {

        public readonly InstanceId id;
        public bool deleted;
        public readonly ushort instanceStamp;

        public PhysicsDesc physics;
        public VisualDesc visual;
        public WeenieDesc weenie;

        public WorldObject(InstanceId id, ushort instanceStamp) {
            this.id = id;
            this.instanceStamp = instanceStamp;
        }
    }
}
