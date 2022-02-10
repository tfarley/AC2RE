namespace AC2RE.Definitions;

public class CraftSkillTitleScore : IHeapObject {

    public PackageType packageType => PackageType.CraftSkillTitleScore;

    public float total; // fTotal
    public float value; // fValue
    public float score; // fScore

    public CraftSkillTitleScore(AC2Reader data) {
        total = data.ReadSingle();
        value = data.ReadSingle();
        score = data.ReadSingle();
    }
}
