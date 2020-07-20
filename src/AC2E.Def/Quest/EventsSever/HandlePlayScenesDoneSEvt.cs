namespace AC2E.Def {

    public class HandlePlayScenesDoneSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Quest__HandlePlayScenesDone;

        // WM_Quest::SendSEvt_HandlePlayScenesDone
        public GMSceneInfoList _playScenes;

        public HandlePlayScenesDoneSEvt(AC2Reader data) {
            _playScenes = data.UnpackPackage<GMSceneInfoList>();
        }
    }
}
