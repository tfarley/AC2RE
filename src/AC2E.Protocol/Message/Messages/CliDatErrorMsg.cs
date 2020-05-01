﻿using AC2E.Dat;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CliDatErrorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_DATABASE;

        public MessageOpcode opcode => MessageOpcode.CLIDAT_ERROR_EVENT;

        public DbType fileDbType;
        public uint fileId;
        public uint unk1;

        public CliDatErrorMsg() {

        }

        public CliDatErrorMsg(BinaryReader data) {
            fileDbType = (DbType)data.ReadUInt32();
            fileId = data.ReadUInt32();
            unk1 = data.ReadUInt32();
        }

        public void write(BinaryWriter data) {
            data.Write((uint)fileDbType);
            data.Write(fileId);
            data.Write(unk1);
        }
    }
}
