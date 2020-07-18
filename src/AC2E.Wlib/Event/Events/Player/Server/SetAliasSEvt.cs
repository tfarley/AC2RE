using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class SetAliasSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetAlias;

        // WM_Player::SendSEvt_SetAlias
        public bool _add;
        public WPString _text;
        public WPString _alias;

        public SetAliasSEvt(BinaryReader data) {
            _add = data.UnpackUInt32() != 0;
            _text = data.UnpackPackage<WPString>();
            _alias = data.UnpackPackage<WPString>();
        }
    }
}
