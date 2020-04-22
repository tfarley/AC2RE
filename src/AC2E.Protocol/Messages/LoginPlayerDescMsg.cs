﻿using AC2E.Protocol.NetBlob;
using AC2E.Utils;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class LoginPlayerDescMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.NET_QUEUE_EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Login__PlayerDesc_ID;

        // TODO: Complete packet breakdown

        public void write(BinaryWriter data) {
            data.Write(Util.hexStringToBytes("3f0800003005008104811f000000000000000000000001000c007c9212e56b760beb55dd38fcf1a672cbb9e6947f1ccd73f500002d3b00000000302120008004000000001000203e000000009000e003000000000b000003180100004e0000002201000001000000230100000010000024010000020000002e01000007000000b6030000ffffffff1c0c000087010000020100003601000003010000180100000701000018010000080100001801000001000002d207000000000000040000021e0100000000c842060100000000803f070100000000803fb80b00000000f041010000022f0100007bcad24da9ff9c410100000201000000cd000047040000012c01000086030000000000002d010000d202000000000000e80300005000000000000000e90300002800000000000000a4de3c00"));
        }
    }
}
