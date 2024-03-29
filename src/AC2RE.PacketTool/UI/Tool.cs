﻿using AC2RE.Definitions;
using AC2RE.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2RE.PacketTool.UI;

internal abstract class Tool {

    private static readonly Predicate<CreateObjectMsg> ALL_FILTER = msg => msg.physicsDesc.pos != null;
    private static readonly Predicate<CreateObjectMsg> MAP_FILTER = msg => ALL_FILTER.Invoke(msg) && msg.id.type == InstanceId.IdType.Static;

    public static readonly Dictionary<string, Func<string, Tool>> TOOLS = new() {
        { "GenerateMapDb", filePath => new GenerateDb(filePath, "map_obj.csv", MAP_FILTER) },
        { "DumpMapObjects", filePath => new DumpObjects(filePath, "map_dump.txt", MAP_FILTER) },
        { "GenerateDb", filePath => new GenerateDb(filePath, "obj.csv", ALL_FILTER) },
        { "DumpObjects", filePath => new DumpObjects(filePath, "obj_dump.txt", ALL_FILTER) },
        { "CorrelatePacketMetadata", filePath => new CorrelatePacketMetadata(filePath) },
    };

    private class GenerateDb : Tool {

        public GenerateDb(string filePath, string outputFileName, Predicate<CreateObjectMsg> filter) {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.FileName = outputFileName;
            saveFileDialog.Filter = "Csv Files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true) {
                HashSet<InstanceId> processedIds = new();

                using (StreamWriter csvWriter = new(File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.Read))) {
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
    }

    private class DumpObjects : Tool {

        public DumpObjects(string filePath, string outputFileName, Predicate<CreateObjectMsg> filter) {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.FileName = outputFileName;
            saveFileDialog.Filter = "Txt Files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true) {
                HashSet<InstanceId> processedIds = new();

                using (StreamWriter dumpWriter = new(File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.Read))) {
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

    private class CorrelatePacketMetadata : Tool {

        private class Metadata {

            public NetBlobId.Flag flags;
            public readonly HashSet<NetQueue> queues = new();
            public readonly HashSet<OrderingType> orderingTypes = new();
        }

        public CorrelatePacketMetadata(string filePath) {

            Dictionary<string, Metadata> opcodeAndEventToMetadata = new();

            PacketUtil.processAllPcaps(filePath, (pcapFileName, netBlobCollection) => {
                foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                    NetBlobRow netBlobRow = new(0, netBlobRecord);
                    string opcodeAndEvent = $"{netBlobRow.opcodeName},{netBlobRow.eventName}";
                    Metadata metadata = opcodeAndEventToMetadata.GetOrCreate(opcodeAndEvent);
                    metadata.flags |= netBlobRecord.netBlob.blobId.flags;
                    metadata.queues.Add(netBlobRecord.netBlob.queueId);
                    metadata.orderingTypes.Add(netBlobRecord.netBlob.blobId.orderingType);
                }
            });

            using (StreamWriter csvWriter = new(File.Open("pkt_meta.txt", FileMode.Create, FileAccess.Write, FileShare.Read))) {
                csvWriter.WriteLine("opcode,event,queues,flags,orderingTypes");
                foreach ((string opcodeAndEvent, Metadata metadata) in opcodeAndEventToMetadata) {
                    StringBuilder sb = new(opcodeAndEvent);
                    sb.Append(',');
                    bool first = true;
                    foreach (NetQueue queue in metadata.queues) {
                        if (!first) {
                            sb.Append('/');
                        }
                        sb.Append(queue);
                        first = false;
                    }
                    sb.Append(',');
                    sb.Append(metadata.flags);
                    sb.Append(',');
                    first = true;
                    foreach (OrderingType orderingType in metadata.orderingTypes) {
                        if (!first) {
                            sb.Append('/');
                        }
                        sb.Append(orderingType);
                        first = false;
                    }
                    csvWriter.WriteLine(sb.ToString());
                }
            }
        }
    }
}
