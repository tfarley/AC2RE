using System.IO;

namespace AC2E.Def {

    public class Frame {

        public Vector pos;
        public Quaternion rot;

        public Frame(BinaryReader data) {
            pos = data.ReadVector();
            rot = data.ReadQuaternion();
        }
    }
}
