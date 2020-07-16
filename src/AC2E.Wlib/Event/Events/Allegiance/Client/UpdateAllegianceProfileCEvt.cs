using System.IO;

namespace AC2E.WLib {

    public class UpdateAllegianceProfileCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__UpdateAllegianceProfile;

        // WM_Allegiance::PostCEvt_UpdateAllegianceProfile
        public AllegianceProfilePkg _prof;

        public UpdateAllegianceProfileCEvt() {

        }

        public UpdateAllegianceProfileCEvt(BinaryReader data) {
            _prof = data.UnpackPackage<AllegianceProfilePkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_prof);
        }
    }
}
