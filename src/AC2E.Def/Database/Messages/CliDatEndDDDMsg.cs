namespace AC2E.Def {

    public class CliDatEndDDDMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.DATABASE;
        public MessageOpcode opcode => MessageOpcode.CLIDAT_END_DDD_EVENT;

        public CliDatEndDDDMsg() {

        }

        public CliDatEndDDDMsg(AC2Reader data) {

        }

        public void write(AC2Writer data) {

        }
    }
}
