namespace AC2RE.Definitions;

public class AddRecipeDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__AddRecipe_Done;

    // CPlayer::RecvCEvt_AddRecipe_Done
    public DataId recipeDid; // didRecipe

    public AddRecipeDoneCEvt() {

    }

    public AddRecipeDoneCEvt(AC2Reader data) {
        recipeDid = data.UnpackDataId();
    }

    public void write(AC2Writer data) {
        data.Pack(recipeDid);
    }
}
