namespace AC2E.Def {

    public class ChannelData : IPackage {

        public PackageType packageType => PackageType.ChannelData;

        public bool pendingRoomCreation; // m_fPendingRoomCreation
        public TextType type; // m_type
        public uint regionId; // m_regionID
        public uint toomId; // m_roomID
        public bool available; // m_available
        public uint factionType; // m_factionType
        public WPString name; // m_name

        public ChannelData() {

        }

        public ChannelData(AC2Reader data) {
            pendingRoomCreation = data.ReadBoolean();
            type = (TextType)data.ReadUInt32();
            regionId = data.ReadUInt32();
            toomId = data.ReadUInt32();
            available = data.ReadBoolean();
            factionType = data.ReadUInt32();
            data.ReadPkg<WPString>(v => name = v);
        }

        public void write(AC2Writer data) {
            data.Write(pendingRoomCreation);
            data.Write((uint)type);
            data.Write(regionId);
            data.Write(toomId);
            data.Write(available);
            data.Write(factionType);
            data.WritePkg(name);
        }
    }
}
