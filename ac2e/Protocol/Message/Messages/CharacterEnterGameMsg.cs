using System.IO;

public class CharacterEnterGameMsg : INetMessage {

    public NetQueue queue => NetQueue.NET_QUEUE_LOGON;

    public MessageOpcode opcode => MessageOpcode.CHARACTER_ENTER_GAME_EVENT;

    public ulong characterId;
    public string accountName;

    public CharacterEnterGameMsg(BinaryReader data) {
        characterId = data.ReadUInt64();
        accountName = data.ReadEncryptedString();
    }
}
