using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class CharacterRenameSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__CharacterRename;

        // WM_Player::SendSEvt_CharacterRename
        public WPString _name;

        public CharacterRenameSEvt(BinaryReader data) {
            _name = data.UnpackPackage<WPString>();
        }
    }
}
