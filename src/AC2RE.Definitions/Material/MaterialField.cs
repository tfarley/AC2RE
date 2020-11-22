namespace AC2RE.Definitions {

    public class MaterialField {

        public RMFieldType fieldType; // fieldType
        public RMDataType dataType; // dataType
        public uint layerIndex; // layerIndex
        public uint indices; // tcIndex, stageIndex, modifierIndex

        public MaterialField(AC2Reader data) {
            fieldType = (RMFieldType)data.ReadUInt32();
            dataType = (RMDataType)data.ReadUInt32();
            layerIndex = data.ReadUInt32();
            indices = data.ReadUInt32();
        }
    }
}
