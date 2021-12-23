namespace AC2RE.Definitions;

public class UpdateExaminationProfileCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Examination__UpdateExaminationProfile;

    // WM_Examination::PostCEvt_UpdateExaminationProfile
    public ExaminationProfile profile; // _examineProf

    public UpdateExaminationProfileCEvt() {

    }

    public UpdateExaminationProfileCEvt(AC2Reader data) {
        profile = data.UnpackPackage<ExaminationProfile>();
    }

    public void write(AC2Writer data) {
        data.Pack(profile);
    }
}
