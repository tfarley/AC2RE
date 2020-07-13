using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class RefreshChannelsCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__RefreshChannels;

        // WM_Communication::PostCEvt_RefreshChannels
        public ARHashPkg<ChannelDataPkg> _channels;

        public RefreshChannelsCEvt() {

        }

        public RefreshChannelsCEvt(BinaryReader data) {
            _channels = data.UnpackPackage<ARHashPkg<IPackage>>().to<ChannelDataPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_channels);
        }
    }
}
