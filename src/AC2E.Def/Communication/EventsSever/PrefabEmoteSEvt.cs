namespace AC2E.Def {

    public class PrefabEmoteSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__PrefabEmote;

        // WM_Communication::SendSEvt_PrefabEmote
        public uint _emoteID;

        public PrefabEmoteSEvt(AC2Reader data) {
            _emoteID = data.UnpackUInt32();
        }
    }
}
