using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ChatChannelControl : IPackage {

    public PackageType packageType => PackageType.ChatChannelControl;

    public uint pad1;
    public Dictionary<uint, StringInfo> regionNameTable; // m_regionNameTable
    public uint pad2;

    public ChatChannelControl(AC2Reader data) {
        pad1 = data.ReadUInt32();
        data.ReadPkg<ARHash>(v => regionNameTable = v.to<uint, StringInfo>());
        pad2 = data.ReadUInt32();
    }
}
