namespace AC2E.Def {

    public class PatronSwearDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Patron_SwearDone;

        // WM_Allegiance::PostCEvt_Patron_SwearDone
        public uint _etype;
        public StringInfo _vassal_name;
        public InstanceId _vassal;

        public PatronSwearDoneCEvt() {

        }

        public PatronSwearDoneCEvt(AC2Reader data) {
            _etype = data.UnpackUInt32();
            _vassal_name = data.UnpackPackage<StringInfo>();
            _vassal = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_etype);
            data.Pack(_vassal_name);
            data.Pack(_vassal);
        }
    }
}
