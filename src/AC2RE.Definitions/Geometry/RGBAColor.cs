namespace AC2RE.Definitions {

    public struct RGBAColor {

        public float r;
        public float g;
        public float b;
        public float a;

        public RGBAColor(float r, float g, float b, float a) {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public override string ToString() {
            return $"<{r}, {g}, {b}, {a}>";
        }
    }
}
