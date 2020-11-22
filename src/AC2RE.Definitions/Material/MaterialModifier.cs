using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class MaterialModifier {

        public DataId did; // m_DID
        public List<MaterialProperty> properties; // properties

        public MaterialModifier(AC2Reader data) {
            did = data.ReadDataId();
            properties = data.ReadList(() => new MaterialProperty(data));
        }
    }
}
