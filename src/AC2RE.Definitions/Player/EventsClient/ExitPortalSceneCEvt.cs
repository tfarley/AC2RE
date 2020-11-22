namespace AC2RE.Definitions {

    public class ExitPortalSceneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalScene;

        // WM_Player::PostCEvt_ExitPortalScene
        public GMSceneInfoList scenes; // _scenes
        public double delay; // _delay

        public ExitPortalSceneCEvt() {

        }

        public ExitPortalSceneCEvt(AC2Reader data) {
            scenes = data.UnpackPackage<GMSceneInfoList>();
            delay = data.UnpackDouble();
        }

        public void write(AC2Writer data) {
            data.Pack(scenes);
            data.Pack(delay);
        }
    }
}
