using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class PrefabEmoteSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__PrefabEmote;

        // WM_Communication::SendSEvt_PrefabEmote
        public uint _emoteID;

        public PrefabEmoteSEvt(BinaryReader data) {
            _emoteID = data.UnpackUInt32();
        }
    }
}
