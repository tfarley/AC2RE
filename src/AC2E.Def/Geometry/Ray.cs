using System.Numerics;

namespace AC2E.Def {

    public class Ray : IPackage {

        public NativeType nativeType => NativeType.RAY;

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
