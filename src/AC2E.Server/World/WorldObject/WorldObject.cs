using AC2E.Def;

namespace AC2E.Server {

    internal class WorldObject {

        public readonly InstanceId id;
        public readonly ushort instanceStamp;

        public StringInfo name;
        public PhysicsComponent physics;
        public VisualComponent visual;

        public WorldObject(InstanceId id, ushort instanceStamp) {
            this.id = id;
            this.instanceStamp = instanceStamp;
        }
    }
}
