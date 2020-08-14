namespace AC2E.Def {

    public struct Matrix4 {

        public float[,] m;

        public Matrix4(AC2Reader data) {
            m = new float[4, 4];
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    m[i, j] = data.ReadSingle();
                }
            }
        }

        public void write(AC2Writer data) {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    data.Write(m[i, j]);
                }
            }
        }

        public override string ToString() {
            if (m == null) {
                return "null";
            }

            return $"{m[0, 0]} {m[0, 1]} {m[0, 2]} {m[0, 3]}\n{m[1, 0]} {m[1, 1]} {m[1, 2]} {m[1, 3]}\n{m[2, 0]} {m[2, 1]} {m[2, 2]} {m[2, 3]}\n{m[3, 0]} {m[3, 1]} {m[3, 2]} {m[3, 3]}";
        }
    }
}
