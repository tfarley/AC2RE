using System.Collections.Generic;

namespace AC2RE.Definitions;

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
    public Dictionary<InvLoc, InventProfile> inventoryByLocationTable; // _invByLocTable
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
        actRegistry = data.UnpackHeapObject<ActRegistry>();
        quests = data.UnpackHeapObject<GMQuestInfoList>();
        options = data.UnpackHeapObject<GameplayOptionsProfile>();
        skills = data.UnpackHeapObject<SkillRepository>();
        effectRegistry = data.UnpackHeapObject<EffectRegistry>();
        filledInventoryLocations = data.UnpackEnum<InvLoc>();
        inventoryByLocationTable = data.UnpackHeapObject<ARHash>().to<InvLoc, InventProfile>();
        inventoryByIdTable = data.UnpackHeapObject<LRHash>().to<InstanceId, InventProfile>();
        containerSegments = data.UnpackHeapObject<RList>().to<ContainerSegmentDescriptor>();
        containerIds = data.UnpackHeapObject<LList>().to<InstanceId>();
        contentIds = data.UnpackHeapObject<LList>().to<InstanceId>();
        localFactionStatus = data.UnpackEnum<FactionStatus>();
        serverFactionStatus = data.UnpackEnum<FactionStatus>();
    }

    public void write(AC2Writer data) {
        data.Pack(money);
        data.Pack(actRegistry);
        data.Pack(quests);
        data.Pack(options);
        data.Pack(skills);
        data.Pack(effectRegistry);
        data.PackEnum(filledInventoryLocations);
        data.Pack(ARHash.from(inventoryByLocationTable));
        data.Pack(LRHash.from(inventoryByIdTable));
        data.Pack(RList.from(containerSegments));
        data.Pack(LList.from(containerIds));
        data.Pack(LList.from(contentIds));
        data.PackEnum(localFactionStatus);
        data.PackEnum(serverFactionStatus);
    }
}
