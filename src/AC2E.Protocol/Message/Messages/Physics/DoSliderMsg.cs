using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class DoSliderMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DoSlider_ID;

        // ECM_Physics::RecvEvt_DoSlider
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint sliderId; // sliderID
        public float newValue; // _new_value
        public float time; // _time

        public DoSliderMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            sliderId = data.ReadUInt32();
            newValue = data.ReadSingle();
            time = data.ReadSingle();
        }
    }
}
