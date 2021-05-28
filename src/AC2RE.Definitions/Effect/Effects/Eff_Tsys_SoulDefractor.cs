namespace AC2RE.Definitions {

    public class Eff_Tsys_SoulDefractor : GenesisEffect {

        public override PackageType packageType => PackageType.Eff_Tsys_SoulDefractor;

        public float subVariance; // m_variance
        public AIMovementType movementType; // m_mType
        public int magicDefense; // m_magicDefense
        public int level; // m_level
        public int health; // m_health
        public int damage; // m_damage
        public int meleeAttack; // m_meleeAttack
        public uint petClass; // m_petClass
        public DataId petIconDid; // m_petIcon
        public int meleeDefense; // m_meleeDefense
        public int vigor; // m_vigor
        public FxId fx; // m_fx
        public IPackage loveTable; // m_loveTable
        public int armor; // m_armor
        public int magicAttack; // m_magicAttack
        public uint petFlags; // m_petFlags
        public int missileDefense; // m_missileDefense
        public int missileAttack; // m_missileAttack
        public SingletonPkg<IPackage> npcTable; // m_npcTable
        public StringInfo subName; // m_name

        public Eff_Tsys_SoulDefractor(AC2Reader data) : base(data) {
            subVariance = data.ReadSingle();
            movementType = (AIMovementType)data.ReadUInt32();
            magicDefense = data.ReadInt32();
            level = data.ReadInt32();
            health = data.ReadInt32();
            damage = data.ReadInt32();
            meleeAttack = data.ReadInt32();
            petClass = data.ReadUInt32();
            petIconDid = data.ReadDataId();
            meleeDefense = data.ReadInt32();
            vigor = data.ReadInt32();
            fx = (FxId)data.ReadUInt32();
            data.ReadPkg<IPackage>(v => loveTable = v); // TODO: AILoveTable, possibly singleton too
            armor = data.ReadInt32();
            magicAttack = data.ReadInt32();
            petFlags = data.ReadUInt32();
            missileDefense = data.ReadInt32();
            missileAttack = data.ReadInt32();
            data.ReadPkg<IPackage>(v => npcTable = v); // TODO: NPCTable
            data.ReadPkg<StringInfo>(v => subName = v);
        }
    }
}
