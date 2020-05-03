namespace AC2E.Def.Structs {

    public struct Heading {

        public float rotDegrees;

        public Heading(float rotDegrees) {
            this.rotDegrees = rotDegrees;
        }

        public override string ToString() {
            return rotDegrees.ToString();
        }
    }
}
