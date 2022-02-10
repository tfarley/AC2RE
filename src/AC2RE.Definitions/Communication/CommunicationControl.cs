using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CommunicationControl : IHeapObject {

    public PackageType packageType => PackageType.CommunicationControl;

    public uint pad1;
    public uint pad2;
    public bool m_setupOK; // m_setupOK
    public Dictionary<uint, StringInfo> textTypeHash; // m_textTypeHash
    public uint pad3;

    public CommunicationControl(AC2Reader data) {
        pad1 = data.ReadUInt32();
        pad2 = data.ReadUInt32();
        m_setupOK = data.ReadBoolean();
        data.ReadHO<ARHash>(v => textTypeHash = v.to<uint, StringInfo>());
        pad3 = data.ReadUInt32();
    }
}
