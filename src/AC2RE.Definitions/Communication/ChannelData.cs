namespace AC2RE.Definitions {

    public class ChannelData : IPackage {

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
            type = (TextType)data.ReadUInt32();
            regionId = data.ReadUInt32();
            roomId = data.ReadUInt32();
            available = data.ReadBoolean();
            factionType = (FactionType)data.ReadUInt32();
            data.ReadPkg<WPString>(v => name = v);
        }

        public void write(AC2Writer data) {
            data.Write(pendingRoomCreation);
            data.Write((uint)type);
            data.Write(regionId);
            data.Write(roomId);
            data.Write(available);
            data.Write((uint)factionType);
            data.WritePkg(name);
        }
    }
}
