using System.IO;

namespace AC2RE.Definitions;

public class ExaminationDataNode {

    // Const *_ExaminationDataType
    public enum DataType : uint {
        Undef = 0, // Undef_ExaminationDataType

        Int = 0x40000001, // Int_ExaminationDataType
        Float = 0x40000002, // Float_ExaminationDataType
        Bool = 0x40000003, // Bool_ExaminationDataType
        String = 0x40000004, // String_ExaminationDataType
        Image = 0x40000005, // Image_ExaminationDataType
        Break = 0x40000006, // Break_ExaminationDataType
        Tab = 0x40000007, // Tab_ExaminationDataType
        Countdown = 0x40000008, // Countdown_ExaminationDataType
        IconDesc = 0x40000009, // IconDesc_ExaminationDataType
        LongInt = 0x4000000A, // LongInt_ExaminationDataType

        EffectID = 0x4000000C, // EffectID_ExaminationDataType
    }

    // ExaminationDataNode
    public DataType type; // _type
    public uint order; // _order
    public int valInt; // valInt
    public float valFloat; // valFloat
    public bool valBool; // valBool
    public DataId valDataID; // valDataID
    public long valLongInt; // valLongInt
    public uint appearanceId; // _appearanceID
    public double valTime; // _value_valTime
    public StringInfo valString; // _value_valStr
    public IconDesc valIconDesc; // _value_valIconDesc

    public ExaminationDataNode() {

    }

    public ExaminationDataNode(AC2Reader data) {
        type = data.ReadEnum<DataType>();
        order = data.ReadUInt32();
        switch (type) {
            case DataType.Undef:
                break;
            case DataType.Int:
            case DataType.EffectID:
                appearanceId = data.ReadUInt32();
                valInt = data.ReadInt32();
                break;
            case DataType.Float:
                appearanceId = data.ReadUInt32();
                valFloat = data.ReadSingle();
                break;
            case DataType.Bool:
                appearanceId = data.ReadUInt32();
                valBool = data.ReadBoolean();
                break;
            case DataType.String:
                appearanceId = data.ReadUInt32();
                valString = new(data);
                break;
            case DataType.Image:
                valDataID = data.ReadDataId();
                break;
            case DataType.Break:
                break;
            case DataType.Tab:
                // TODO: Not sure if this is correct type
                valInt = data.ReadInt32();
                break;
            case DataType.Countdown:
                appearanceId = data.ReadUInt32();
                valTime = data.ReadDouble();
                break;
            case DataType.IconDesc:
                valIconDesc = new(data);
                break;
            case DataType.LongInt:
                appearanceId = data.ReadUInt32();
                valLongInt = data.ReadInt64();
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        data.Write(order);
        switch (type) {
            case DataType.Undef:
                break;
            case DataType.Int:
            case DataType.EffectID:
                data.Write(appearanceId);
                data.Write(valInt);
                break;
            case DataType.Float:
                data.Write(appearanceId);
                data.Write(valFloat);
                break;
            case DataType.Bool:
                data.Write(appearanceId);
                data.Write(valBool);
                break;
            case DataType.String:
                data.Write(appearanceId);
                valString.write(data);
                break;
            case DataType.Image:
                data.Write(valDataID);
                break;
            case DataType.Break:
                break;
            case DataType.Tab:
                // TODO: Not sure if this is correct type
                data.Write(valInt);
                break;
            case DataType.Countdown:
                data.Write(appearanceId);
                data.Write(valTime);
                break;
            case DataType.IconDesc:
                valIconDesc.write(data);
                break;
            case DataType.LongInt:
                data.Write(appearanceId);
                data.Write(valLongInt);
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }
}
