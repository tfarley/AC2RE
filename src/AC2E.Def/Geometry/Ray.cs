namespace AC2E.Def {

    public class Ray : IPackage {

        public NativeType nativeType => NativeType.RAY;

        public Vector pt; // pt
        public Vector dir; // dir
        public float length; // length

        public Ray(AC2Reader data) {
            pt = data.ReadVector();
            dir = data.ReadVector();
            length = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(pt);
            data.Write(dir);
            data.Write(length);
        }
    }
}
