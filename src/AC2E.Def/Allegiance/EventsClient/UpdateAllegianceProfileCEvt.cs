namespace AC2E.Def {

    public class UpdateAllegianceProfileCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__UpdateAllegianceProfile;

        // WM_Allegiance::PostCEvt_UpdateAllegianceProfile
        public AllegianceProfile _prof;

        public UpdateAllegianceProfileCEvt() {

        }

        public UpdateAllegianceProfileCEvt(AC2Reader data) {
            _prof = data.UnpackPackage<AllegianceProfile>();
        }

        public void write(AC2Writer data) {
            data.Pack(_prof);
        }
    }
}
