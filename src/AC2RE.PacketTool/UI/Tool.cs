using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2RE.PacketTool.UI {

    internal abstract class Tool {

        public static readonly Dictionary<string, Func<string, Tool>> TOOLS = new() {
            { "GenerateMapDb", filePath => new GenerateMapDb(filePath) },
            { "DumpMapObjects", filePath => new DumpMapObjects(filePath) },
        };

        private class GenerateMapDb : Tool {

            public GenerateMapDb(string filePath) {

                HashSet<InstanceId> processedIds = new();

                using (StreamWriter csvWriter = new StreamWriter(new FileStream("map_obj.csv", FileMode.Create, FileAccess.Write))) {
                    csvWriter.WriteLine("packageType,id,entityDid,landblockId,cellId,posX,posY,posZ,rotX,rotY,rotZ,rotW,scale,nameStringId,nameTableDid");
                    PacketUtil.processAllPcaps(filePath, (pcapFileName, netBlobCollection) => {
                        foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                            INetMessage? genericMsg = netBlobRecord.message;
                            if (genericMsg is CreateObjectMsg msg && (byte)(msg.id.id >> 56) == 0x40 && processedIds.Add(msg.id)) {
                                Position pos = msg.physicsDesc.pos;
                                csvWriter.WriteLine($"{msg.weenieDesc.packageType},{msg.id.id},{msg.weenieDesc.entityDid.id},{pos.cell.landblockId.id},{pos.cell.localCellId.id},{pos.frame.pos.X},{pos.frame.pos.Y},{pos.frame.pos.Z},{pos.frame.rot.X},{pos.frame.rot.Y},{pos.frame.rot.Z},{pos.frame.rot.W},{msg.weenieDesc.scale},{msg.weenieDesc.name?.stringId.ToString() ?? "NULL"},{msg.weenieDesc.name?.tableDid.id.ToString() ?? "NULL"}");
                            }
                        }
                    });
                }
            }
        }

        private class DumpMapObjects : Tool {

            public DumpMapObjects(string filePath) {

                HashSet<InstanceId> processedIds = new();

                using (StreamWriter dumpWriter = new StreamWriter(new FileStream("map_dump.txt", FileMode.Create, FileAccess.Write))) {
                    PacketUtil.processAllPcaps(filePath, (pcapFileName, netBlobCollection) => {
                        foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                            INetMessage? genericMsg = netBlobRecord.message;
                            if (genericMsg is CreateObjectMsg msg && (byte)(msg.id.id >> 56) == 0x40 && processedIds.Add(msg.id)) {
                                dumpWriter.WriteLine(Util.objectToString(msg));
                            }
                        }
                    });
                }
            }
        }
    }
}
