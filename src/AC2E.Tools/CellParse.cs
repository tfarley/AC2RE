using AC2E.Def;
using Serilog;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Tools {

    public class CellParse {

        public static void getMissingCells(string datFileName) {
            if (File.Exists(datFileName)) {

                HashSet<ushort> seenLandblocks = new HashSet<ushort>();

                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                    foreach (DataId did in datReader.dids) {
                        seenLandblocks.Add((ushort)((did.id >> 16) & 0xFFFF));
                    }
                }

                Log.Information($"Parsed dat {datFileName}, num landblocks: {seenLandblocks.Count}.");
            }
        }
    }
}
