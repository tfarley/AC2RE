namespace AC2RE.Definitions {

    public class Eff_Mn_Doppelganger : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.Eff_Mn_Doppelganger;

        public float subVariance; // m_variance
        public int magicDefense; // m_magicDefense
        public int level; // m_level
        public int health; // m_health
        public int damage; // m_damage
        public int meleeAttack; // m_meleeAttack
        public int meleeDefense; // m_meleeDefense
        public int vigor; // m_vigor
        public FxId fx; // m_fx
        public int armor; // m_armor
        public int magicAttack; // m_magicAttack
        public int missileDefense; // m_missileDefense
        public int missileAttack; // m_missileAttack
        public StringInfo subName; // m_name

        public Eff_Mn_Doppelganger(AC2Reader data) : base(data) {
            subVariance = data.ReadSingle();
            magicDefense = data.ReadInt32();
            level = data.ReadInt32();
            health = data.ReadInt32();
            damage = data.ReadInt32();
            meleeAttack = data.ReadInt32();
            meleeDefense = data.ReadInt32();
            vigor = data.ReadInt32();
            fx = (FxId)data.ReadUInt32();
            armor = data.ReadInt32();
            magicAttack = data.ReadInt32();
            missileDefense = data.ReadInt32();
            missileAttack = data.ReadInt32();
            data.ReadPkg<StringInfo>(v => subName = v);
        }
    }
}
