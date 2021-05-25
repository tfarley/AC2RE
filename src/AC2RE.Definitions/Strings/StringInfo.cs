using System.Collections.Generic;
using System.Text;

namespace AC2RE.Definitions {

    public class StringInfo : IPackage {

        public NativeType nativeType => NativeType.STRINGINFO;

        public uint stringId; // m_stringID
        public DataId tableDid; // m_tableID
        public Dictionary<StringVariable, StringInfoData> variables; // m_variables
        public string literalValue; // m_LiteralValue

        public StringInfo() {

        }

        public StringInfo(string literalValue) {
            this.literalValue = literalValue;
        }

        public StringInfo(DataId tableDid, uint stringId) {
            this.tableDid = tableDid;
            this.stringId = stringId;
        }

        public StringInfo(DataId tableDid, uint stringId, Dictionary<StringVariable, StringInfoData> variables) {
            this.tableDid = tableDid;
            this.stringId = stringId;
            this.variables = variables;
        }

        public StringInfo(AC2Reader data) {
            stringId = data.ReadUInt32();
            tableDid = data.ReadDataId();
            variables = new();
            ushort numVariables = data.ReadUInt16();
            if (data.ReadUInt16() != 0) {
                literalValue = data.ReadString(Encoding.Unicode);
            }
            for (int i = 0; i < numVariables; i++) {
                variables.Add((StringVariable)data.ReadUInt32(), new(data));
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
                foreach ((StringVariable variable, StringInfoData variableData) in variables) {
                    data.Write((uint)variable);
                    variableData.write(data);
                }
            }
        }
    }
}
