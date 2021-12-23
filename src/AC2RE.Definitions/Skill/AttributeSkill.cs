namespace AC2RE.Definitions;

public class AttributeSkill : Skill {

    public override PackageType packageType => PackageType.AttributeSkill;

    public DataId recommendedLevelTableDid; // m_didRecommendedLevelTable
    public double attributeMod; // m_attributeMod
    public uint category; // m_category

    public AttributeSkill(AC2Reader data) : base(data) {
        recommendedLevelTableDid = data.ReadDataId();
        attributeMod = data.ReadDouble();
        category = data.ReadUInt32();
    }
}
