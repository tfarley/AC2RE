namespace AC2RE.Definitions {

    public class EnterPortalSceneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__EnterPortalScene;

        // WM_Player::PostCEvt_EnterPortalScene
        public GMSceneInfoList scenes; // _scenes
        public double delay; // _delay

        public EnterPortalSceneCEvt() {

        }

        public EnterPortalSceneCEvt(AC2Reader data) {
            scenes = data.UnpackPackage<GMSceneInfoList>();
            delay = data.UnpackDouble();
        }

        public void write(AC2Writer data) {
            data.Pack(scenes);
            data.Pack(delay);
        }
    }
}
