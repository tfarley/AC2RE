namespace AC2RE.Definitions;

public class ChannelData : IHeapObject {

    public PackageType packageType => PackageType.ChannelData;

    public bool pendingRoomCreation; // m_fPendingRoomCreation
    public TextType type; // m_type
    public uint regionId; // m_regionID
    public uint roomId; // m_roomID
    public bool available; // m_available
    public FactionType factionType; // m_factionType
    public WPString name; // m_name

    public ChannelData() {

    }

    public ChannelData(AC2Reader data) {
        pendingRoomCreation = data.ReadBoolean();
        type = data.ReadEnum<TextType>();
        regionId = data.ReadUInt32();
        roomId = data.ReadUInt32();
        available = data.ReadBoolean();
        factionType = data.ReadEnum<FactionType>();
        data.ReadHO<WPString>(v => name = v);
    }

    public void write(AC2Writer data) {
        data.Write(pendingRoomCreation);
        data.WriteEnum(type);
        data.Write(regionId);
        data.Write(roomId);
        data.Write(available);
        data.WriteEnum(factionType);
        data.WriteHO(name);
    }
}
