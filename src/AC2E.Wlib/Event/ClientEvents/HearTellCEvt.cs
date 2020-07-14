using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class HearTellCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CHearTell;

        // WM_Communication::PostCEvt_CHearTell
        public uint _weenieChatFlags;
        public StringInfo _msg;
        public StringInfo _teller;
        public InstanceId _tellerID;

        public HearTellCEvt() {

        }

        public HearTellCEvt(BinaryReader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfo>();
            _teller = data.UnpackPackage<StringInfo>();
            _tellerID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_weenieChatFlags);
            data.Pack(_msg);
            data.Pack(_teller);
            data.Pack(_tellerID);
        }
    }
}
