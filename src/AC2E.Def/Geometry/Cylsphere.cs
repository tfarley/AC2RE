namespace AC2E.Def {

    public class Cylsphere {

        public Vector lowPoint; // low_pt
        public float height; // height
        public float radius; // radius

        public Cylsphere(AC2Reader data) {
            lowPoint = data.ReadVector();
            height = data.ReadSingle();
            radius = data.ReadSingle();
        }
    }
}
