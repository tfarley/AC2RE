namespace AC2RE.Definitions;

public class UpdateFactionStatusCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.PK__Faction_UpdateStatus;

    // WM_PK::PostCEvt_Faction_UpdateStatus
    public FactionStatus status; // _newStatus

    public UpdateFactionStatusCEvt() {

    }

    public UpdateFactionStatusCEvt(AC2Reader data) {
        status = data.UnpackEnum<FactionStatus>();
    }

    public void write(AC2Writer data) {
        data.PackEnum(status);
    }
}
