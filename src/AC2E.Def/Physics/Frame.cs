using System.IO;

namespace AC2E.Def {

    public class Frame {

        public Vector pos;
        public Quaternion rot;

        public Frame(Vector pos, Quaternion rot) {
            this.pos = pos;
            this.rot = rot;
        }

        public Frame(BinaryReader data) {
            pos = data.ReadVector();
            rot = data.ReadQuaternion();
        }

        public void write(BinaryWriter data) {
            data.Write(pos);
            data.Write(rot);
        }
    }
}
