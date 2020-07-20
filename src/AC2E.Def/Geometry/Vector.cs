namespace AC2E.Def {

    public struct Vector : IPackage {

        public NativeType nativeType => NativeType.VECTOR;

        public float x;
        public float y;
        public float z;

        public Vector(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(this);
        }

        public override string ToString() {
            return $"<{x}, {y}, {z}>";
        }
    }
}
