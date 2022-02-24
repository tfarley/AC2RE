namespace AC2RE.Definitions;

public class CharacterExitGameCMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Login__CharExitGame;
    public OrderingType orderingType => OrderingType.UNORDERED;

    public CharacterExitGameCMsg() {

    }

    public CharacterExitGameCMsg(AC2Reader data) {

    }

    public void write(AC2Writer data) {

    }
}
