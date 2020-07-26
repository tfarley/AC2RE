using System.Collections.Generic;

namespace AC2E.Def {

    public class GMSceneInfo : IPackage {

        public NativeType nativeType => NativeType.GMSCENEINFO;

        public uint id; // _ID
        public StringInfo name; // _siName
        public uint actId; // _actID
        public uint sceneNum; // _uiSceneNum
        public bool isHidden; // _bIsHidden
        public bool isPlayable; // _bIsPlayable
        public List<GMKeyframe> keyframes; // _keyframeList

        public GMSceneInfo() {

        }

        public GMSceneInfo(AC2Reader data) {
            id = data.ReadUInt32();
            name = new StringInfo(data);
            actId = data.ReadUInt32();
            sceneNum = data.ReadUInt32();
            isHidden = data.ReadBoolean();
            isPlayable = data.ReadBoolean();
            keyframes = data.ReadList(() => new GMKeyframe(data));
        }

        public void write(AC2Writer data) {
            data.Write(id);
            name.write(data);
            data.Write(actId);
            data.Write(sceneNum);
            data.Write(isHidden);
            data.Write(isPlayable);
            data.Write(keyframes, v => v.write(data));
        }
    }
}
