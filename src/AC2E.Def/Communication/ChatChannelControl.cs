using System.Collections.Generic;

namespace AC2E.Def {

    public class ChatChannelControl : IPackage {

        public PackageType packageType => PackageType.ChatChannelControl;

        public uint unk1;
        public Dictionary<uint, StringInfo> regionNameTable; // m_regionNameTable
        public uint unk2;

        public ChatChannelControl(AC2Reader data) {
            unk1 = data.ReadUInt32();
            data.ReadPkg<ARHash>(v => regionNameTable = v.to<uint, StringInfo>());
            unk2 = data.ReadUInt32();
        }
    }
}
