using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CharacterExitGameSMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharExitGame_ID;

        // ECM_Login::RecvEvt_CharExitGame
        public InstanceId characterId;

        public CharacterExitGameSMsg(BinaryReader data) {
            characterId = data.ReadInstanceId();
        }
    }
}
