using System.Collections.Generic;

namespace AC2E.Def {

    public class SkillPanel : MasterListMember {

        public override PackageType packageType => PackageType.SkillPanel;

        public DataId backgroundDid; // mBackground
        public Dictionary<uint, SkillUINode> nodeHash; // mNodeHash
        public StringInfo description; // mDesc
        public DataId iconDid; // mIcon
        public StringInfo name; // mName

        public SkillPanel(AC2Reader data) : base(data) {
            backgroundDid = data.ReadDataId();
            data.ReadPkg<ARHash>(v => nodeHash = v.to<uint, SkillUINode>());
            data.ReadPkg<StringInfo>(v => description = v);
            iconDid = data.ReadDataId();
            data.ReadPkg<StringInfo>(v => name = v);
        }
    }
}
