﻿using AC2E.Dat;
using System.IO;

namespace AC2E.Protocol {

    public class CliDatRequestDataMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.DATABASE;
        public MessageOpcode opcode => MessageOpcode.CLIDAT_REQUEST_DATA_EVENT;

        // CCliDatRequestEvent::CDataFormat
        public QualifiedDataId qdid; // qdid

        public CliDatRequestDataMsg(BinaryReader data) {
            qdid = data.ReadQualifiedDataId();
        }
    }
}
