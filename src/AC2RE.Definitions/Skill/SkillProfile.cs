namespace AC2RE.Definitions;

public class SkillProfile : IHeapObject {

    public PackageType packageType => PackageType.SkillProfile;

    public int level; // level
    public SkillId skillId; // skill

    public SkillProfile(AC2Reader data) {
        level = data.ReadInt32();
        skillId = (SkillId)data.ReadUInt32();
    }
}
