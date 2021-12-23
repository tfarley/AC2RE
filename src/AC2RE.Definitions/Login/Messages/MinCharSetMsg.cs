using System.Collections.Generic;
using System.Text;

namespace AC2RE.Definitions;

public class MinCharSetMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Login__MinCharSet;

    // MinimalCharacterSet
    public uint numAllowedCharacters; // m_numAllowedCharacters
    public string accountName; // m_account
    public List<string> characterNames; // m_names
    public List<InstanceId> characterIds; // m_iids

    public MinCharSetMsg() {

    }

    public MinCharSetMsg(AC2Reader data) {
        numAllowedCharacters = data.ReadUInt32();
        accountName = data.ReadString();
        characterNames = data.ReadList(() => data.ReadString(Encoding.Unicode));
        characterIds = data.ReadList(data.ReadInstanceId);
    }

    public void write(AC2Writer data) {
        data.Write(numAllowedCharacters);
        data.Write(accountName);
        data.Write(characterNames, v => data.Write(v, Encoding.Unicode));
        data.Write(characterIds, data.Write);
    }
}
