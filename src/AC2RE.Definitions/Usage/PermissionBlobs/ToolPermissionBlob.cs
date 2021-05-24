using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ToolPermissionBlob : IPackage {

        public PackageType packageType => PackageType.ToolPermissionBlob;

        public List<SkillId> requiredSkillIds; // m_RequiredSkillList
        public uint skillLevel; // m_uiSkillLevel

        public ToolPermissionBlob(AC2Reader data) {
            data.ReadPkg<AList>(v => requiredSkillIds = v.to<SkillId>());
            skillLevel = data.ReadUInt32();
        }
    }
}
