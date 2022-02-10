using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Ingredient : IHeapObject {

    public PackageType packageType => PackageType.Ingredient;

    // WLib Ingredient
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsFixedStackSize = 1 << 0, // IsFixedStackSize 0x00000001
        IsDynamicStackSize = 1 << 1, // IsDynamicStackSize 0x00000002
        HasSpinnerRange = 1 << 2, // HasSpinnerRange 0x00000004
        HasForcedDescription = 1 << 3, // HasForcedDescription 0x00000008
        IsFixedLevel = 1 << 4, // IsFixedLevel 0x00000010
        IsDynamicLevel = 1 << 5, // IsDynamicLevel 0x00000020
        IsFixedArcaneLore = 1 << 6, // IsFixedArcaneLore 0x00000040
        IsDynamicArcaneLore = 1 << 7, // IsDynamicArcaneLore 0x00000080
    }

    public uint ordinal; // m_uiOrdinal
    public StringInfo description; // m_siDesc
    public SingletonPkg<LevelMappingTable> loreMappingTable; // m_loreMappingTable
    public bool autoSpinner; // m_bAutoSpinner
    public uint minSpinnerVal; // m_minSpinnerVal
    public uint requiredFlags; // m_requiredFlags
    public Dictionary<DataId, DataId> items; // m_hashItems
    public Flag flags; // m_flags
    public StringInfo pluralDescription; // m_siPluralDesc
    public bool autoPopulate; // m_bAutoPopulate
    public uint maxSpinnerVal; // m_maxSpinnerVal
    public SingletonPkg<LevelMappingTable> levelMappingTable; // m_levelMappingTable
    public uint quantity; // m_uiQuantity
    public Dictionary<uint, uint> itemClasses; // m_hashItemClasses
    public uint restrictedFlags; // m_restrictedFlags
    public StringInfo forcedDescription; // m_siForcedDesc
    public int level; // m_iLevel
    public int lore; // m_iLore
    public SingletonPkg<LevelMappingTable> stackMappingTable; // m_stackMappingTable

    public Ingredient(AC2Reader data) {
        ordinal = data.ReadUInt32();
        data.ReadHO<StringInfo>(v => description = v);
        data.ReadHO<LevelMappingTable>(v => loreMappingTable = v);
        autoSpinner = data.ReadBoolean();
        minSpinnerVal = data.ReadUInt32();
        requiredFlags = data.ReadUInt32();
        data.ReadHO<AAHash>(v => items = v.to<DataId, DataId>());
        flags = (Flag)data.ReadUInt32();
        data.ReadHO<StringInfo>(v => pluralDescription = v);
        autoPopulate = data.ReadBoolean();
        maxSpinnerVal = data.ReadUInt32();
        data.ReadHO<LevelMappingTable>(v => levelMappingTable = v);
        quantity = data.ReadUInt32();
        data.ReadHO<AAHash>(v => itemClasses = v);
        restrictedFlags = data.ReadUInt32();
        data.ReadHO<StringInfo>(v => forcedDescription = v);
        level = data.ReadInt32();
        lore = data.ReadInt32();
        data.ReadHO<LevelMappingTable>(v => stackMappingTable = v);
    }
}
