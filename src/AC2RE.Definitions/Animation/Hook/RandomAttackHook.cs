namespace AC2RE.Definitions {

    public class RandomAttackHook : AttackHook {

        public override PackageType packageType => PackageType.RandomAttackHook;

        public float multMaxDmg; // m_fMultMaxDmg
        public float addMinDmg; // m_fAddMinDmg
        public float multMinDmg; // m_fMultMinDmg
        public float addMaxDmg; // m_fAddMaxDmg

        public RandomAttackHook(AC2Reader data) : base(data) {
            multMaxDmg = data.ReadSingle();
            addMinDmg = data.ReadSingle();
            multMinDmg = data.ReadSingle();
            addMaxDmg = data.ReadSingle();
        }
    }
}
