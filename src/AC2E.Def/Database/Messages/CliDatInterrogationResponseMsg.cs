﻿namespace AC2E.Def {

    public class CliDatInterrogationResponseMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.DATABASE;
        public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT;

        // CCliDatInterrogationResponseEvent::CDataFormat
        public Language language; // LanguageID
        // public uint[] IterationListData = new uint[4]; // IterationListData

        public CliDatInterrogationResponseMsg(AC2Reader data) {
            language = (Language)data.ReadUInt32();
            // TODO: Read IterationListData, 88 bytes worth - always?
        }
    }
}