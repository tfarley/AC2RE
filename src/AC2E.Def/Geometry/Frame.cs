namespace AC2E.Def {

    public class Frame : IPackage {

        public NativeType nativeType => NativeType.FRAME;

        public Vector pos;
        public Quaternion rot;

        public Frame(Vector pos, Quaternion rot) {
            this.pos = pos;
            this.rot = rot;
        }

        public Frame(AC2Reader data) {
            pos = data.ReadVector();
            rot = data.ReadQuaternion();
        }

        public void write(AC2Writer data) {
            data.Write(pos);
            data.Write(rot);
        }
    }
}
