using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CLandBlockData {

    // CLandBlockData
    public DataId did; // m_DID
    public List<byte> heights; // height
    public List<uint> cellInfos; // m_cellinfo
    public DataId landBlockInfoDid; // m_lbi_id
    public DataId pathmapDid; // m_pathmap_id

    public CLandBlockData(AC2Reader data) {
        did = data.ReadDataId();
        landBlockInfoDid = data.ReadDataId();
        pathmapDid = data.ReadDataId();
        heights = data.ReadList(data.ReadByte);
        cellInfos = data.ReadList(data.ReadUInt32);
        data.Align(4);
    }
}
