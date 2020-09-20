namespace AC2E.Def {

    public class CharGenVerificationMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharGenVerification_ID;

        // ECM_Login::RecvEvt_CharGenVerification
        public CharGenResponse response; // response
        public CharacterIdentity characterIdentity; // _identity
        public uint weenieCharGenResult; // weenieCharGenResult

        public CharGenVerificationMsg() {

        }

        public CharGenVerificationMsg(AC2Reader data) {
            response = (CharGenResponse)data.ReadUInt32();
            characterIdentity = new CharacterIdentity(data);
            weenieCharGenResult = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write((uint)response);
            characterIdentity.write(data);
            data.Write(weenieCharGenResult);
        }
    }
}
