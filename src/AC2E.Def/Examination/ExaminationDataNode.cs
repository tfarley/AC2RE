using System.IO;

namespace AC2E.Def {

    public class ExaminationDataNode {

        public ExaminationDataType _type;
        public uint _order;
        public int valInt;
        public float valFloat;
        public bool valBool;
        public DataId valDataID;
        public long valLongInt;
        public uint _appearanceID;
        public double _value_valTime;
        public StringInfo _value_valStr;
        public IconDesc _value_valIconDesc;

        public ExaminationDataNode() {

        }

        public ExaminationDataNode(AC2Reader data) {
            _type = (ExaminationDataType)data.ReadUInt32();
            _order = data.ReadUInt32();
            switch (_type) {
                case ExaminationDataType.UNDEF:
                    break;
                case ExaminationDataType.INT:
                case ExaminationDataType.EFFECT_ID:
                    _appearanceID = data.ReadUInt32();
                    valInt = data.ReadInt32();
                    break;
                case ExaminationDataType.FLOAT:
                    _appearanceID = data.ReadUInt32();
                    valFloat = data.ReadSingle();
                    break;
                case ExaminationDataType.BOOL:
                    _appearanceID = data.ReadUInt32();
                    valBool = data.ReadBoolean();
                    break;
                case ExaminationDataType.STRING:
                    _appearanceID = data.ReadUInt32();
                    _value_valStr = new StringInfo(data);
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
                    _appearanceID = data.ReadUInt32();
                    _value_valTime = data.ReadDouble();
                    break;
                case ExaminationDataType.ICON_DESC:
                    _value_valIconDesc = new IconDesc(data);
                    break;
                case ExaminationDataType.LONG_INT:
                    _appearanceID = data.ReadUInt32();
                    valLongInt = data.ReadInt64();
                    break;
                default:
                    throw new InvalidDataException(_type.ToString());
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)_type);
            data.Write(_order);
            switch (_type) {
                case ExaminationDataType.UNDEF:
                    break;
                case ExaminationDataType.INT:
                case ExaminationDataType.EFFECT_ID:
                    data.Write(_appearanceID);
                    data.Write(valInt);
                    break;
                case ExaminationDataType.FLOAT:
                    data.Write(_appearanceID);
                    data.Write(valFloat);
                    break;
                case ExaminationDataType.BOOL:
                    data.Write(_appearanceID);
                    data.Write(valBool);
                    break;
                case ExaminationDataType.STRING:
                    data.Write(_appearanceID);
                    _value_valStr.write(data);
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
                    data.Write(_appearanceID);
                    data.Write(_value_valTime);
                    break;
                case ExaminationDataType.ICON_DESC:
                    _value_valIconDesc.write(data);
                    break;
                case ExaminationDataType.LONG_INT:
                    data.Write(_appearanceID);
                    data.Write(valLongInt);
                    break;
                default:
                    throw new InvalidDataException(_type.ToString());
            }
        }
    }
}
