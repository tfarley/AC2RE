using System.Collections.Generic;

namespace AC2E.Def {

    public class PropertyDesc {

        public class BlockProperty {

            public uint pName; // pname
            public DataId blockMapDid; // block_map
            public bool discreteBlockProperty; // discrete_block_property
            public List<BaseProperty> mapValues; // map_values

            public BlockProperty(AC2Reader data) {
                pName = data.ReadUInt32();
                blockMapDid = data.ReadDataId();
                discreteBlockProperty = data.ReadBoolean();
                mapValues = data.ReadList(() => new BaseProperty(data));
            }
        }

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
