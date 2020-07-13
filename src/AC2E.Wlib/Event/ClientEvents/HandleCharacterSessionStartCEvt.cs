using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class HandleCharacterSessionStartCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__HandleCharacterSessionStart;

        // WM_Player::PostCEvt_HandleCharacterSessionStart
        public int money;
        public ActRegistryPkg _aReg;
        public GMQuestInfoListPkg _quests;
        public GameplayOptionsProfilePkg _options;
        public SkillRepositoryPkg _skills;
        public EffectRegistryPkg _regEffect;
        public uint _filledInvLocs;
        public ARHashPkg<InventProfilePkg> _invByLocTable;
        public LRHashPkg<InventProfilePkg> _invByIIDTable;
        public RListPkg<ContainerSegmentDescriptorPkg> _ContainerSegments;
        public LListPkg _Containers;
        public LListPkg _Contents;
        public uint _locFactionStatus;
        public uint _srvFactionStatus;

        public HandleCharacterSessionStartCEvt() {

        }

        public HandleCharacterSessionStartCEvt(BinaryReader data) {
            money = data.UnpackInt32();
            _aReg = data.UnpackPackage<ActRegistryPkg>();
            _quests = data.UnpackPackage<GMQuestInfoListPkg>();
            _options = data.UnpackPackage<GameplayOptionsProfilePkg>();
            _skills = data.UnpackPackage<SkillRepositoryPkg>();
            _regEffect = data.UnpackPackage<EffectRegistryPkg>();
            _filledInvLocs = data.UnpackUInt32();
            _invByLocTable = data.UnpackPackage<ARHashPkg<IPackage>>().to<InventProfilePkg>();
            _invByIIDTable = data.UnpackPackage<LRHashPkg<IPackage>>().to<InventProfilePkg>();
            _ContainerSegments = data.UnpackPackage<RListPkg<IPackage>>().to<ContainerSegmentDescriptorPkg>();
            _Containers = data.UnpackPackage<LListPkg>();
            _Contents = data.UnpackPackage<LListPkg>();
            _locFactionStatus = data.UnpackUInt32();
            _srvFactionStatus = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(money);
            data.Pack(_aReg);
            data.Pack(_quests);
            data.Pack(_options);
            data.Pack(_skills);
            data.Pack(_regEffect);
            data.Pack(_filledInvLocs);
            data.Pack(_invByLocTable);
            data.Pack(_invByIIDTable);
            data.Pack(_ContainerSegments);
            data.Pack(_Containers);
            data.Pack(_Contents);
            data.Pack(_locFactionStatus);
            data.Pack(_srvFactionStatus);
        }
    }
}
