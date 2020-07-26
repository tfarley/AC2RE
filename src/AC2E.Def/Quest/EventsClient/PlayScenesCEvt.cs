﻿namespace AC2E.Def {

    public class PlayScenesCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Quest__PlayScenes;

        // WM_Quest::PostCEvt_PlayScenes
        public GMSceneInfoList scenes; // _scenes

        public PlayScenesCEvt() {

        }

        public PlayScenesCEvt(AC2Reader data) {
            scenes = data.UnpackPackage<GMSceneInfoList>();
        }

        public void write(AC2Writer data) {
            data.Pack(scenes);
        }
    }
}
