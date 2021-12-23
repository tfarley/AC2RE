namespace AC2RE.Definitions;

public class CharacterDeletionSMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.LOGON;
    public MessageOpcode opcode => MessageOpcode.Login__CharacterDeletion;

    // ECM_Login::RecvEvt_CharacterDeletion
    public string accountName;
    public InstanceId characterId; // id

    public CharacterDeletionSMsg(AC2Reader data) {
        accountName = data.ReadString();
        characterId = data.ReadInstanceId();
    }
}
