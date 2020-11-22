namespace AC2RE.Definitions {

    public class StaticAttackHook : AttackHook {

        public override PackageType packageType => PackageType.StaticAttackHook;

        public int addMod; // m_AddMod
        public float multMod; // m_MultMod

        public StaticAttackHook(AC2Reader data) : base(data) {
            addMod = data.ReadInt32();
            multMod = data.ReadSingle();
        }
    }
}
