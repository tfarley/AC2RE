using System.Collections.Generic;

namespace AC2E.Def {

    public class GMSceneInfo : IPackage {

        public NativeType nativeType => NativeType.GMSCENEINFO;

        public uint _ID;
        public StringInfo _siName;
        public uint _actID;
        public uint _uiSceneNum;
        public bool _bIsHidden;
        public bool _bIsPlayable;
        public List<GMKeyframe> _keyframeList;

        public GMSceneInfo() {

        }

        public GMSceneInfo(AC2Reader data) {
            _ID = data.ReadUInt32();
            _siName = new StringInfo(data);
            _actID = data.ReadUInt32();
            _uiSceneNum = data.ReadUInt32();
            _bIsHidden = data.ReadBoolean();
            _bIsPlayable = data.ReadBoolean();
            _keyframeList = data.ReadList(() => new GMKeyframe(data));
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(_ID);
            _siName.write(data);
            data.Write(_actID);
            data.Write(_uiSceneNum);
            data.Write(_bIsHidden ? (uint)1 : (uint)0);
            data.Write(_bIsPlayable ? (uint)1 : (uint)0);
            data.Write(_keyframeList, v => v.write(data, registry));
        }
    }
}
