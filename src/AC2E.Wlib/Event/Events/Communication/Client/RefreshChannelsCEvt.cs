using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RefreshChannelsCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__RefreshChannels;

        // WM_Communication::PostCEvt_RefreshChannels
        public ARHash<ChannelDataPkg> _channels;

        public RefreshChannelsCEvt() {

        }

        public RefreshChannelsCEvt(BinaryReader data) {
            _channels = data.UnpackPackage<ARHash<IPackage>>().to<ChannelDataPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_channels);
        }
    }
}
