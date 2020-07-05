using AC2E.Protocol;
using AC2E.WLib;
using System;

namespace AC2E.PacketTool {

    public class NetBlobRow {

        public int lineNum { get; set; }
        public string sr { get; private set; }
        public int seq { get; private set; }
        public int packetNum { get; private set; }
        public string time { get; private set; }
        public string opcodeName { get; private set; }
        public string eventName { get; private set; }
        public int size { get; private set; }
        public string queue { get; private set; }
        public string error => netBlobRecord.messageErrorTypeOptional.ToString();
        public byte orderingType { get; private set; }
        public char isEphemeral { get; private set; }
        public char isCell { get; private set; }
        public char isOutOfWorld { get; private set; }

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
            if (opcode == MessageOpcode.Evt_Interp__InterpSEvent_ID || opcode == MessageOpcode.Evt_Interp__InterpSEventEncrypt_ID) {
                eventName = ((ServerEventFunctionId)BitConverter.ToUInt32(netBlobRecord.netBlob.payload, 4)).ToString();
            } else if (opcode == MessageOpcode.Evt_Interp__InterpCEvent_Private_ID || opcode == MessageOpcode.Evt_Interp__InterpCEvent_Cell_ID || opcode == MessageOpcode.Evt_Interp__InterpCEvent_Visual_ID || opcode == MessageOpcode.Evt_Interp__InterpCEvent_Broadcast_ID) {
                eventName = ((ClientEventFunctionId)BitConverter.ToUInt32(netBlobRecord.netBlob.payload, 4)).ToString();
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
