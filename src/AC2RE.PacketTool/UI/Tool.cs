using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2RE.PacketTool.UI {

    internal abstract class Tool {

        private static readonly Predicate<CreateObjectMsg> ALL_FILTER = msg => msg.physicsDesc.pos != null;
        private static readonly Predicate<CreateObjectMsg> MAP_FILTER = msg => ALL_FILTER.Invoke(msg) && (byte)(msg.id.id >> 56) == 0x40;

        public static readonly Dictionary<string, Func<string, Tool>> TOOLS = new() {
            { "GenerateMapDb", filePath => new GenerateDb(filePath, "map_obj.csv", MAP_FILTER) },
            { "DumpMapObjects", filePath => new DumpObjects(filePath, "map_dump.txt", MAP_FILTER) },
            { "GenerateDb", filePath => new GenerateDb(filePath, "obj.csv", ALL_FILTER) },
            { "DumpObjects", filePath => new DumpObjects(filePath, "obj_dump.txt", ALL_FILTER) },
        };

        private class GenerateDb : Tool {

            public GenerateDb(string filePath, string outputFileName, Predicate<CreateObjectMsg> filter) {

                HashSet<InstanceId> processedIds = new();

                using (StreamWriter csvWriter = new(File.OpenWrite(outputFileName))) {
                    csvWriter.WriteLine("packageType,id,entityDid,landblockId,cellId,posX,posY,posZ,rotX,rotY,rotZ,rotW,scale,nameStringId,nameTableDid");
                    PacketUtil.processAllPcaps(filePath, (pcapFileName, netBlobCollection) => {
                        foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                            INetMessage? genericMsg = netBlobRecord.message;
                            if (genericMsg is CreateObjectMsg msg && filter.Invoke(msg) && processedIds.Add(msg.id)) {
                                Position pos = msg.physicsDesc.pos ?? new();
                                csvWriter.WriteLine($"{msg.weenieDesc.packageType},{msg.id.id},{msg.weenieDesc.entityDid.id},{pos.cell.landblockId.id},{pos.cell.localCellId.id},{pos.frame.pos.X},{pos.frame.pos.Y},{pos.frame.pos.Z},{pos.frame.rot.X},{pos.frame.rot.Y},{pos.frame.rot.Z},{pos.frame.rot.W},{msg.weenieDesc.scale},{msg.weenieDesc.name?.stringId.ToString() ?? "NULL"},{msg.weenieDesc.name?.tableDid.id.ToString() ?? "NULL"}");
                            }
                        }
                    });
                }
            }
        }

        private class DumpObjects : Tool {

            public DumpObjects(string filePath, string outputFileName, Predicate<CreateObjectMsg> filter) {

                HashSet<InstanceId> processedIds = new();

                using (StreamWriter dumpWriter = new(File.OpenWrite(outputFileName))) {
                    PacketUtil.processAllPcaps(filePath, (pcapFileName, netBlobCollection) => {
                        foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                            INetMessage? genericMsg = netBlobRecord.message;
                            if (genericMsg is CreateObjectMsg msg && filter.Invoke(msg) && processedIds.Add(msg.id)) {
                                dumpWriter.WriteLine(Util.objectToString(msg));
                            }
                        }
                    });
                }
            }
        }
    }
}
