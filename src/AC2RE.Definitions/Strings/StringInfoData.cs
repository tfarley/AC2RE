using System;

namespace AC2RE.Definitions {

    public class StringInfoData {

        // Const SID_Type_*
        public enum DataType : uint {
            Undef = 0,

            Int = 2, // SID_Type_Int
            FormattedInt = 3, // SID_Type_FormattedInt
            Float = 4, // SID_Type_Float
            FormattedFloat = 5, // SID_Type_FormattedFloat
            UInt = 6, // SID_Type_UInt
            FormattedUInt = 7, // SID_Type_FormattedUInt

            StringInfo = 9, // SID_Type_StringInfo
            LInt = 10, // SID_Type_LInt
            FormattedLInt = 11, // SID_Type_FormattedLInt
            ULInt = 12, // SID_Type_ULInt
            FormattedULInt = 13, // SID_Type_FormattedULInt
        }

        public DataType type; // type

        public int valInt;
        public double valDouble;
        public ushort valDoublePrecision;
        public uint valUInt;
        public long valLong;
        public ulong valULong;
        public StringInfo valString;

        public StringInfoData() {

        }

        public StringInfoData(StringInfo value) {
            type = DataType.StringInfo;
            valString = value;
        }

        public StringInfoData(int value, DataType type = DataType.Int) {
            this.type = type;
            valInt = value;
        }

        public StringInfoData(AC2Reader data) {
            // TODO: Not sure if this should be full 32 read or 16 + align
            type = (DataType)data.ReadUInt16();
            data.Align(4);
            switch (type) {
                case DataType.Int:
                    valInt = data.ReadInt32();
                    break;
                case DataType.Float:
                    valDouble = data.ReadDouble();
                    valDoublePrecision = data.ReadUInt16();
                    data.Align(4);
                    break;
                case DataType.UInt:
                    valUInt = data.ReadUInt32();
                    break;
                case DataType.StringInfo:
                    valString = new(data);
                    break;
                case DataType.LInt:
                    valLong = data.ReadInt64();
                    break;
                case DataType.ULInt:
                    valULong = data.ReadUInt64();
                    break;
                default:
                    throw new NotImplementedException($"StringInfoData type {type}.");
            }
        }

        public void write(AC2Writer data) {
            // TODO: Not sure if this should be full 32 write or 16 + align
            data.Write((ushort)type);
            data.Align(4);
            switch (type) {
                case DataType.Int:
                    data.Write(valInt);
                    break;
                case DataType.Float:
                    data.Write(valDouble);
                    data.Write(valDoublePrecision);
                    data.Align(4);
                    break;
                case DataType.UInt:
                    data.Write(valUInt);
                    break;
                case DataType.StringInfo:
                    valString.write(data);
                    break;
                case DataType.LInt:
                    data.Write(valLong);
                    break;
                case DataType.ULInt:
                    data.Write(valULong);
                    break;
                default:
                    throw new NotImplementedException($"StringInfoData type {type}.");
            }
        }
    }
}
