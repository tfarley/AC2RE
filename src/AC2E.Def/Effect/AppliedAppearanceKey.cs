namespace AC2E.Def {

    public class AppliedAppearanceKey {

        public uint key; // m_key
        public float scalarMod; // m_scalar_mod

        public AppliedAppearanceKey() {

        }

        public AppliedAppearanceKey(AC2Reader data) {
            key = data.ReadUInt32();
            scalarMod = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(key);
            data.Write(scalarMod);
        }
    }
}
