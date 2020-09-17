using AC2E.Def;

namespace AC2E.Server {

    internal class WorldObject {

        public readonly InstanceId id;
        public readonly ushort instanceStamp;

        public PhysicsComponent physics;
        public VisualComponent visual;
    }
}
