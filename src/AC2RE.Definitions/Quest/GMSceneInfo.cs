using System.Collections.Generic;

namespace AC2RE.Definitions;

public class GMSceneInfo : IHeapObject {

    public NativeType nativeType => NativeType.gmSceneInfo;

    public SceneId id; // _ID
    public StringInfo name; // _siName
    public ActId actId; // _actID
    public uint sceneNum; // _uiSceneNum
    public bool hidden; // _bIsHidden
    public bool playable; // _bIsPlayable
    public List<GMKeyframe> keyframes; // _keyframeList

    public GMSceneInfo() {

    }

    public GMSceneInfo(AC2Reader data) {
        id = data.ReadEnum<SceneId>();
        name = new(data);
        actId = data.ReadEnum<ActId>();
        sceneNum = data.ReadUInt32();
        hidden = data.ReadBoolean();
        playable = data.ReadBoolean();
        keyframes = data.ReadList(() => new GMKeyframe(data));
    }

    public void write(AC2Writer data) {
        data.WriteEnum(id);
        name.write(data);
        data.WriteEnum(actId);
        data.Write(sceneNum);
        data.Write(hidden);
        data.Write(playable);
        data.Write(keyframes, v => v.write(data));
    }
}
