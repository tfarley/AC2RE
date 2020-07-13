using AC2E.Def;
using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class DisplayStringInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__DisplayStringInfo;

        // WM_Communication::PostCEvt_DisplayStringInfo
        public StringInfoPkg _msg;
        public TextType _type;

        public DisplayStringInfoCEvt() {

        }

        public DisplayStringInfoCEvt(BinaryReader data) {
            _msg = data.UnpackPackage<StringInfoPkg>();
            _type = (TextType)data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_msg);
            data.Pack((uint)_type);
        }
    }
}
