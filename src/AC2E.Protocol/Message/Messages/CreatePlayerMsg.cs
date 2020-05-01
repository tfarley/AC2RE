﻿using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CreatePlayerMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.NET_QUEUE_EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CreatePlayer_ID;

        public InstanceId objectId;
        public uint regionId;

        public CreatePlayerMsg() {

        }

        public CreatePlayerMsg(BinaryReader data) {
            objectId = data.ReadInstanceId();
            regionId = data.ReadUInt32();
        }

        public void write(BinaryWriter data) {
            data.Write(objectId);
            data.Write(regionId);
        }
    }
}
