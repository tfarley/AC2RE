using AC2E.Def;
using System.Numerics;

namespace AC2E.Server {

    internal class WorldObject {

        public readonly InstanceId id;
        public bool deleted;
        public ushort instanceStamp;

        public float heading;
        public Vector3 motion;
        public PhysicsDesc physics;
        public VisualDesc visual;
        public WeenieDesc weenie;

        public WorldObject(InstanceId id) {
            this.id = id;
        }
    }
}
