namespace AC2E.Def {

    public class MinMaxEffect : Effect {

        public float maxSpellcraft; // m_fMaxSpellcraft
        public float maxMod; // m_fMaxMod
        public float minMod; // m_fMinMod
        public float minSpellcraft; // m_fMinSpellcraft

        public MinMaxEffect(AC2Reader data) : base(data) {
            maxSpellcraft = data.ReadSingle();
            maxMod = data.ReadSingle();
            minMod = data.ReadSingle();
            minSpellcraft = data.ReadSingle();
        }
    }
}
