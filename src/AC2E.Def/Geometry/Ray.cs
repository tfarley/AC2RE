namespace AC2E.Def {

    public class Ray : IPackage {

        public NativeType nativeType => NativeType.RAY;

        public Vector origin; // pt
        public Vector direction; // dir
        public float length; // length

        public Ray(AC2Reader data) {
            origin = data.ReadVector();
            direction = data.ReadVector();
            length = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(origin);
            data.Write(direction);
            data.Write(length);
        }
    }
}
