using System.IO;

namespace AC2RE.Definitions;

public class AttributeDesc {

    // Const ATTRIBUTE_*
    public enum AttributeType : uint {
        UNDEF, // ATTRIBUTE_UNDEF
        ID, // ATTRIBUTE_ID
        INT, // ATTRIBUTE_INT
        FLOAT, // ATTRIBUTE_FLOAT
        BOOL, // ATTRIBUTE_BOOL
        ASCII, // ATTRIBUTE_ASCII
        STRING, // ATTRIBUTE_STRING
        DATAID, // ATTRIBUTE_DATAID
    }

    // AttributeDesc
    public uint id; // attributeID
    public AttributeType type; // type
    public StringId valStringId; // val_strID
    public uint valId; // val_ID
    public int valInt; // val_Int
    public float valFloat; // val_Float
    public bool valBool; // val_Bool
    public string valAscii; // val_ASCII
    public DataId valDid; // val_DataID

    public AttributeDesc() {

    }

    public AttributeDesc(AC2Reader data) {
        id = data.ReadUInt32();
        type = data.ReadEnum<AttributeType>();
        switch (type) {
            case AttributeType.ID:
                valId = data.ReadUInt32();
                break;
            case AttributeType.INT:
                valInt = data.ReadInt32();
                break;
            case AttributeType.FLOAT:
                valFloat = data.ReadSingle();
                break;
            case AttributeType.BOOL:
                valBool = data.ReadBoolean();
                break;
            case AttributeType.ASCII:
                valAscii = data.ReadString();
                break;
            case AttributeType.STRING:
                valStringId = data.ReadStringId();
                break;
            case AttributeType.DATAID:
                valDid = data.ReadDataId();
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }

    public void write(AC2Writer data) {
        data.Write(id);
        data.WriteEnum(type);
        switch (type) {
            case AttributeType.ID:
                data.Write(valId);
                break;
            case AttributeType.INT:
                data.Write(valInt);
                break;
            case AttributeType.FLOAT:
                data.Write(valFloat);
                break;
            case AttributeType.BOOL:
                data.Write(valBool);
                break;
            case AttributeType.ASCII:
                data.Write(valAscii);
                break;
            case AttributeType.STRING:
                data.Write(valStringId);
                break;
            case AttributeType.DATAID:
                data.Write(valDid);
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }
}
