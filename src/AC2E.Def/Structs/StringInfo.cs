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
                    throw new NotImplementedException();
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
                    throw new NotImplementedException();
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

        public StringInfo(BinaryReader data) {
            stringId = data.ReadUInt32();
            tableDid = data.ReadDataId();
            variables = data.ReadDictionary(data.ReadUInt32, () => new StringInfoData(data));
            // TODO: Need to ensure this is the actual logic for detecting a literal/how to parse
            if (tableDid.id == 0) {
                literalValue = data.ReadEncryptedString(Encoding.Unicode);
            }
        }

        public void write(BinaryWriter data) {
            data.Write(stringId);
            data.Write(tableDid);
            data.Write(variables, data.Write, v => v.write(data));
            // TODO: Need to ensure this is the actual logic for detecting a literal/how to write
            if (tableDid.id == 0) {
                data.WriteEncryptedString(literalValue);
            }
        }
    }
}
