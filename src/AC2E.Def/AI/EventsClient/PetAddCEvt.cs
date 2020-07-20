namespace AC2E.Def {

    public class PetAddCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.AI__PetAdd;

        // WM_AI::PostCEvt_AI_PetAdd
        public PetData _petData;
        public DataId _icon;
        public StringInfo _name;
        public InstanceId _iidPet;

        public PetAddCEvt() {

        }

        public PetAddCEvt(AC2Reader data) {
            _petData = data.UnpackPackage<PetData>();
            _icon = data.UnpackDataId();
            _name = data.UnpackPackage<StringInfo>();
            _iidPet = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_petData);
            data.Pack(_icon);
            data.Pack(_name);
            data.Pack(_iidPet);
        }
    }
}
