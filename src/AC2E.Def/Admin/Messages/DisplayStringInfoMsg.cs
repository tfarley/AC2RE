namespace AC2E.Def {

    public class DisplayStringInfoMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Admin__DisplayStringInfo_ID;

        // ECM_Admin::RecvEvt_DisplayStringInfo
        public TextType type; // type
        public StringInfo text; // _si

        public DisplayStringInfoMsg() {

        }

        public DisplayStringInfoMsg(AC2Reader data) {
            type = (TextType)data.ReadUInt32();
            text = new StringInfo(data);
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            text.write(data);
        }
    }
}
