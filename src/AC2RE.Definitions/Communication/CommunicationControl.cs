using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CommunicationControl : IPackage {

        public PackageType packageType => PackageType.CommunicationControl;

        public uint unk1;
        public bool m_setupOK; // m_setupOK
        public Dictionary<uint, StringInfo> textTypeHash; // m_textTypeHash
        public uint unk2;
        public uint unk3;

        public CommunicationControl(AC2Reader data) {
            unk1 = data.ReadUInt32();
            unk2 = data.ReadUInt32();
            m_setupOK = data.ReadBoolean();
            data.ReadPkg<ARHash>(v => textTypeHash = v.to<uint, StringInfo>());
            unk3 = data.ReadUInt32();
        }
    }
}
