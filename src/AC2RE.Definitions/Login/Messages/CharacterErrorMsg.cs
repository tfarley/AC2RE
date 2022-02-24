namespace AC2RE.Definitions;

public class CharacterErrorMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Login__CharacterError;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // ECM_Login::RecvEvt_CharacterError
    public CharError error; // error

    public CharacterErrorMsg(AC2Reader data) {
        error = data.ReadEnum<CharError>();
    }
}
