using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class GMSceneInfo : IPackage {

        public NativeType nativeType => NativeType.GMSCENEINFO;

        public SceneId id; // _ID
        public StringInfo name; // _siName
        public uint actId; // _actID
        public uint sceneNum; // _uiSceneNum
        public bool hidden; // _bIsHidden
        public bool playable; // _bIsPlayable
        public List<GMKeyframe> keyframes; // _keyframeList

        public GMSceneInfo() {

        }

        public GMSceneInfo(AC2Reader data) {
            id = (SceneId)data.ReadUInt32();
            name = new(data);
            actId = data.ReadUInt32();
            sceneNum = data.ReadUInt32();
            hidden = data.ReadBoolean();
            playable = data.ReadBoolean();
            keyframes = data.ReadList(() => new GMKeyframe(data));
        }

        public void write(AC2Writer data) {
            data.Write((uint)id);
            name.write(data);
            data.Write(actId);
            data.Write(sceneNum);
            data.Write(hidden);
            data.Write(playable);
            data.Write(keyframes, v => v.write(data));
        }
    }
}
