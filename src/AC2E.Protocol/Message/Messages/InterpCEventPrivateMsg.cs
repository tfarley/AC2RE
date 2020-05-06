using AC2E.Interp.Event;
using AC2E.Interp.Event.ClientEvents;
using AC2E.Protocol.NetBlob;
using System;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class InterpCEventPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Interp__InterpCEvent_Private_ID;

        public IClientEvent netEvent;

        public InterpCEventPrivateMsg() {

        }

        public InterpCEventPrivateMsg(BinaryReader data) {
            ClientEventFunctionId funcId = (ClientEventFunctionId)data.ReadUInt32();
            uint length = data.ReadUInt32();
            switch (funcId) {
                case ClientEventFunctionId.Effect__ClientRemoveEffect:
                    netEvent = new ClientRemoveEffectCEvt(data);
                    break;
                case ClientEventFunctionId.Player__EnterPortalSpace:
                    netEvent = new EnterPortalSpaceCEvt(data);
                    break;
                case ClientEventFunctionId.Player__ExitPortalSpace:
                    netEvent = new ExitPortalSpaceCEvt(data);
                    break;
                default:
                    throw new NotImplementedException($"Unhandled client event: {funcId}.");
            }
        }

        public void write(BinaryWriter data) {
            data.Write((uint)netEvent.funcId);
            // Placeholder for length
            data.Write((uint)0);
            long contentStart = data.BaseStream.Position;
            netEvent.write(data);
            long contentEnd = data.BaseStream.Position;
            long contentLength = contentEnd - contentStart;
            data.BaseStream.Seek(-contentLength - 4, SeekOrigin.Current);
            data.Write((uint)contentLength);
            data.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
        }
    }
}
