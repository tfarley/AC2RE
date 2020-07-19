using System.IO;

namespace AC2E.Def {

    public class UpdateExaminationProfileCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Examination__UpdateExaminationProfile;

        // WM_Examination::PostCEvt_UpdateExaminationProfile
        public ExaminationProfile _examineProf;

        public UpdateExaminationProfileCEvt() {

        }

        public UpdateExaminationProfileCEvt(BinaryReader data) {
            _examineProf = data.UnpackPackage<ExaminationProfile>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_examineProf);
        }
    }
}
