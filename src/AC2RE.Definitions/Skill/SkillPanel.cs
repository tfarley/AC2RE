using System.Collections.Generic;

namespace AC2RE.Definitions;

public class SkillPanel : MasterListMember {

    public override PackageType packageType => PackageType.SkillPanel;

    public DataId backgroundDid; // mBackground
    public Dictionary<uint, SkillUINode> nodeHash; // mNodeHash
    public StringInfo description; // mDesc
    public DataId iconDid; // mIcon
    public StringInfo name; // mName

    public SkillPanel(AC2Reader data) : base(data) {
        backgroundDid = data.ReadDataId();
        data.ReadHO<ARHash>(v => nodeHash = v.to<uint, SkillUINode>());
        data.ReadHO<StringInfo>(v => description = v);
        iconDid = data.ReadDataId();
        data.ReadHO<StringInfo>(v => name = v);
    }
}
