namespace AC2RE.Definitions;

public class PlayScenesCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__PlayScenes;

    // WM_Quest::PostCEvt_PlayScenes
    public GMSceneInfoList scenes; // _scenes

    public PlayScenesCEvt() {

    }

    public PlayScenesCEvt(AC2Reader data) {
        scenes = data.UnpackHeapObject<GMSceneInfoList>();
    }

    public void write(AC2Writer data) {
        data.Pack(scenes);
    }
}
