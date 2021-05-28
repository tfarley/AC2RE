﻿using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Ingredient : IPackage {

        public PackageType packageType => PackageType.Ingredient;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            FIXED_STACK_SIZE = 1 << 0, // 0x00000001, Ingredient::IsFixedStackSize
            DYNAMIC_STACK_SIZE = 1 << 1, // 0x00000002, Ingredient::IsDynamicStackSize
            SPINNER_RANGE = 1 << 2, // 0x00000004, Ingredient::HasSpinnerRange
            FORCED_DESCRIPTION = 1 << 3, // 0x00000008, Ingredient::HasForcedDescription
            FIXED_LEVEL = 1 << 4, // 0x00000010, Ingredient::IsFixedLevel
            DYNAMIC_LEVEL = 1 << 5, // 0x00000020, Ingredient::IsDynamicLevel
            FIXED_ARCANE_LORE = 1 << 6, // 0x00000040, Ingredient::IsFixedArcaneLore
            DYNAMIC_ARCANE_LORE = 1 << 7, // 0x00000080, Ingredient::IsDynamicArcaneLore
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
            data.ReadPkg<StringInfo>(v => description = v);
            data.ReadPkg<LevelMappingTable>(v => loreMappingTable = v);
            autoSpinner = data.ReadBoolean();
            minSpinnerVal = data.ReadUInt32();
            requiredFlags = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => items = v.to<DataId, DataId>());
            flags = (Flag)data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => pluralDescription = v);
            autoPopulate = data.ReadBoolean();
            maxSpinnerVal = data.ReadUInt32();
            data.ReadPkg<LevelMappingTable>(v => levelMappingTable = v);
            quantity = data.ReadUInt32();
            data.ReadPkg<AAHash>(v => itemClasses = v);
            restrictedFlags = data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => forcedDescription = v);
            level = data.ReadInt32();
            lore = data.ReadInt32();
            data.ReadPkg<LevelMappingTable>(v => stackMappingTable = v);
        }
    }
}
