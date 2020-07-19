using System.IO;

namespace AC2E.Def {

    public class HandleCharacterSessionStartCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__HandleCharacterSessionStart;

        // WM_Player::PostCEvt_HandleCharacterSessionStart
        public int money;
        public ActRegistry _aReg;
        public GMQuestInfoList _quests;
        public GameplayOptionsProfile _options;
        public SkillRepository _skills;
        public EffectRegistry _regEffect;
        public uint _filledInvLocs;
        public ARHash<InventProfile> _invByLocTable;
        public LRHash<InventProfile> _invByIIDTable;
        public RList<ContainerSegmentDescriptor> _ContainerSegments;
        public InstanceIdList _Containers;
        public InstanceIdList _Contents;
        public uint _locFactionStatus;
        public uint _srvFactionStatus;

        public HandleCharacterSessionStartCEvt() {

        }

        public HandleCharacterSessionStartCEvt(BinaryReader data) {
            money = data.UnpackInt32();
            _aReg = data.UnpackPackage<ActRegistry>();
            _quests = data.UnpackPackage<GMQuestInfoList>();
            _options = data.UnpackPackage<GameplayOptionsProfile>();
            _skills = data.UnpackPackage<SkillRepository>();
            _regEffect = data.UnpackPackage<EffectRegistry>();
            _filledInvLocs = data.UnpackUInt32();
            _invByLocTable = data.UnpackPackage<ARHash<IPackage>>().to<InventProfile>();
            _invByIIDTable = data.UnpackPackage<LRHash<IPackage>>().to<InventProfile>();
            _ContainerSegments = data.UnpackPackage<RList<IPackage>>().to<ContainerSegmentDescriptor>();
            _Containers = new InstanceIdList(data.UnpackPackage<LList>());
            _Contents = new InstanceIdList(data.UnpackPackage<LList>());
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
