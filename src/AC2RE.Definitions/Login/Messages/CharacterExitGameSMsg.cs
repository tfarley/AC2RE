namespace AC2RE.Definitions;

public class CharacterExitGameSMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.LOGON;
    public MessageOpcode opcode => MessageOpcode.Login__CharExitGame;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // ECM_Login::RecvEvt_CharExitGame
    public InstanceId characterId;

    public CharacterExitGameSMsg(AC2Reader data) {
        characterId = data.ReadInstanceId();
    }
}
