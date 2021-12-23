namespace AC2RE.Definitions;

public class DisplayDeathMessageCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Death__DisplayDeathMsg;

    // WM_Death::PostCEvt_DeathSystem_DisplayDeathMsg
    public StringInfo lastAttackerName; // _siLastAttacker

    public DisplayDeathMessageCEvt() {

    }

    public DisplayDeathMessageCEvt(AC2Reader data) {
        lastAttackerName = data.UnpackPackage<StringInfo>();
    }

    public void write(AC2Writer data) {
        data.Pack(lastAttackerName);
    }
}
