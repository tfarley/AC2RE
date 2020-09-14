namespace AC2E.Def {

    public class DamageDisplayInfo : IPackage {

        public PackageType packageType => PackageType.DamageDisplayInfo;

        public bool playerIsTarget; // m_playerIsTarget
        public bool playerIsAttacker; // m_playerIsAttacker
        public bool harmful; // m_harmful
        public uint attackerHealthPoints; // m_attackerHealthPoints
        public uint attackResult; // m_attackResult
        public bool heal; // m_heal
        public uint targetHealthPoints; // m_targetHealthPoints
        public bool attackerIsPlayersPet; // m_attackerIsPlayersPet
        public bool nonDamaging; // m_nonDamaging
        public gmCEntity target; // m_target
        public uint nonBasicSkillId; // m_nonBasicSkillID
        public CPlayer player; // m_player
        public gmCEntity attacker; // m_attacker

        public DamageDisplayInfo(AC2Reader data) {
            playerIsTarget = data.ReadBoolean();
            playerIsAttacker = data.ReadBoolean();
            harmful = data.ReadBoolean();
            attackerHealthPoints = data.ReadUInt32();
            attackResult = data.ReadUInt32();
            heal = data.ReadBoolean();
            targetHealthPoints = data.ReadUInt32();
            attackerIsPlayersPet = data.ReadBoolean();
            nonDamaging = data.ReadBoolean();
            data.ReadPkg<gmCEntity>(v => target = v);
            nonBasicSkillId = data.ReadUInt32();
            data.ReadPkg<CPlayer>(v => player = v);
            data.ReadPkg<gmCEntity>(v => attacker = v);
        }
    }
}
