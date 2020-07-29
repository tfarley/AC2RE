namespace AC2E.Def {

    public class HandleCharacterSessionStartCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__HandleCharacterSessionStart;

        // WM_Player::PostCEvt_HandleCharacterSessionStart
        public int money; // money
        public ActRegistry actRegistry; // _aReg
        public GMQuestInfoList quests; // _quests
        public GameplayOptionsProfile options; // _options
        public SkillRepository skills; // _skills
        public EffectRegistry effectRegistry; // _regEffect
        public uint filledInventoryLocations; // _filledInvLocs
        public ARHash<InventProfile> inventoryByLocationTable; // _invByLocTable
        public LRHash<InventProfile> inventoryByIdTable; // _invByIIDTable
        public RList<ContainerSegmentDescriptor> containerSegments; // _ContainerSegments
        public InstanceIdList containerIds; // _Containers
        public InstanceIdList contentIds; // _Contents
        public uint localFactionStatus; // _locFactionStatus
        public uint serverFactionStatus; // _srvFactionStatus

        public HandleCharacterSessionStartCEvt() {

        }

        public HandleCharacterSessionStartCEvt(AC2Reader data) {
            money = data.UnpackInt32();
            actRegistry = data.UnpackPackage<ActRegistry>();
            quests = data.UnpackPackage<GMQuestInfoList>();
            options = data.UnpackPackage<GameplayOptionsProfile>();
            skills = data.UnpackPackage<SkillRepository>();
            effectRegistry = data.UnpackPackage<EffectRegistry>();
            filledInventoryLocations = data.UnpackUInt32();
            inventoryByLocationTable = data.UnpackPackage<ARHash<IPackage>>().to<InventProfile>();
            inventoryByIdTable = data.UnpackPackage<LRHash<IPackage>>().to<InventProfile>();
            containerSegments = data.UnpackPackage<RList<IPackage>>().to<ContainerSegmentDescriptor>();
            containerIds = new InstanceIdList(data.UnpackPackage<LList>());
            contentIds = new InstanceIdList(data.UnpackPackage<LList>());
            localFactionStatus = data.UnpackUInt32();
            serverFactionStatus = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(money);
            data.Pack(actRegistry);
            data.Pack(quests);
            data.Pack(options);
            data.Pack(skills);
            data.Pack(effectRegistry);
            data.Pack(filledInventoryLocations);
            data.Pack(inventoryByLocationTable);
            data.Pack(inventoryByIdTable);
            data.Pack(containerSegments);
            data.Pack(containerIds);
            data.Pack(contentIds);
            data.Pack(localFactionStatus);
            data.Pack(serverFactionStatus);
        }
    }
}
