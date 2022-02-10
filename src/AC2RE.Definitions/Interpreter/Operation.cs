namespace AC2RE.Definitions;

public class Operation : IHeapObject {

    public PackageType packageType => PackageType.Operation;

    public float operand; // m_operand

    public Operation(AC2Reader data) {
        operand = data.ReadSingle();
    }
}
