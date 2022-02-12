namespace AC2RE.Definitions;

public class CharGenVerificationMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Login__CharGenVerification;

    // ECM_Login::RecvEvt_CharGenVerification
    public CharGenResponse response; // response
    public CharacterIdentity characterIdentity; // _identity
    public uint weenieCharGenResult; // weenieCharGenResult

    public CharGenVerificationMsg() {

    }

    public CharGenVerificationMsg(AC2Reader data) {
        response = data.ReadEnum<CharGenResponse>();
        characterIdentity = new(data);
        weenieCharGenResult = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(response);
        characterIdentity.write(data);
        data.Write(weenieCharGenResult);
    }
}
