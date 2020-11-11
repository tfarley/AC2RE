using System.Collections.Generic;
using System.Text;

namespace AC2E.Def {

    public class StringTableString {

        public uint table; // table
        public List<string> strings; // strings
        public List<uint> variables; // variables
        public List<string> varNames; // varNames

        public StringTableString() {

        }

        public StringTableString(AC2Reader data) {
            table = data.ReadUInt32();
            ushort hasVarNamesAndNumStrings = data.ReadUInt16();
            bool hasVarNames = (hasVarNamesAndNumStrings & 0x8000) != 0;
            ushort numStrings = (ushort)(hasVarNamesAndNumStrings & ~0x8000);
            strings = new();
            for (int i = 0; i < numStrings; i++) {
                bool stringNotEmpty = data.ReadUInt16() != 0;
                data.Align(4);
                if (stringNotEmpty) {
                    strings.Add(data.ReadString(Encoding.Unicode));
                } else {
                    strings.Add("");
                }
            }
            data.Align(4);
            variables = data.ReadList(data.ReadUInt32, 2);
            data.Align(4);
            if (hasVarNames) {
                varNames = data.ReadList(() => data.ReadString(Encoding.Unicode));
            }
        }
    }
}
