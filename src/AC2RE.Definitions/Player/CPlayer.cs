using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CPlayer : Player {

    public override PackageType packageType => PackageType.CPlayer;

    public InstanceId lastSelectionId; // m_lastSelectionID
    public WPString logFile; // m_logFile
    public uint curSpecialAttackId; // m_curr_special_attack_id
    public CShopperContext shopperContext; // m_shopperContext
    public List<IPackage> friendList; // m_friendList
    public SelectionInfo selectionInfo; // m_selectionInfo
    public uint curWarmupBehavior; // m_currentWarmupBehavior
    public HistoryList replyHistoryList; // m_replyHistoryList
    public StringInfo lastTellee; // m_siLastTellee
    public StringInfo lastTeller; // m_siLastTeller
    public uint allegianceRoomId; // m_allegianceRoomID
    public CPetState clientPetState; // m_clientPetState
    public StringInfo motd; // m_motd
    public GMQuestInfoList quests; // m_Quests
    public List<IPackage> squelchList; // m_squelchList
    public HistoryList retellHistoryList; // m_retellHistoryList
    public bool isExamining; // m_isExamining
    public PublicVendorProfile curVendor; // m_pvpCurrentVendor
    public InstanceId selectionInfoTarget; // m_selectionInfoTgt

    public CPlayer(AC2Reader data) : base(data) {
        lastSelectionId = data.ReadInstanceId();
        data.ReadPkg<WPString>(v => logFile = v);
        curSpecialAttackId = data.ReadUInt32();
        data.ReadPkg<CShopperContext>(v => shopperContext = v);
        data.ReadPkg<RList>(v => friendList = v);
        data.ReadPkg<SelectionInfo>(v => selectionInfo = v);
        curWarmupBehavior = data.ReadUInt32();
        data.ReadPkg<HistoryList>(v => replyHistoryList = v);
        data.ReadPkg<StringInfo>(v => lastTellee = v);
        data.ReadPkg<StringInfo>(v => lastTeller = v);
        allegianceRoomId = data.ReadUInt32();
        data.ReadPkg<CPetState>(v => clientPetState = v);
        data.ReadPkg<StringInfo>(v => motd = v);
        data.ReadPkg<GMQuestInfoList>(v => quests = v);
        data.ReadPkg<RList>(v => squelchList = v);
        data.ReadPkg<HistoryList>(v => retellHistoryList = v);
        isExamining = data.ReadBoolean();
        data.ReadPkg<PublicVendorProfile>(v => curVendor = v);
        selectionInfoTarget = data.ReadInstanceId();
    }
}
