namespace AC2E.Def {

    public class TransformBase {

        public Vector offset; // offset
        public Vector scale; // scale
        public Quaternion rot; // qt

        public TransformBase(AC2Reader data) {
            offset = data.ReadVector();
            scale = data.ReadVector();
            rot = data.ReadQuaternion();
        }
    }
}
