namespace AC2RE.Definitions;

public class MaterialField {

    // MaterialField
    public RMFieldType fieldType; // fieldType
    public RMDataType dataType; // dataType
    public uint layerIndex; // layerIndex
    public uint indices; // tcIndex, stageIndex, modifierIndex

    public MaterialField(AC2Reader data) {
        fieldType = data.ReadEnum<RMFieldType>();
        dataType = data.ReadEnum<RMDataType>();
        layerIndex = data.ReadUInt32();
        indices = data.ReadUInt32();
    }
}
