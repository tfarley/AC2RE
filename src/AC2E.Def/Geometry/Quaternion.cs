namespace AC2E.Def {

    public struct Quaternion {

        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override string ToString() {
            return $"<{x}, {y}, {z}, {w}>";
        }
    }
}
