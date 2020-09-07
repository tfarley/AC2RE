using System.Numerics;

namespace AC2E.Def {

    public struct VectorPkg : IPackage {

        public NativeType nativeType => NativeType.VECTOR;

        public Vector3 v;

        public VectorPkg(Vector3 v) {
            this.v = v;
        }

        public void write(AC2Writer data) {
            data.Write(v);
        }

        public override string ToString() {
            return v.ToString();
        }
    }
}
