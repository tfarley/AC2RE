using AC2E.Def.Extensions;
using AC2E.Interp.Extensions;
using AC2E.Interp.Packages;
using System.IO;

namespace AC2E.Interp.Event.ClientEvents {

    public class HandleCharacterSessionStartCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__HandleCharacterSessionStart;

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
