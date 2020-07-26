﻿namespace AC2E.Def {

    public class RefreshChannelsCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__RefreshChannels;

        // WM_Communication::PostCEvt_RefreshChannels
        public ARHash<ChannelData> channels; // _channels

        public RefreshChannelsCEvt() {

        }

        public RefreshChannelsCEvt(AC2Reader data) {
            channels = data.UnpackPackage<ARHash<IPackage>>().to<ChannelData>();
        }

        public void write(AC2Writer data) {
            data.Pack(channels);
        }
    }
}
