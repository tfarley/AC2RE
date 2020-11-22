namespace AC2RE.Definitions {

    public class HandlePlayScenesDoneSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Quest__HandlePlayScenesDone;

        // WM_Quest::SendSEvt_HandlePlayScenesDone
        public GMSceneInfoList scenes; // _playScenes

        public HandlePlayScenesDoneSEvt(AC2Reader data) {
            scenes = data.UnpackPackage<GMSceneInfoList>();
        }
    }
}
