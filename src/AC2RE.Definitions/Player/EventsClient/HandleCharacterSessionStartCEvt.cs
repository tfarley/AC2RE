using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class HandleCharacterSessionStartCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__HandleCharacterSessionStart;

        // WM_Player::PostCEvt_HandleCharacterSessionStart
        public int money; // money
        public ActRegistry actRegistry; // _aReg
        public GMQuestInfoList quests; // _quests
        public GameplayOptionsProfile options; // _options
        public SkillRepository skills; // _skills
        public EffectRegistry effectRegistry; // _regEffect
        public InvLoc filledInventoryLocations; // _filledInvLocs
        public Dictionary<uint, InventProfile> inventoryByLocationTable; // _invByLocTable
        public Dictionary<InstanceId, InventProfile> inventoryByIdTable; // _invByIIDTable
        public List<ContainerSegmentDescriptor> containerSegments; // _ContainerSegments
        public List<InstanceId> containerIds; // _Containers
        public List<InstanceId> contentIds; // _Contents
        public FactionStatus localFactionStatus; // _locFactionStatus
        public FactionStatus serverFactionStatus; // _srvFactionStatus

        public HandleCharacterSessionStartCEvt() {

        }

        public HandleCharacterSessionStartCEvt(AC2Reader data) {
            money = data.UnpackInt32();
            actRegistry = data.UnpackPackage<ActRegistry>();
            quests = data.UnpackPackage<GMQuestInfoList>();
            options = data.UnpackPackage<GameplayOptionsProfile>();
            skills = data.UnpackPackage<SkillRepository>();
            effectRegistry = data.UnpackPackage<EffectRegistry>();
            filledInventoryLocations = (InvLoc)data.UnpackUInt32();
            inventoryByLocationTable = data.UnpackPackage<ARHash>().to<uint, InventProfile>();
            inventoryByIdTable = data.UnpackPackage<LRHash>().to<InstanceId, InventProfile>();
            containerSegments = data.UnpackPackage<RList>().to<ContainerSegmentDescriptor>();
            containerIds = data.UnpackPackage<LList>().to<InstanceId>();
            contentIds = data.UnpackPackage<LList>().to<InstanceId>();
            localFactionStatus = (FactionStatus)data.UnpackUInt32();
            serverFactionStatus = (FactionStatus)data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(money);
            data.Pack(actRegistry);
            data.Pack(quests);
            data.Pack(options);
            data.Pack(skills);
            data.Pack(effectRegistry);
            data.Pack((uint)filledInventoryLocations);
            data.Pack(ARHash.from(inventoryByLocationTable));
            data.Pack(LRHash.from(inventoryByIdTable));
            data.Pack(RList.from(containerSegments));
            data.Pack(LList.from(containerIds));
            data.Pack(LList.from(contentIds));
            data.Pack((uint)localFactionStatus);
            data.Pack((uint)serverFactionStatus);
        }
    }
}
