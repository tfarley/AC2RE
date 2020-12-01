namespace AC2RE.Definitions {

    public class CPositionMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CPosition_ID;

        // ECM_Physics::SendEvt_CPosition
        public CPositionPack posPack; // _position_pack

        public CPositionMsg(AC2Reader data) {
            posPack = new(data);
        }
    }
}
