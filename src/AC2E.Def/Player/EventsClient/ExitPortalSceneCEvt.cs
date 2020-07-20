namespace AC2E.Def {

    public class ExitPortalSceneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalScene;

        // WM_Player::PostCEvt_ExitPortalScene
        public GMSceneInfoList _scenes;
        public double _delay;

        public ExitPortalSceneCEvt() {

        }

        public ExitPortalSceneCEvt(AC2Reader data) {
            _scenes = data.UnpackPackage<GMSceneInfoList>();
            _delay = data.UnpackDouble();
        }

        public void write(AC2Writer data) {
            data.Pack(_scenes);
            data.Pack(_delay);
        }
    }
}
