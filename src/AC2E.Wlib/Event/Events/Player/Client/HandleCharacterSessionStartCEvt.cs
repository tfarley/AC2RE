using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class HandleCharacterSessionStartCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__HandleCharacterSessionStart;

        // WM_Player::PostCEvt_HandleCharacterSessionStart
        public int money;
        public ActRegistryPkg _aReg;
        public GMQuestInfoList _quests;
        public GameplayOptionsProfile _options;
        public SkillRepositoryPkg _skills;
        public EffectRegistryPkg _regEffect;
        public uint _filledInvLocs;
        public ARHash<InventProfilePkg> _invByLocTable;
        public LRHash<InventProfilePkg> _invByIIDTable;
        public RList<ContainerSegmentDescriptorPkg> _ContainerSegments;
        public LList _Containers;
        public LList _Contents;
        public uint _locFactionStatus;
        public uint _srvFactionStatus;

        public HandleCharacterSessionStartCEvt() {

        }

        public HandleCharacterSessionStartCEvt(BinaryReader data) {
            money = data.UnpackInt32();
            _aReg = data.UnpackPackage<ActRegistryPkg>();
            _quests = data.UnpackPackage<GMQuestInfoList>();
            _options = data.UnpackPackage<GameplayOptionsProfile>();
            _skills = data.UnpackPackage<SkillRepositoryPkg>();
            _regEffect = data.UnpackPackage<EffectRegistryPkg>();
            _filledInvLocs = data.UnpackUInt32();
            _invByLocTable = data.UnpackPackage<ARHash<IPackage>>().to<InventProfilePkg>();
            _invByIIDTable = data.UnpackPackage<LRHash<IPackage>>().to<InventProfilePkg>();
            _ContainerSegments = data.UnpackPackage<RList<IPackage>>().to<ContainerSegmentDescriptorPkg>();
            _Containers = data.UnpackPackage<LList>();
            _Contents = data.UnpackPackage<LList>();
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
