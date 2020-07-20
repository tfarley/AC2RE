using System.Collections.Generic;
using System.Text;

namespace AC2E.Def {

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

        public StringInfo(AC2Reader data) {
            stringId = data.ReadUInt32();
            tableDid = data.ReadDataId();
            variables = new Dictionary<uint, StringInfoData>();
            ushort numVariables = data.ReadUInt16();
            if (data.ReadUInt16() != 0) {
                literalValue = data.ReadString(Encoding.Unicode);
            }
            for (int i = 0; i < numVariables; i++) {
                variables.Add(data.ReadUInt32(), new StringInfoData(data));
            }
        }

        public void write(AC2Writer data) {
            data.Write(stringId);
            data.Write(tableDid);
            int numVariables = variables != null ? variables.Count : 0;
            data.Write((ushort)numVariables);
            if (literalValue != null) {
                data.Write((ushort)1);
                data.Write(literalValue, Encoding.Unicode);
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
    }
}
