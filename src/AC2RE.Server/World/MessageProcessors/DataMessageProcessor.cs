using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class DataMessageProcessor : BaseMessageProcessor {

        public DataMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.CLIDAT_REQUEST_DATA_EVENT: {
                        CliDatRequestDataMsg msg = (CliDatRequestDataMsg)genericMsg;
                        send(player, new CliDatErrorMsg {
                            qdid = msg.qdid,
                            error = 1,
                        });
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }
    }
}
