using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Player : Agent {

    public override PackageType packageType => PackageType.Player;

    public StringInfo vassalName; // m_vassalName
    public InstanceId selectionId; // m_selectionID
    public Fellowship fellowship; // m_fellowship
    public ExaminationRequest examinationRequest; // m_examinationRequest
    public Dictionary<ulong, IHeapObject> saleReminders; // m_saleReminders
    public Dictionary<uint, IHeapObject> channels; // m_channels
    public bool isAttacking; // m_isAttacking
    public CraftRegistry craftRegistry; // m_craftRegistry
    public GameplayOptionsProfile currentOptionsProfile; // m_currentOptionsProfile
    public bool logoffOK; // m_bLogoffOK
    public ResurrectionRequest rezRequest; // m_rezRequest
    public Trade trade; // m_trade
    public ActRegistry acts; // m_Acts

    public Player(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => vassalName = v);
        selectionId = data.ReadInstanceId();
        data.ReadHO<Fellowship>(v => fellowship = v);
        data.ReadHO<ExaminationRequest>(v => examinationRequest = v);
        data.ReadHO<LRHash>(v => saleReminders = v);
        data.ReadHO<ARHash>(v => channels = v);
        isAttacking = data.ReadBoolean();
        data.ReadHO<CraftRegistry>(v => craftRegistry = v);
        data.ReadHO<GameplayOptionsProfile>(v => currentOptionsProfile = v);
        logoffOK = data.ReadBoolean();
        data.ReadHO<ResurrectionRequest>(v => rezRequest = v);
        data.ReadHO<Trade>(v => trade = v);
        data.ReadHO<ActRegistry>(v => acts = v);
    }
}
