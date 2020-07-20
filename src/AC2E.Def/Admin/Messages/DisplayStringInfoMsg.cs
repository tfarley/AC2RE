namespace AC2E.Def {

    public class DisplayStringInfoMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Admin__DisplayStringInfo_ID;

        // ECM_Admin::RecvEvt_DisplayStringInfo
        public uint type; // type
        public StringInfo stringInfo; // _si

        public DisplayStringInfoMsg() {

        }

        public DisplayStringInfoMsg(AC2Reader data) {
            type = data.ReadUInt32();
            stringInfo = new StringInfo(data);
        }

        public void write(AC2Writer data) {
            data.Write(type);
            stringInfo.write(data);
        }
    }
}
