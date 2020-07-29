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
    }
}
