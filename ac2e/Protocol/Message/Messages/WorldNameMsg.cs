using System.IO;

public class WorldNameMsg : INetMessage {

    public NetQueue queue => NetQueue.NET_QUEUE_EVENT;

    public MessageOpcode opcode => MessageOpcode.Evt_Admin__WorldName_ID;

    public uint numConnections;
    public uint maxConnections;
    public uint unk1 = 0x00010000; // TODO: What is this?
    public string worldName;
    public uint unk2 = 0x34DDF9FC; // TODO: What is this?
    public uint unk3 = 0x40A633CB; // TODO: What is this?
    public ushort unk4; // TODO: What is this?

    public void write(BinaryWriter data) {
        data.Write(numConnections);
        data.Write(maxConnections);
        data.Write(unk1);
        data.WriteEncryptedString(worldName);
        data.Write(unk2);
        data.Write(unk3);
        data.Write(unk4);
    }
}
