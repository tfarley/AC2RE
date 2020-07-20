namespace AC2E.Def {

    public class CPositionMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CPosition_ID;

        // ECM_Physics::SendEvt_CPosition
        public CPositionPack positionPack; // _position_pack

        public CPositionMsg(AC2Reader data) {
            positionPack = new CPositionPack(data);
        }
    }
}
