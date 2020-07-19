using System.IO;

namespace AC2E.Def {

    public class ClearMarkersCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ClearMarkers;

        // WM_Player::RecvCEvt_ClearMarkers
        public uint type;

        public ClearMarkersCEvt() {

        }

        public ClearMarkersCEvt(BinaryReader data) {
            type = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(type);
        }
    }
}
