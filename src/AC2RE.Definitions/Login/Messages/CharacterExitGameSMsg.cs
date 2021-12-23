namespace AC2RE.Definitions;

public class CharacterExitGameSMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Login__CharExitGame;

    // ECM_Login::RecvEvt_CharExitGame
    public InstanceId characterId;

    public CharacterExitGameSMsg(AC2Reader data) {
        characterId = data.ReadInstanceId();
    }
}
