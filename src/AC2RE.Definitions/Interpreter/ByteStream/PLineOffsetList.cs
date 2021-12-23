using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PLineOffsetList {

    public class PLineOffsetInfo {

        public uint offset; // Offset
        public uint lineNum; // LineNum

        public PLineOffsetInfo(AC2Reader data) {
            offset = data.ReadUInt32();
            lineNum = data.ReadUInt32();
        }
    }

    public string sourceFileName; // SourceFilename
    public List<PLineOffsetInfo> lineOffsets;

    public PLineOffsetList(AC2Reader data) {
        sourceFileName = data.ReadString();
        lineOffsets = data.ReadList(() => new PLineOffsetInfo(data));
    }
}
