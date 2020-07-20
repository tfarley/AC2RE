using System;

namespace AC2E.Def {

    public class StringInfoData {

        // Const SID_Type_*
        public enum StringInfoDataType {
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

        public StringInfoDataType type; // type

        public int dataInt;
        public double dataDouble;
        public ushort dataDoublePrecision;
        public uint dataUInt;
        public long dataLong;
        public ulong dataULong;
        public StringInfo stringInfo;

        public StringInfoData(AC2Reader data) {
            // TODO: Not sure if this should be full 32 read or 16 + align
            type = (StringInfoDataType)data.ReadUInt16();
            data.Align(4);
            switch (type) {
                case StringInfoDataType.INT:
                    dataInt = data.ReadInt32();
                    break;
                case StringInfoDataType.FLOAT:
                    dataDouble = data.ReadDouble();
                    dataDoublePrecision = data.ReadUInt16();
                    data.Align(4);
                    break;
                case StringInfoDataType.UINT:
                    dataUInt = data.ReadUInt32();
                    break;
                case StringInfoDataType.STRING_INFO:
                    stringInfo = new StringInfo(data);
                    break;
                case StringInfoDataType.LINT:
                    dataLong = data.ReadInt64();
                    break;
                case StringInfoDataType.ULINT:
                    dataULong = data.ReadUInt64();
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
                case StringInfoDataType.INT:
                    data.Write(dataInt);
                    break;
                case StringInfoDataType.FLOAT:
                    data.Write(dataDouble);
                    data.Write(dataDoublePrecision);
                    data.Align(4);
                    break;
                case StringInfoDataType.UINT:
                    data.Write(dataUInt);
                    break;
                case StringInfoDataType.STRING_INFO:
                    stringInfo.write(data);
                    break;
                case StringInfoDataType.LINT:
                    data.Write(dataLong);
                    break;
                case StringInfoDataType.ULINT:
                    data.Write(dataULong);
                    break;
                default:
                    throw new NotImplementedException($"StringInfoData type {type}.");
            }
        }
    }
}
