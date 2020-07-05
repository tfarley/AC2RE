namespace AC2E.Def {

    public struct Quaternion {

        public float x;
        public float y;
        public float z;
        public float w;

        public override string ToString() {
            return $"<{x}, {y}, {z}, {w}>";
        }
    }
}
