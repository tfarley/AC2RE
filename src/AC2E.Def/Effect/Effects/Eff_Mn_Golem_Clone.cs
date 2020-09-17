namespace AC2E.Def {

    public class Eff_Mn_Golem_Clone : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.Eff_Mn_Golem_Clone;

        public float subVariance; // m_variance
        public int magicDefense; // m_magicDefense
        public int level; // m_level
        public int health; // m_health
        public int damage; // m_damage
        public int meleeAttack; // m_meleeAttack
        public FactionType faction; // m_faction
        public float meleeDefense; // m_meleeDefense
        public float vigor; // m_vigor
        public float armor; // m_armor
        public float magicAttack; // m_magicAttack
        public float missileDefense; // m_missileDefense
        public float missileAttack; // m_missileAttack
        public float deathXp; // m_deathXP
        public StringInfo subName; // m_name

        public Eff_Mn_Golem_Clone(AC2Reader data) : base(data) {
            subVariance = data.ReadSingle();
            magicDefense = data.ReadInt32();
            level = data.ReadInt32();
            health = data.ReadInt32();
            damage = data.ReadInt32();
            meleeAttack = data.ReadInt32();
            faction = (FactionType)data.ReadUInt32();
            meleeDefense = data.ReadSingle();
            vigor = data.ReadSingle();
            armor = data.ReadSingle();
            magicAttack = data.ReadSingle();
            missileDefense = data.ReadSingle();
            missileAttack = data.ReadSingle();
            deathXp = data.ReadSingle();
            data.ReadPkg<StringInfo>(v => subName = v);
        }
    }
}
