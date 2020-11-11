using System;

namespace AC2E.Def {

    public class StringInfoData {

        // Const SID_Type_*
        public enum DataType : uint {
            INT = 2,
            FORMATTED_INT = 3,
            FLOAT = 4,
            FORMATTED_FLOAT = 5,
            UINT = 6,
            FORMATTED_UINT = 7,

            STRING_INFO = 9,
            LINT = 10,
            FORMATTED_LINT = 11,
            ULINT = 12,
            FORMATTED_ULINT = 13,
        }

        public DataType type; // type

        public int valInt;
        public double valDouble;
        public ushort valDoublePrecision;
        public uint valUInt;
        public long valLong;
        public ulong valULong;
        public StringInfo valString;

        public StringInfoData(AC2Reader data) {
            // TODO: Not sure if this should be full 32 read or 16 + align
            type = (DataType)data.ReadUInt16();
            data.Align(4);
            switch (type) {
                case DataType.INT:
                    valInt = data.ReadInt32();
                    break;
                case DataType.FLOAT:
                    valDouble = data.ReadDouble();
                    valDoublePrecision = data.ReadUInt16();
                    data.Align(4);
                    break;
                case DataType.UINT:
                    valUInt = data.ReadUInt32();
                    break;
                case DataType.STRING_INFO:
                    valString = new(data);
                    break;
                case DataType.LINT:
                    valLong = data.ReadInt64();
                    break;
                case DataType.ULINT:
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
                case DataType.INT:
                    data.Write(valInt);
                    break;
                case DataType.FLOAT:
                    data.Write(valDouble);
                    data.Write(valDoublePrecision);
                    data.Align(4);
                    break;
                case DataType.UINT:
                    data.Write(valUInt);
                    break;
                case DataType.STRING_INFO:
                    valString.write(data);
                    break;
                case DataType.LINT:
                    data.Write(valLong);
                    break;
                case DataType.ULINT:
                    data.Write(valULong);
                    break;
                default:
                    throw new NotImplementedException($"StringInfoData type {type}.");
            }
        }
    }
}
