namespace AC2RE.Definitions;

public class Heading : IHeapObject {

    public NativeType nativeType => NativeType.Heading;

    // Enum Heading::unit_type
    public enum Unit : uint {
        DEGREES, // DEGREES
        RADIANS, // RADIANS
    }

    public float rotDegrees;

    public Heading(float rotDegrees) {
        this.rotDegrees = rotDegrees;
    }

    public override string ToString() {
        return rotDegrees.ToString();
    }
}
