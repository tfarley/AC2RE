namespace AC2RE.Definitions;

public class RemoveRecipeDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__RemoveRecipe_Done;

    // CPlayer::RecvCEvt_RemoveRecipe_Done
    public DataId recipeDid; // didRecipe

    public RemoveRecipeDoneCEvt() {

    }

    public RemoveRecipeDoneCEvt(AC2Reader data) {
        recipeDid = data.UnpackDataId();
    }

    public void write(AC2Writer data) {
        data.Pack(recipeDid);
    }
}
