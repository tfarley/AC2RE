namespace AC2E.Def {

    public struct BBox {

        public Vector min; // min
        public Vector max; // max

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
