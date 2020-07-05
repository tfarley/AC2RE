using AC2E.Def;
using AC2E.Utils;
using System.IO;

namespace AC2E.Protocol {

    public class CreateObjectMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CreateObject_ID;

        // TODO: Complete packet breakdown
        // CM_Physics::RecvEvt_CreateObject
        public InstanceId objectId; // objectID
        public VisualDesc vDesc; // _vdesc
        public PhysicsDesc pDesc; // _pdesc
        //public WeenieDesc wDesc; // _wdesc

        public CreateObjectMsg() {

        }

        public CreateObjectMsg(BinaryReader data) {
            objectId = data.ReadInstanceId();
            vDesc = new VisualDesc(data);
            pDesc = new PhysicsDesc(data);
        }

        public void write(BinaryWriter data) {
            //data.Write(objectId);
            //vDesc.write(data);
            //pDesc.write(data);
            data.Write(Util.hexStringToBytes("9ddd000000003021220100002300001f2f2a693f2f2a693fa3707d3f2100000002000040070000024e0000200200000202000000295c0f3e100000000000803f5000002002000002020000008fc2753e100000000000803f0c00002001000002010000009a99993e0d0000200300000203000000cdcc4c3e0b0000009a99193e0c0000009a99993e0e000020010000021c0000418fc2753df900002002000002020000000ad7233d100000000000803f160000200200000202000000cdcc4c3e100000000000803f828080000500080001000040010000000a0000400000803f000000003e00b58d84221e43f8d5eb42458101434ca5733e0000000000000000dfa5783f713d8a3f010000000000000005811f027f030000300500470000000000000000000001000c007c9212e56b760beb55dd38fcf1a672cbb9e6947f1ccd73f500002d3b00000000302120008004000000001000203e000000009000e00300000000ccde3c00"));
        }
    }
}
