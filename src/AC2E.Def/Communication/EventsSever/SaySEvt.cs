namespace AC2E.Def {

    public class SaySEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__Say;

        // WM_Communication::SendSEvt_Say
        public uint weenieChatFlags; // _weenieChatFlags
        public StringInfo text; // _msg

        public SaySEvt(AC2Reader data) {
            weenieChatFlags = data.UnpackUInt32();
            text = data.UnpackPackage<StringInfo>();
        }
    }
}
