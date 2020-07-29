using System.IO;

namespace AC2E.Def {

    public class ExaminationDataNode {

        // Const *_ExaminationDataType
        public enum DataType : uint {
            UNDEF = 0,
            INT = 0x40000001,
            FLOAT = 0x40000002,
            BOOL = 0x40000003,
            STRING = 0x40000004,
            IMAGE = 0x40000005,
            BREAK = 0x40000006,
            TAB = 0x40000007,
            COUNTDOWN = 0x40000008,
            ICON_DESC = 0x40000009,
            LONG_INT = 0x4000000A,

            EFFECT_ID = 0x4000000C,
        }

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
            type = (DataType)data.ReadUInt32();
            order = data.ReadUInt32();
            switch (type) {
                case DataType.UNDEF:
                    break;
                case DataType.INT:
                case DataType.EFFECT_ID:
                    appearanceId = data.ReadUInt32();
                    valInt = data.ReadInt32();
                    break;
                case DataType.FLOAT:
                    appearanceId = data.ReadUInt32();
                    valFloat = data.ReadSingle();
                    break;
                case DataType.BOOL:
                    appearanceId = data.ReadUInt32();
                    valBool = data.ReadBoolean();
                    break;
                case DataType.STRING:
                    appearanceId = data.ReadUInt32();
                    valString = new StringInfo(data);
                    break;
                case DataType.IMAGE:
                    valDataID = data.ReadDataId();
                    break;
                case DataType.BREAK:
                    break;
                case DataType.TAB:
                    // TODO: Not sure if this is correct type
                    valInt = data.ReadInt32();
                    break;
                case DataType.COUNTDOWN:
                    appearanceId = data.ReadUInt32();
                    valTime = data.ReadDouble();
                    break;
                case DataType.ICON_DESC:
                    valIconDesc = new IconDesc(data);
                    break;
                case DataType.LONG_INT:
                    appearanceId = data.ReadUInt32();
                    valLongInt = data.ReadInt64();
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(order);
            switch (type) {
                case DataType.UNDEF:
                    break;
                case DataType.INT:
                case DataType.EFFECT_ID:
                    data.Write(appearanceId);
                    data.Write(valInt);
                    break;
                case DataType.FLOAT:
                    data.Write(appearanceId);
                    data.Write(valFloat);
                    break;
                case DataType.BOOL:
                    data.Write(appearanceId);
                    data.Write(valBool);
                    break;
                case DataType.STRING:
                    data.Write(appearanceId);
                    valString.write(data);
                    break;
                case DataType.IMAGE:
                    data.Write(valDataID);
                    break;
                case DataType.BREAK:
                    break;
                case DataType.TAB:
                    // TODO: Not sure if this is correct type
                    data.Write(valInt);
                    break;
                case DataType.COUNTDOWN:
                    data.Write(appearanceId);
                    data.Write(valTime);
                    break;
                case DataType.ICON_DESC:
                    valIconDesc.write(data);
                    break;
                case DataType.LONG_INT:
                    data.Write(appearanceId);
                    data.Write(valLongInt);
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }
    }
}
