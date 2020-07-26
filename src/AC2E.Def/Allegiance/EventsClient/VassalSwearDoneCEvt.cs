namespace AC2E.Def {

    public class VassalSwearDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Vassal_SwearDone;

        // WM_Allegiance::PostCEvt_Vassal_SwearDone
        public uint error; // _etype
        public StringInfo patronName; // _patron_name
        public InstanceId patronId; // _patron

        public VassalSwearDoneCEvt() {

        }

        public VassalSwearDoneCEvt(AC2Reader data) {
            error = data.UnpackUInt32();
            patronName = data.UnpackPackage<StringInfo>();
            patronId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(error);
            data.Pack(patronName);
            data.Pack(patronId);
        }
    }
}
