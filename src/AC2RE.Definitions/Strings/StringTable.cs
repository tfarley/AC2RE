using System.Collections.Generic;

namespace AC2RE.Definitions;

public class StringTable {

    // StringTable
    public DataId did; // m_DID
    public uint currentStateId; // currentStateID
    public ushort baseVal; // m_base
    public ushort numDecimalDigits; // m_numDecimalDigits
    public bool leadingZero; // m_leadingZero
    public Dictionary<uint, StringTableString> strings; // m_strings

    public StringTable() {

    }

    public StringTable(AC2Reader data) {
        did = data.ReadDataId();
        currentStateId = data.ReadUInt32();
        baseVal = data.ReadUInt16();
        leadingZero = data.ReadBoolean();
        numDecimalDigits = data.ReadUInt16();
        strings = data.ReadDictionary(data.ReadUInt32, () => new StringTableString(data));
    }
}
