using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RefreshChannelsCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__RefreshChannels;

    // WM_Communication::PostCEvt_RefreshChannels
    public Dictionary<TextType, ChannelData> channels; // _channels

    public RefreshChannelsCEvt() {

    }

    public RefreshChannelsCEvt(AC2Reader data) {
        channels = data.UnpackHeapObject<ARHash>().to<TextType, ChannelData>();
    }

    public void write(AC2Writer data) {
        data.Pack(ARHash.from(channels));
    }
}
