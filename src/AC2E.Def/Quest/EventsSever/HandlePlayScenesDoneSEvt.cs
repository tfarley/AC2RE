using System.IO;

namespace AC2E.Def {

    public class HandlePlayScenesDoneSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Quest__HandlePlayScenesDone;

        // WM_Quest::SendSEvt_HandlePlayScenesDone
        public GMSceneInfoList _playScenes;

        public HandlePlayScenesDoneSEvt(BinaryReader data) {
            _playScenes = data.UnpackPackage<GMSceneInfoList>();
        }
    }
}
