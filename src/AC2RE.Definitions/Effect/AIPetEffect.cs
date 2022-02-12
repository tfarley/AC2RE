namespace AC2RE.Definitions;

public class AIPetEffect : GenesisEffect {

    public override PackageType packageType => PackageType.AIPetEffect;

    public AIPetClass petClass; // m_petClass

    public AIPetEffect(AC2Reader data) : base(data) {
        petClass = data.ReadEnum<AIPetClass>();
    }
}
