namespace AC2RE.Definitions {

    public class PerkSkill : Skill {

        public override PackageType packageType => PackageType.PerkSkill;

        public int priority; // m_priority
        public double value; // m_value
        public uint perkFlags; // m_perkFlags
        public uint perkType; // m_perkType

        public PerkSkill(AC2Reader data) : base(data) {
            priority = data.ReadInt32();
            value = data.ReadDouble();
            perkFlags = data.ReadUInt32();
            perkType = data.ReadUInt32();
        }
    }
}
