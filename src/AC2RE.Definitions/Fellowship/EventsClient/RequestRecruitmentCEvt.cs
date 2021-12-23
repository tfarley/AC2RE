namespace AC2RE.Definitions;

public class RequestRecruitmentCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__RequestRecruitment;

    // WM_Fellowship::PostCEvt_RequestRecruitment
    public StringInfo fellowshipName; // _fellowship_name
    public StringInfo leaderName; // _leader_name
    public InstanceId leaderId; // _leader

    public RequestRecruitmentCEvt() {

    }

    public RequestRecruitmentCEvt(AC2Reader data) {
        fellowshipName = data.UnpackPackage<StringInfo>();
        leaderName = data.UnpackPackage<StringInfo>();
        leaderId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(fellowshipName);
        data.Pack(leaderName);
        data.Pack(leaderId);
    }
}
