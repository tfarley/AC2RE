using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class UpdateExaminationProfileCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Examination__UpdateExaminationProfile;

        // WM_Examination::PostCEvt_UpdateExaminationProfile
        public ExaminationProfilePkg _examineProf;

        public UpdateExaminationProfileCEvt() {

        }

        public UpdateExaminationProfileCEvt(BinaryReader data) {
            _examineProf = data.UnpackPackage<ExaminationProfilePkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_examineProf);
        }
    }
}
