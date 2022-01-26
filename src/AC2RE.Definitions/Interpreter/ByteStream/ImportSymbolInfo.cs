using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ImportSymbolInfo {

    public string name; // m_name
    public List<uint> offsets; // m_offsets
    public int fromAddr; // m_from_addr

    public ImportSymbolInfo(AC2Reader data) {
        name = data.ReadString();
        offsets = data.ReadList(data.ReadUInt32);
        fromAddr = data.ReadInt32();
    }
}
