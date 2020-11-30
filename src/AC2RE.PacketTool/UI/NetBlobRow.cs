using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.Collections.Generic;

namespace AC2RE.PacketTool.UI {

    internal class NetBlobRow {

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
        public Dictionary<string, object>? matchResultValues { get; private set; }

        public NetBlobRecord netBlobRecord;
        public MessageOpcode opcode;

        public NetBlobRow(int lineNum, NetBlobRecord netBlobRecord) {
            this.lineNum = lineNum;
            this.netBlobRecord = netBlobRecord;
            sr = netBlobRecord.isClientToServer ? "S" : "R";
            seq = (int)(netBlobRecord.netBlob.blobId.sequenceId & 0xFFFFFFFF);
            packetNum = netBlobRecord.startPacketNum;
            time = $"{netBlobRecord.startTimestamp:0.00}";
            opcode = netBlobRecord.netBlob.payload != null ? (MessageOpcode)BitConverter.ToUInt32(netBlobRecord.netBlob.payload) : MessageOpcode.UNDEF_EVENT;
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

        public bool matches(string? opcodeFilter, string? eventFilter, object? errorsFilter, string? stringFilter, byte[]? bytePatternFilter, bool showIncomplete, CustomFilter? customFilter) {
            if (opcodeFilter != null && !"".Equals(opcodeFilter) && !opcodeName.Contains(opcodeFilter, StringComparison.OrdinalIgnoreCase)) {
                return false;
            }

            if (eventFilter != null && !"".Equals(eventFilter) && !eventName.Contains(eventFilter, StringComparison.OrdinalIgnoreCase)) {
                return false;
            }

            if (!showIncomplete && netBlobRecord.netBlob.payload == null) {
                return false;
            }

            if (errorsFilter != null && !"".Equals(errorsFilter)) {
                if ("All".Equals(errorsFilter)) {
                    if (netBlobRecord.messageException == null) {
                        return false;
                    }
                } else if (!netBlobRecord.messageErrorType.Equals(errorsFilter)) {
                    return false;
                }
            }

            if (stringFilter != null && !"".Equals(stringFilter) && !Util.objectToString(netBlobRecord.message).Contains(stringFilter, StringComparison.OrdinalIgnoreCase)) {
                return false;
            }

            if (bytePatternFilter != null && bytePatternFilter.Length > 0 && Util.indexOfPattern(netBlobRecord.netBlob.payload, 0, bytePatternFilter) == -1) {
                return false;
            }

            if (customFilter != null) {
                Dictionary<string, object> matchResultValues = new();
                if (!customFilter.matches(this, matchResultValues)) {
                    return false;
                }
                if (matchResultValues.Count > 0) {
                    this.matchResultValues = matchResultValues;
                }
            }

            return true;
        }
    }
}
