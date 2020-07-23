namespace AC2E.Def {

    public class Heading : IPackage {

        public NativeType nativeType => NativeType.HEADING;

        public float rotDegrees;

        public Heading(float rotDegrees) {
            this.rotDegrees = rotDegrees;
        }

        public override string ToString() {
            return rotDegrees.ToString();
        }
    }
}
