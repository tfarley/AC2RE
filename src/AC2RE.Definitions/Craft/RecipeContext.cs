using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RecipeContext : IHeapObject {

    public PackageType packageType => PackageType.RecipeContext;

    public float craftCheck; // m_fCraftCheck
    public uint spinnerVal; // m_uiSpinnerVal
    public int skillLevel; // m_iSkillLvl
    public bool notifyUI; // m_bNotifyUI
    public DataId recipeDid; // m_didRecipe
    public Player crafter; // m_crafter
    public Dictionary<uint, ulong> productIds; // m_productIIDs
    public float randVal; // m_fRandVal
    public long craftXp; // m_craftXP
    public InstanceId targetId; // m_iidTarget
    public CraftBlob blob; // m_blob
    public int cost; // m_cost
    public Dictionary<uint, ulong> ingredientIds; // m_ingredientIIDs

    public RecipeContext(AC2Reader data) {
        craftCheck = data.ReadSingle();
        spinnerVal = data.ReadUInt32();
        skillLevel = data.ReadInt32();
        notifyUI = data.ReadBoolean();
        recipeDid = data.ReadDataId();
        data.ReadHO<Player>(v => crafter = v);
        data.ReadHO<ALHash>(v => productIds = v);
        randVal = data.ReadSingle();
        craftXp = data.ReadInt64();
        targetId = data.ReadInstanceId();
        data.ReadHO<CraftBlob>(v => blob = v);
        cost = data.ReadInt32();
        data.ReadHO<ALHash>(v => ingredientIds = v);
    }
}
