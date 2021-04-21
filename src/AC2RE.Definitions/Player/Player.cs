using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Player : Agent {

        public override PackageType packageType => PackageType.Player;

        public StringInfo vassalName; // m_vassalName
        public InstanceId selectionId; // m_selectionID
        public Fellowship fellowship; // m_fellowship
        public ExaminationRequest examinationRequest; // m_examinationRequest
        public Dictionary<ulong, IPackage> saleReminders; // m_saleReminders
        public Dictionary<uint, IPackage> channels; // m_channels
        public bool isAttacking; // m_isAttacking
        public CraftRegistry craftRegistry; // m_craftRegistry
        public GameplayOptionsProfile currentOptionsProfile; // m_currentOptionsProfile
        public bool logoffOK; // m_bLogoffOK
        public ResurrectionRequest rezRequest; // m_rezRequest
        public Trade trade; // m_trade
        public ActRegistry acts; // m_Acts

        public Player(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => vassalName = v);
            selectionId = data.ReadInstanceId();
            data.ReadPkg<Fellowship>(v => fellowship = v);
            data.ReadPkg<ExaminationRequest>(v => examinationRequest = v);
            data.ReadPkg<LRHash>(v => saleReminders = v);
            data.ReadPkg<ARHash>(v => channels = v);
            isAttacking = data.ReadBoolean();
            data.ReadPkg<CraftRegistry>(v => craftRegistry = v);
            data.ReadPkg<GameplayOptionsProfile>(v => currentOptionsProfile = v);
            logoffOK = data.ReadBoolean();
            data.ReadPkg<ResurrectionRequest>(v => rezRequest = v);
            data.ReadPkg<Trade>(v => trade = v);
            data.ReadPkg<ActRegistry>(v => acts = v);
        }
    }
}
