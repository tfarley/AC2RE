using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Def.Structs {

    public class Frame {

        public Vector pos;
        public Quaternion rot;

        public Frame(BinaryReader data) {
            pos = data.ReadVector();
            rot = data.ReadQuaternion();
        }
    }
}
