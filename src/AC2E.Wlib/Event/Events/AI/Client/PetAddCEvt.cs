using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class PetAddCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__PetAdd;

        // WM_AI::PostCEvt_AI_PetAdd
        public PetDataPkg _petData;
        public DataId _icon;
        public StringInfo _name;
        public InstanceId _iidPet;

        public PetAddCEvt() {

        }

        public PetAddCEvt(BinaryReader data) {
            _petData = data.UnpackPackage<PetDataPkg>();
            _icon = data.UnpackDataId();
            _name = data.UnpackPackage<StringInfo>();
            _iidPet = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_petData);
            data.Pack(_icon);
            data.Pack(_name);
            data.Pack(_iidPet);
        }
    }
}
