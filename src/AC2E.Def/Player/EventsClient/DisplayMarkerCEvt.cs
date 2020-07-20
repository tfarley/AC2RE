namespace AC2E.Def {

    public class DisplayMarkerCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__DisplayMarker;

        // WM_Player::PostCEvt_DisplayMarker
        public StringInfo _msg;
        public DataId _marker;
        public CellId _cell;
        public InstanceId _id;
        public uint _type;

        public DisplayMarkerCEvt() {

        }

        public DisplayMarkerCEvt(AC2Reader data) {
            _msg = data.UnpackPackage<StringInfo>();
            _marker = data.UnpackDataId();
            _cell = new CellId(data.UnpackUInt32());
            _id = data.UnpackInstanceId();
            _type = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_msg);
            data.Pack(_marker);
            data.Pack(_cell.id);
            data.Pack(_id);
            data.Pack(_type);
        }
    }
}
