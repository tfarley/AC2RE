using AC2E.Interp.Event;
using AC2E.PacketTool.Reader;
using AC2E.Protocol.Message;
using System;

namespace AC2E.PacketTool.UI {

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
        }
    }
}
