namespace AC2E.Def {

    public class HandlePlayScenesDoneSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Quest__HandlePlayScenesDone;

        // WM_Quest::SendSEvt_HandlePlayScenesDone
        public GMSceneInfoList playScenes; // _playScenes

        public HandlePlayScenesDoneSEvt(AC2Reader data) {
            playScenes = data.UnpackPackage<GMSceneInfoList>();
        }
    }
}
