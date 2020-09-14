namespace AC2E.Def {

    public class ToolPermissionBlob : IPackage {

        public PackageType packageType => PackageType.ToolPermissionBlob;

        public AList requiredSkills; // m_RequiredSkillList
        public uint skillLevel; // m_uiSkillLevel

        public ToolPermissionBlob(AC2Reader data) {
            data.ReadPkg<AList>(v => requiredSkills = v);
            skillLevel = data.ReadUInt32();
        }
    }
}
