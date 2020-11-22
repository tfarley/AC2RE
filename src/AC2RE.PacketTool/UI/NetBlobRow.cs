using AC2RE.Definitions;
using System;

namespace AC2RE.PacketTool {

    public class NetBlobRow {

        public int lineNum { get; init; }
        public string sr { get; init; }
        public int seq { get; init; }
        public int packetNum { get; init; }
        public string time { get; init; }
        public string opcodeName { get; init; }
        public string eventName { get; init; }
        public int size { get; init; }
        public string queue { get; init; }
        public string error => netBlobRecord.messageErrorTypeOptional.ToString();
        public byte orderingType { get; init; }
        public char isEphemeral { get; init; }
        public char isCell { get; init; }
        public char isOutOfWorld { get; init; }

        public NetBlobRecord netBlobRecord;

        public NetBlobRow(int lineNum, NetBlobRecord netBlobRecord) {
            this.lineNum = lineNum;
            this.netBlobRecord = netBlobRecord;
            sr = netBlobRecord.isClientToServer ? "S" : "R";
            seq = (int)(netBlobRecord.netBlob.blobId.sequenceId & 0xFFFFFFFF);
            packetNum = netBlobRecord.startPacketNum;
            time = $"{netBlobRecord.startTimestamp:0.00}";
            MessageOpcode opcode = netBlobRecord.netBlob.payload != null ? (MessageOpcode)BitConverter.ToUInt32(netBlobRecord.netBlob.payload) : MessageOpcode.UNDEF_EVENT;
            opcodeName = opcode.ToString();
            if (netBlobRecord.netBlob.payload != null) {
                if (opcode == MessageOpcode.Evt_Interp__InterpSEvent_ID || opcode == MessageOpcode.Evt_Interp__InterpSEventEncrypt_ID) {
                    eventName = ((ServerEventFunctionId)BitConverter.ToUInt32(netBlobRecord.netBlob.payload, 4)).ToString();
                } else if (opcode == MessageOpcode.Evt_Interp__InterpCEvent_Private_ID || opcode == MessageOpcode.Evt_Interp__InterpCEvent_Broadcast_ID) {
                    eventName = ((ClientEventFunctionId)BitConverter.ToUInt32(netBlobRecord.netBlob.payload, 4)).ToString();
                } else if (opcode == MessageOpcode.Evt_Interp__InterpCEvent_Cell_ID || opcode == MessageOpcode.Evt_Interp__InterpCEvent_Visual_ID) {
                    eventName = ((ClientEventFunctionId)BitConverter.ToUInt32(netBlobRecord.netBlob.payload, 16)).ToString();
                } else {
                    eventName = "";
                }
            } else {
                eventName = "";
            }
            size = netBlobRecord.netBlob.payload != null ? netBlobRecord.netBlob.payload.Length : 0;
            queue = netBlobRecord.netBlob.queueId.ToString();
            orderingType = netBlobRecord.netBlob.blobId.orderingType;
            isEphemeral = netBlobRecord.netBlob.blobId.isEphemeral ? 'T' : 'F';
            isCell = netBlobRecord.netBlob.blobId.isCell ? 'T' : 'F';
            isOutOfWorld = netBlobRecord.netBlob.blobId.isOutOfWorld ? 'T' : 'F';
        }
    }
}
