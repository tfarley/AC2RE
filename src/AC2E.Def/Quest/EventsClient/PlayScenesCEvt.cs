using System.IO;

namespace AC2E.Def {

    public class PlayScenesCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__PlayScenes;

        // WM_Quest::PostCEvt_PlayScenes
        public GMSceneInfoList _scenes;

        public PlayScenesCEvt() {

        }

        public PlayScenesCEvt(BinaryReader data) {
            _scenes = data.UnpackPackage<GMSceneInfoList>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_scenes);
        }
    }
}
