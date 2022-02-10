namespace AC2RE.Definitions;

public class ACFraction : IHeapObject {

    public PackageType packageType => PackageType.ACFraction;

    public IHeapObject abilityCalculator; // m_ac
    public float val; // m_fVal

    public ACFraction(AC2Reader data) {
        data.ReadHO<IHeapObject>(v => abilityCalculator = v);
        val = data.ReadSingle();
    }
}
