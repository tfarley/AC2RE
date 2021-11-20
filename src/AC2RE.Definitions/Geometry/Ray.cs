using System.Numerics;

namespace AC2RE.Definitions {

    public class Ray : IPackage {

        public NativeType nativeType => NativeType.Ray;

        public Vector3 origin; // pt
        public Vector3 direction; // dir
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
