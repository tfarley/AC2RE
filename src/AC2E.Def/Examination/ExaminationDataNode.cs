using System.IO;

namespace AC2E.Def {

    public class ExaminationDataNode {

        public ExaminationDataType type; // _type
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
            type = (ExaminationDataType)data.ReadUInt32();
            order = data.ReadUInt32();
            switch (type) {
                case ExaminationDataType.UNDEF:
                    break;
                case ExaminationDataType.INT:
                case ExaminationDataType.EFFECT_ID:
                    appearanceId = data.ReadUInt32();
                    valInt = data.ReadInt32();
                    break;
                case ExaminationDataType.FLOAT:
                    appearanceId = data.ReadUInt32();
                    valFloat = data.ReadSingle();
                    break;
                case ExaminationDataType.BOOL:
                    appearanceId = data.ReadUInt32();
                    valBool = data.ReadBoolean();
                    break;
                case ExaminationDataType.STRING:
                    appearanceId = data.ReadUInt32();
                    valString = new StringInfo(data);
                    break;
                case ExaminationDataType.IMAGE:
                    valDataID = data.ReadDataId();
                    break;
                case ExaminationDataType.BREAK:
                    break;
                case ExaminationDataType.TAB:
                    // TODO: Not sure if this is correct type
                    valInt = data.ReadInt32();
                    break;
                case ExaminationDataType.COUNTDOWN:
                    appearanceId = data.ReadUInt32();
                    valTime = data.ReadDouble();
                    break;
                case ExaminationDataType.ICON_DESC:
                    valIconDesc = new IconDesc(data);
                    break;
                case ExaminationDataType.LONG_INT:
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
                case ExaminationDataType.UNDEF:
                    break;
                case ExaminationDataType.INT:
                case ExaminationDataType.EFFECT_ID:
                    data.Write(appearanceId);
                    data.Write(valInt);
                    break;
                case ExaminationDataType.FLOAT:
                    data.Write(appearanceId);
                    data.Write(valFloat);
                    break;
                case ExaminationDataType.BOOL:
                    data.Write(appearanceId);
                    data.Write(valBool);
                    break;
                case ExaminationDataType.STRING:
                    data.Write(appearanceId);
                    valString.write(data);
                    break;
                case ExaminationDataType.IMAGE:
                    data.Write(valDataID);
                    break;
                case ExaminationDataType.BREAK:
                    break;
                case ExaminationDataType.TAB:
                    // TODO: Not sure if this is correct type
                    data.Write(valInt);
                    break;
                case ExaminationDataType.COUNTDOWN:
                    data.Write(appearanceId);
                    data.Write(valTime);
                    break;
                case ExaminationDataType.ICON_DESC:
                    valIconDesc.write(data);
                    break;
                case ExaminationDataType.LONG_INT:
                    data.Write(appearanceId);
                    data.Write(valLongInt);
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }
    }
}
