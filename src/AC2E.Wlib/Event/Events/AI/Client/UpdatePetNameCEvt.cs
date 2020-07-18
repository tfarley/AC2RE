using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdatePetNameCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__UpdatePetName;

        // WM_AI::PostCEvt_AI_UpdatePetName
        public StringInfo _petName;
        public InstanceId _iidPet;

        public UpdatePetNameCEvt() {

        }

        public UpdatePetNameCEvt(BinaryReader data) {
            _petName = data.UnpackPackage<StringInfo>();
            _iidPet = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_petName);
            data.Pack(_iidPet);
        }
    }
}
