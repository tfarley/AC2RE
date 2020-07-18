using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestSetPetNameSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestSetPetName;

        // WM_Player::SendSEvt_RequestSetPetName
        public WPString _sPetName;
        public InstanceId _iidPet;

        public RequestSetPetNameSEvt(BinaryReader data) {
            _sPetName = data.UnpackPackage<WPString>();
            _iidPet = data.UnpackInstanceId();
        }
    }
}
