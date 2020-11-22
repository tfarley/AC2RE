using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class PropertyDesc {

        public DataId did; // m_DID
        public uint version; // version
        public List<BlockProperty> blockProps; // block_props

        public PropertyDesc(AC2Reader data) {
            did = data.ReadDataId();
            version = data.ReadUInt32();
            blockProps = data.ReadList(() => new BlockProperty(data));
        }
    }
}
