using System.Numerics;

namespace AC2RE.Definitions {

    public struct BBox {

        public Vector3 min; // min
        public Vector3 max; // max

        public BBox(AC2Reader data) {
            min = data.ReadVector();
            max = data.ReadVector();
        }

        public void write(AC2Writer data) {
            data.Write(min);
            data.Write(max);
        }
    }
}
