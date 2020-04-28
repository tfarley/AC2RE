using AC2E.Interp.Event;
using AC2E.Interp.Event.ServerEvents;
using AC2E.Protocol.NetBlob;
using Serilog;
using System;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class InterpSEventMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.NET_QUEUE_WEENIE;

        public MessageOpcode opcode => MessageOpcode.Evt_Interp__InterpSEvent_ID;

        public IServerEvent netEvent;

        public InterpSEventMsg(BinaryReader data) {
            ServerEventFunctionId funcId = (ServerEventFunctionId)data.ReadUInt32();
            switch (funcId) {
                case ServerEventFunctionId.Player__UploadUISettings:
                    // TODO: Implement
                    netEvent = new GenericSEvt(ServerEventFunctionId.Player__UploadUISettings, data, 0);
                    break;
                case ServerEventFunctionId.Combat__StartAttack:
                    netEvent = new StartAttackSEvt(data);
                    break;
                default:
                    Log.Error($"Unhandled server event: {funcId} - message not processed!");
                    throw new Exception();
            }
        }
    }
}
