using System.Collections.Generic;

namespace AC2E.Def {

    public class SkillUINode : IPackage {

        public NativeType nativeType => NativeType.SKILLUINODE;

        public bool enumVal; // mEnum
        public uint type; // mType
        public StringInfo name; // mName
        public StringInfo description; // mDesc
        public DataId iconDid; // mIcon
        public List<uint> parents; // mParents
        public List<uint> dependencies; // mDependencies
        public uint x; // mX
        public uint y; // mY
        public bool isPassive; // mIsPassive
        public bool untrainable; // mUntrainable
        public uint unk1;

        public SkillUINode(AC2Reader data) {
            enumVal = data.ReadBoolean();
            type = data.ReadUInt32();
            name = new StringInfo(data);
            description = new StringInfo(data);
            iconDid = data.ReadDataId();
            x = data.ReadUInt32();
            y = data.ReadUInt32();
            parents = data.ReadList(data.ReadUInt32);
            dependencies = data.ReadList(data.ReadUInt32);
            isPassive = data.ReadBoolean();
            untrainable = data.ReadBoolean();
            unk1 = data.ReadUInt32();
        }
    }
}
