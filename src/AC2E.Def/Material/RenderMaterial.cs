using System.Collections.Generic;

namespace AC2E.Def {

    public class RenderMaterial {

        public DataId did; // m_DID
        public MaterialModifier properties; // properties
        public List<MaterialLayer> layers; // layers
        public SortMode sortMode; // sortMode

        public RenderMaterial(AC2Reader data) {
            did = data.ReadDataId();
            properties = new(data);
            layers = data.ReadList(() => new MaterialLayer(data));
            sortMode = (SortMode)data.ReadUInt32();
        }
    }
}
