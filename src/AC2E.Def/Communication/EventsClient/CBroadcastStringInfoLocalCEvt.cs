using System.IO;

namespace AC2E.Def {

    public class CBroadcastStringInfoLocalCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CBroadcastStringInfoLocal;

        // WM_Communication::PostCEvt_CBroadcastStringInfoLocal
        public StringInfo _msg;
        public TextType _type;

        public CBroadcastStringInfoLocalCEvt() {

        }

        public CBroadcastStringInfoLocalCEvt(BinaryReader data) {
            _msg = data.UnpackPackage<StringInfo>();
            _type = (TextType)data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_msg);
            data.Pack((uint)_type);
        }
    }
}
