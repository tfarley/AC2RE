using System.IO;

namespace AC2E.Def {

    public class EnterPortalSceneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__EnterPortalScene;

        // WM_Player::PostCEvt_EnterPortalScene
        public GMSceneInfoList _scenes;
        public double _delay;

        public EnterPortalSceneCEvt() {

        }

        public EnterPortalSceneCEvt(BinaryReader data) {
            _scenes = data.UnpackPackage<GMSceneInfoList>();
            _delay = data.UnpackDouble();
        }

        public void write(BinaryWriter data) {
            data.Pack(_scenes);
            data.Pack(_delay);
        }
    }
}
