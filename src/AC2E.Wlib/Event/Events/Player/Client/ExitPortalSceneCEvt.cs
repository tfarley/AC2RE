using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ExitPortalSceneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalScene;

        // WM_Player::PostCEvt_ExitPortalScene
        public GMSceneInfoList _scenes;
        public double _delay;

        public ExitPortalSceneCEvt() {

        }

        public ExitPortalSceneCEvt(BinaryReader data) {
            _scenes = data.UnpackPackage<GMSceneInfoList>();
            _delay = data.UnpackDouble();
        }

        public void write(BinaryWriter data) {
            data.Pack(_scenes);
            data.Pack(_delay);
        }
    }
}
