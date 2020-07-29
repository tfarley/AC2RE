namespace AC2E.Def {

    public class LeaveWorldMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__LeaveWorld_ID;

        // ECM_Physics::RecvEvt_LeaveWorld
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public ushort posStamp; // _position_stamp

        public LeaveWorldMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            posStamp = data.ReadUInt16();
            data.Align(4);
        }
    }
}
