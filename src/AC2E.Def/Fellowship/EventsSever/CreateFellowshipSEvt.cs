namespace AC2E.Def {

    public class CreateFellowshipSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__CreateFellowship;

        // WM_Fellowship::SendSEvt_CreateFellowship
        public uint _lootingMethod;
        public bool _social;
        public WPString _name;

        public CreateFellowshipSEvt(AC2Reader data) {
            _lootingMethod = data.UnpackUInt32();
            _social = data.UnpackBoolean();
            _name = data.UnpackPackage<WPString>();
        }
    }
}
