using System.Collections.Generic;

namespace AC2RE.Definitions;

public class FXData {

    // FXData
    public FXNode defaultNode; // m_default
    public Dictionary<uint, FXNode> terrainData; // m_terrainData

    public FXData() {

    }

    public FXData(AC2Reader data) {
        defaultNode = new(data);
        terrainData = data.ReadDictionary(data.ReadUInt32, () => new FXNode(data));
    }

    public void write(AC2Writer data) {
        defaultNode.write(data);
        data.Write(terrainData, data.Write, v => v.write(data));
    }
}
