namespace AC2E.Def {

    public class PrefabEmoteSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__PrefabEmote;

        // WM_Communication::SendSEvt_PrefabEmote
        public uint emoteId; // _emoteID

        public PrefabEmoteSEvt(AC2Reader data) {
            emoteId = data.UnpackUInt32();
        }
    }
}
