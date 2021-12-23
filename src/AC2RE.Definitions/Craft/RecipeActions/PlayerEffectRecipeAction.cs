using System;

namespace AC2RE.Definitions;

public class PlayerEffectRecipeAction : IPackage {

    public PackageType packageType => PackageType.PlayerEffectRecipeAction;

    // WLib PlayerEffectRecipeAction
    [Flags]
    public enum Flag : uint {
        None = 0,
        HasEffect = 1 << 0, // SetEffectByName 0x00000001
        HasEffectDynamicSpellcraft = 1 << 1, // SetEffectByNameDynamicSpellcraft 0x00000002
    }

    public float spellcraft; // m_fSpellcraft
    public SingletonPkg<Effect> effect; // m_effect
    public Flag flags; // m_flags
    public DataId mappingTableDid; // m_didMappingTable

    public PlayerEffectRecipeAction(AC2Reader data) {
        spellcraft = data.ReadSingle();
        data.ReadPkg<Effect>(v => effect = v);
        flags = (Flag)data.ReadUInt32();
        mappingTableDid = data.ReadDataId();
    }
}
