﻿using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CharGenVerificationMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharGenVerification_ID;

        // ECM_Login::RecvEvt_CharGenVerification
        public uint response; // response
        public CharacterIdentity character; // _identity
        public uint weenieCharGenResult; // weenieCharGenResult

        public CharGenVerificationMsg(BinaryReader data) {
            response = data.ReadUInt32();
            character = new CharacterIdentity(data);
            weenieCharGenResult = data.ReadUInt32();
        }
    }
}
