namespace AC2RE.Definitions;

public class FloatScaleDuple : IHeapObject {

    public PackageType packageType => PackageType.FloatScaleDuple;

    public float value; // m_value
    public float level; // m_level

    public FloatScaleDuple(AC2Reader data) {
        value = data.ReadSingle();
        level = data.ReadSingle();
    }
}
