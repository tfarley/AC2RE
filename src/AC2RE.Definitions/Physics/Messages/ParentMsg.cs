namespace AC2RE.Definitions {

    public class ParentMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Physics__Parent;

        // ECM_Physics::RecvEvt_Parent
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public InstanceIdWithStamp parentIdWithChildPosStamp; // _parent_id_and_child_position_stamp
        public HoldingLocation childLocation; // _child_location
        public Orientation orientationId; // _orientation_id

        public ParentMsg() {

        }

        public ParentMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            parentIdWithChildPosStamp = data.ReadInstanceIdWithStamp();
            childLocation = (HoldingLocation)data.ReadUInt32();
            orientationId = (Orientation)data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write(parentIdWithChildPosStamp);
            data.Write((uint)childLocation);
            data.Write((uint)orientationId);
        }
    }
}
