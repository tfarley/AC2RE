namespace AC2E.Def {

    public struct Vector {

        public float x;
        public float y;
        public float z;

        public Vector(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString() {
            return $"<{x}, {y}, {z}>";
        }
    }
}
