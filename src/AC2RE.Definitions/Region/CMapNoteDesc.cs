using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CMapNoteDesc {

    public class CMapNoteRow {

        // CMapNoteRow
        public uint stringId; // _stringID
        public DataId iconDid; // _iconDID
        public uint x; // _x0
        public uint y; // _y0

        public CMapNoteRow(AC2Reader data) {
            stringId = data.ReadUInt32();
            iconDid = data.ReadDataId();
            x = data.ReadUInt32();
            y = data.ReadUInt32();
        }
    }

    // CMapNoteDesc
    public DataId did; // m_DID
    public DataId stringTableDid; // _stringTableDID
    public List<CMapNoteRow> rows; // _rows

    public CMapNoteDesc(AC2Reader data) {
        did = data.ReadDataId();
        stringTableDid = data.ReadDataId();
        rows = data.ReadList(() => new CMapNoteRow(data));
    }
}
