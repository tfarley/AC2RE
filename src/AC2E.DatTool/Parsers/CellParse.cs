﻿using AC2E.Def;
using Serilog;
using System.Collections.Generic;

namespace AC2E.DatTool {

    public class CellParse {

        public static void getMissingCells(DatReader datReader) {
            HashSet<ushort> seenLandblocks = new();

            foreach (DataId did in datReader.dids) {
                seenLandblocks.Add((ushort)((did.id >> 16) & 0xFFFF));
            }

            Log.Information($"Parsed dat {datReader.datFileName}, num landblocks: {seenLandblocks.Count}.");
        }
    }
}
