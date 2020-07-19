using System.IO;

namespace AC2E.Def {

    public class ChannelData : IPackage {

        public PackageType packageType => PackageType.ChannelData;

        public bool m_fPendingRoomCreation;
        public TextType m_type;
        public uint m_regionID;
        public uint m_roomID;
        public bool m_available;
        public uint m_factionType;
        public WPString m_name;

        public ChannelData() {

        }

        public ChannelData(BinaryReader data, PackageRegistry registry) {
            m_fPendingRoomCreation = data.ReadUInt32() != 0;
            m_type = (TextType)data.ReadUInt32();
            m_regionID = data.ReadUInt32();
            m_roomID = data.ReadUInt32();
            m_available = data.ReadUInt32() != 0;
            m_factionType = data.ReadUInt32();
            data.ReadPkgRef<WPString>(v => m_name = v, registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_fPendingRoomCreation ? (uint)1 : (uint)0);
            data.Write((uint)m_type);
            data.Write(m_regionID);
            data.Write(m_roomID);
            data.Write(m_available ? (uint)1 : (uint)0);
            data.Write(m_factionType);
            data.Write(m_name, registry);
        }
    }
}
