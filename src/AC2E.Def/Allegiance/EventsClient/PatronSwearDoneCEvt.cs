namespace AC2E.Def {

    public class PatronSwearDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Patron_SwearDone;

        // WM_Allegiance::PostCEvt_Patron_SwearDone
        public uint error; // _etype
        public StringInfo vassalName; // _vassal_name
        public InstanceId vassalId; // _vassal

        public PatronSwearDoneCEvt() {

        }

        public PatronSwearDoneCEvt(AC2Reader data) {
            error = data.UnpackUInt32();
            vassalName = data.UnpackPackage<StringInfo>();
            vassalId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(error);
            data.Pack(vassalName);
            data.Pack(vassalId);
        }
    }
}
