using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Act : MasterListMember {

    public override PackageType packageType => PackageType.Act;

    public StringInfo longName; // m_siLongName
    public bool isCompletable; // m_bIsCompletable
    public List<IHeapObject> completionEffects; // m_actCompletionEffects
    public bool isVisible; // m_bIsVisible
    public DataId iconDid; // m_didIcon
    public StringInfo description; // m_siDescription
    public Dictionary<uint, IHeapObject> sceneExaminationInfo; // m_sceneExaminationInfo
    public uint numToComplete; // m_uiNumToComplete
    public Dictionary<SceneId, GMSceneInfo> sceneTable; // m_sceneTable
    public bool isActive; // m_bIsActive
    public uint actNum; // m_uiActNum
    public StringInfo name; // m_strName

    public Act(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => longName = v);
        isCompletable = data.ReadBoolean();
        data.ReadHO<RList>(v => completionEffects = v);
        isVisible = data.ReadBoolean();
        iconDid = data.ReadDataId();
        data.ReadHO<StringInfo>(v => description = v);
        data.ReadHO<ARHash>(v => sceneExaminationInfo = v);
        numToComplete = data.ReadUInt32();
        data.ReadHO<ARHash>(v => sceneTable = v.to<SceneId, GMSceneInfo>());
        isActive = data.ReadBoolean();
        actNum = data.ReadUInt32();
        data.ReadHO<StringInfo>(v => name = v);
    }
}
