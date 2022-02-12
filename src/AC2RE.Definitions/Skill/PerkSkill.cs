using System;

namespace AC2RE.Definitions;

public class PerkSkill : Skill {

    public override PackageType packageType => PackageType.PerkSkill;

    // WLib PerkSkill
    [Flags]
    public new enum Flag : uint {
        None = 0,
        IsVitalChange = 1 << 0, // IsVitalChange 0x00000001
    }

    public int priority; // m_priority
    public double value; // m_value
    public Flag perkFlags; // m_perkFlags
    public uint perkType; // m_perkType

    public PerkSkill(AC2Reader data) : base(data) {
        priority = data.ReadInt32();
        value = data.ReadDouble();
        perkFlags = data.ReadEnum<Flag>();
        perkType = data.ReadUInt32();
    }
}
