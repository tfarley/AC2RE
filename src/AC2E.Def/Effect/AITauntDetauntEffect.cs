namespace AC2E.Def {

    public class AITauntDetauntEffect : InstantEffect {

        public override PackageType packageType => PackageType.AITauntDetauntEffect;

        public int minSpellcraft; // m_nMinSpellcraft
        public int maxSpellcraft; // m_nMaxSpellcraft
        public uint flags; // m_flags
        public float minFractionalMod; // m_fMinFractionalMod
        public float maxFractionalMod; // m_fMaxFractionalMod
        public int maxRawMod; // m_nMaxRawMod
        public int minRawMod; // m_nMinRawMod

        public AITauntDetauntEffect(AC2Reader data) : base(data) {
            minSpellcraft = data.ReadInt32();
            maxSpellcraft = data.ReadInt32();
            flags = data.ReadUInt32();
            minFractionalMod = data.ReadSingle();
            maxFractionalMod = data.ReadSingle();
            maxRawMod = data.ReadInt32();
            minRawMod = data.ReadInt32();
        }
    }
}
