using System.Collections.Generic;

namespace AC2RE.Definitions;

public class HistoryList : IHeapObject {

    public PackageType packageType => PackageType.HistoryList;

    public List<IHeapObject> history; // m_history
    public uint size; // m_size
    public int index; // m_index

    public HistoryList(AC2Reader data) {
        data.ReadHO<RList>(v => history = v);
        size = data.ReadUInt32();
        index = data.ReadInt32();
    }
}
