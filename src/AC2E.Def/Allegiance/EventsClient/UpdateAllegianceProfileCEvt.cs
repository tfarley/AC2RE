using System.IO;

namespace AC2E.Def {

    public class UpdateAllegianceProfileCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__UpdateAllegianceProfile;

        // WM_Allegiance::PostCEvt_UpdateAllegianceProfile
        public AllegianceProfile _prof;

        public UpdateAllegianceProfileCEvt() {

        }

        public UpdateAllegianceProfileCEvt(BinaryReader data) {
            _prof = data.UnpackPackage<AllegianceProfile>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_prof);
        }
    }
}
