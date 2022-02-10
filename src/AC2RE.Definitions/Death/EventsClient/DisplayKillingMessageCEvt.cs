namespace AC2RE.Definitions;

public class DisplayKillingMessageCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Death__DisplayKillingMsg;

    // WM_Death::PostCEvt_DeathSystem_DisplayKillingMsg
    public StringInfo defenderName; // _siDefender

    public DisplayKillingMessageCEvt() {

    }

    public DisplayKillingMessageCEvt(AC2Reader data) {
        defenderName = data.UnpackHeapObject<StringInfo>();
    }

    public void write(AC2Writer data) {
        data.Pack(defenderName);
    }
}
