using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CharacterSetMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Login__CharacterSet;

    // CharacterSet
    public List<CharacterIdentity> characters; // set_
    public List<CharacterIdentity> deletedCharacters; // delSet_
    public uint status; // status_
    public uint numAllowedCharacters; // numAllowedCharacters_
    public string accountName; // account_
    public bool useTurbineChat; // m_fUseTurbineChat
    public bool hasLegions;
    public uint unk1;

    public CharacterSetMsg() {

    }

    public CharacterSetMsg(AC2Reader data) {
        characters = data.ReadList(() => new CharacterIdentity(data));
        deletedCharacters = data.ReadList(() => new CharacterIdentity(data));
        status = data.ReadUInt32();
        numAllowedCharacters = data.ReadUInt32();
        accountName = data.ReadString();
        useTurbineChat = data.ReadBoolean();
        hasLegions = data.ReadBoolean();
        unk1 = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(characters, v => v.write(data));
        data.Write(deletedCharacters, v => v.write(data));
        data.Write(status);
        data.Write(numAllowedCharacters);
        data.Write(accountName);
        data.Write(useTurbineChat);
        data.Write(hasLegions);
        data.Write(unk1);
    }
}
