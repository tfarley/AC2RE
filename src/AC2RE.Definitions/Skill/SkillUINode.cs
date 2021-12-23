using System.Collections.Generic;

namespace AC2RE.Definitions;

public class SkillUINode : IPackage {

    public NativeType nativeType => NativeType.SkillUINode;

    public SkillId skillId; // mEnum
    public uint type; // mType
    public StringInfo name; // mName
    public StringInfo description; // mDesc
    public DataId iconDid; // mIcon
    public List<SkillId> parentSkillIds; // mParents
    public List<SkillId> dependentSkillIds; // mDependencies
    public uint x; // mX
    public uint y; // mY
    public bool isPassive; // mIsPassive
    public bool untrainable; // mUntrainable
    public uint unk1;

    public SkillUINode(AC2Reader data) {
        skillId = (SkillId)data.ReadUInt32();
        type = data.ReadUInt32();
        name = new(data);
        description = new(data);
        iconDid = data.ReadDataId();
        x = data.ReadUInt32();
        y = data.ReadUInt32();
        parentSkillIds = data.ReadList(() => (SkillId)data.ReadUInt32());
        dependentSkillIds = data.ReadList(() => (SkillId)data.ReadUInt32());
        isPassive = data.ReadBoolean();
        untrainable = data.ReadBoolean();
        unk1 = data.ReadUInt32();
    }
}
