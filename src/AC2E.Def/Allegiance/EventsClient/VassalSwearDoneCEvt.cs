namespace AC2E.Def {

    public class VassalSwearDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Vassal_SwearDone;

        // WM_Allegiance::PostCEvt_Vassal_SwearDone
        public uint _etype;
        public StringInfo _patron_name;
        public InstanceId _patron;

        public VassalSwearDoneCEvt() {

        }

        public VassalSwearDoneCEvt(AC2Reader data) {
            _etype = data.UnpackUInt32();
            _patron_name = data.UnpackPackage<StringInfo>();
            _patron = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_etype);
            data.Pack(_patron_name);
            data.Pack(_patron);
        }
    }
}
