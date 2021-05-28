using System;

namespace AC2RE.Definitions {

    public class PerkSkill : Skill {

        public override PackageType packageType => PackageType.PerkSkill;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            VITAL_CHANGE = 1 << 0, // 0x00000001, PerkSkill::IsVitalChange
        }

        public int priority; // m_priority
        public double value; // m_value
        public Flag perkFlags; // m_perkFlags
        public uint perkType; // m_perkType

        public PerkSkill(AC2Reader data) : base(data) {
            priority = data.ReadInt32();
            value = data.ReadDouble();
            perkFlags = (Flag)data.ReadUInt32();
            perkType = data.ReadUInt32();
        }
    }
}
