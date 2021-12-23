using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CraftRandomEntry : IPackage {

    public PackageType packageType => PackageType.CraftRandomEntry;

    public List<IPackage> outcomeRecipeActions; // m_outcomeActions
    public float craftXpMod; // m_fCraftXPMod
    public float threshold; // m_fThresh
    public uint inventorySlots; // m_uiInvSlots

    public CraftRandomEntry(AC2Reader data) {
        data.ReadPkg<RList>(v => outcomeRecipeActions = v);
        craftXpMod = data.ReadSingle();
        threshold = data.ReadSingle();
        inventorySlots = data.ReadUInt32();
    }
}
