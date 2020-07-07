using AC2E.Dat;
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

        public StringInfo stringInfo;

        public StringInfoData(BinaryReader data) {
            // TODO: Not sure if this should be full 32 read or 16 + align
            type = (StringInfoDataType)data.ReadUInt16();
            data.Align(4);
            switch (type) {
                case StringInfoDataType.STRING_INFO:
                    stringInfo = new StringInfo(data);
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
                case StringInfoDataType.STRING_INFO:
                    stringInfo.write(data);
                    break;
                default:
                    throw new NotImplementedException($"StringInfoData type {type}.");
            }
        }
    }

    public class StringInfo {

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
            // TODO: Guessed on the reading/writing of this - see if isLiteral might actually be between numVars and variable dictionary itself? Note that StringInfo has ::Pack instead of ::StreamPack like most structs
            stringId = data.ReadUInt32();
            tableDid = data.ReadDataId();
            variables = new Dictionary<uint, StringInfoData>();
            ushort numVariables = data.ReadUInt16();
            bool isLiteral = data.ReadUInt16() != 0;
            for (int i = 0; i < numVariables; i++) {
                variables.Add(data.ReadUInt32(), new StringInfoData(data));
            }
            if (isLiteral) {
                literalValue = data.ReadEncryptedString(Encoding.Unicode);
            }
        }

        public void write(BinaryWriter data) {
            data.Write(stringId);
            data.Write(tableDid);
            int numVariables = variables != null ? variables.Count : 0;
            data.Write((ushort)numVariables);
            data.Write(literalValue != null ? (ushort)1 : (ushort)0);
            if (numVariables > 0) {
                foreach (var element in variables) {
                    data.Write(element.Key);
                    element.Value.write(data);
                }
            }
            if (literalValue != null) {
                data.WriteEncryptedString(literalValue, Encoding.Unicode);
            }
        }
    }
}
