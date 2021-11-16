using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class UsageMessageProcessor : BaseMessageProcessor {

        public UsageMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.Evt_Interp__InterpSEvent_ID: {
                        InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                        if (msg.netEvent.funcId == ServerEventFunctionId.Usage__Usage_TryToUseItem) {
                            TryToUseItemSEvt sEvent = (TryToUseItemSEvt)msg.netEvent;

                            send(player, new InterpCEventPrivateMsg {
                                netEvent = new TryToUseItemDoneCEvt {
                                    usageDesc = sEvent.usageDesc,
                                }
                            });
                        } else {
                            return false;
                        }
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }
    }
}
