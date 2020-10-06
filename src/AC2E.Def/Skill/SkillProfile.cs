namespace AC2E.Def {

    public class SkillProfile : IPackage {

        public PackageType packageType => PackageType.SkillProfile;

        public int level; // level
        public SkillId skill; // skill

        public SkillProfile(AC2Reader data) {
            level = data.ReadInt32();
            skill = (SkillId)data.ReadUInt32();
        }
    }
}
