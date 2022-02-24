namespace AC2RE.Definitions;

public class CharacterEnterGameMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.LOGON;
    public MessageOpcode opcode => MessageOpcode.CHARACTER_ENTER_GAME_EVENT;
    public OrderingType orderingType => OrderingType.UNORDERED;

    public InstanceId characterId;
    public string accountName;

    public CharacterEnterGameMsg(AC2Reader data) {
        characterId = data.ReadInstanceId();
        accountName = data.ReadString();
    }
}
