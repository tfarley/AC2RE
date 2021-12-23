using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.DatTool;

internal class CellParse {

    public static void getMissingCells(DatReader datReader) {
        HashSet<ushort> seenLandblocks = new();

        foreach (DataId did in datReader.dids) {
            seenLandblocks.Add((ushort)((did.id >> 16) & 0xFFFF));
        }

        Logs.GENERAL.info("Parsed dat",
            "fileName", datReader.datFileName,
            "numLandblocks", seenLandblocks.Count);
    }
}
