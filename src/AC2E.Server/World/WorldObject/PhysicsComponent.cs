using AC2E.Def;
using System.Numerics;

namespace AC2E.Server {

    internal class PhysicsComponent {

        public Position pos;
        public Vector3 vel;
        public Vector3 accel;
        public ushort teleportStamp;
        public ushort forcePosStamp;
        public ushort movetoStamp;
    }
}
