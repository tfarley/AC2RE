using AC2E.Def;
using Serilog;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Server {

    internal class CellParse {

        public static void getMissingCells(string datFileName) {
            if (File.Exists(datFileName)) {

                HashSet<ushort> seenLandblocks = new HashSet<ushort>();

                int numFiles = 0;
                using (DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(datFileName)))) {
                    BTree filesystemTree = new BTree(datReader);

                    foreach (BTNode node in filesystemTree.offsetToNode.Values) {
                        numFiles += node.entries.Count;

                        foreach (BTEntry entry in node.entries) {
                            seenLandblocks.Add((ushort)((entry.did.id >> 16) & 0xFFFF));
                        }
                    }
                }

                Log.Information($"Parsed dat {datFileName}, num files: {numFiles}, num landblocks: {seenLandblocks.Count}.");
            }
        }
    }
}
