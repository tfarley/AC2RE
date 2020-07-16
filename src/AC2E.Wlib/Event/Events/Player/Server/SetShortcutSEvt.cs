using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class SetShortcutSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetShortcut;

        // WM_Player::SendSEvt_SetShortcut
        public ShortcutInfo _shortcut;
        public uint _index;

        public SetShortcutSEvt(BinaryReader data) {
            _shortcut = data.UnpackPackage<ShortcutInfo>();
            _index = data.UnpackUInt32();
        }
    }
}
