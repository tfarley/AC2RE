namespace AC2E.Def {

    public class ParentMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__Parent_ID;

        // ECM_Physics::RecvEvt_Parent
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public InstanceIdWithStamp parentIdWithChildPositionStamp; // _parent_id_and_child_position_stamp
        public uint childLocation; // _child_location
        public uint orientationId; // _orientation_id

        public ParentMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            parentIdWithChildPositionStamp = data.ReadInstanceIdWithStamp();
            childLocation = data.ReadUInt32();
            orientationId = data.ReadUInt32();
        }
    }
}
