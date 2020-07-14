using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        public float dataFloat;
        public uint dataUInt;
        public long dataLong;
        public ulong dataULong;
        public StringInfo stringInfo;

        public StringInfoData(BinaryReader data) {
            // TODO: Not sure if this should be full 32 read or 16 + align
            type = (StringInfoDataType)data.ReadUInt16();
            data.Align(4);
            switch (type) {
                case StringInfoDataType.INT:
                    dataInt = data.ReadInt32();
                    break;
                case StringInfoDataType.FLOAT:
                    dataFloat = data.ReadSingle();
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

        public void write(BinaryWriter data) {
            // TODO: Not sure if this should be full 32 write or 16 + align
            data.Write((ushort)type);
            data.Align(4);
            switch (type) {
                case StringInfoDataType.INT:
                    data.Write(dataInt);
                    break;
                case StringInfoDataType.FLOAT:
                    data.Write(dataFloat);
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

    public class StringInfo : IPackage {

        public NativeType nativeType => NativeType.STRINGINFO;

        public uint stringId; // m_stringID
        public DataId tableDid; // m_tableID
        public Dictionary<uint, StringInfoData> variables; // m_variables
        public string literalValue; // m_LiteralValue

        public StringInfo() {

        }

        public StringInfo(string literalValue) {
            this.literalValue = literalValue;
        }

        public StringInfo(BinaryReader data) {
            stringId = data.ReadUInt32();
            tableDid = data.ReadDataId();
            variables = new Dictionary<uint, StringInfoData>();
            ushort numVariables = data.ReadUInt16();
            if (data.ReadUInt16() != 0) {
                literalValue = data.ReadEncryptedString(Encoding.Unicode);
            }
            for (int i = 0; i < numVariables; i++) {
                variables.Add(data.ReadUInt32(), new StringInfoData(data));
            }
        }

        public void write(BinaryWriter data) {
            data.Write(stringId);
            data.Write(tableDid);
            int numVariables = variables != null ? variables.Count : 0;
            data.Write((ushort)numVariables);
            if (literalValue != null) {
                data.Write((ushort)1);
                data.WriteEncryptedString(literalValue, Encoding.Unicode);
            } else {
                data.Write((ushort)0);
            }
            if (numVariables > 0) {
                foreach (var element in variables) {
                    data.Write(element.Key);
                    element.Value.write(data);
                }
            }
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            write(data);
        }
    }
}
