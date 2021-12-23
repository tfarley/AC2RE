namespace AC2RE.Definitions;

public class AppliedAppearanceKey {

    public AppearanceKey key; // m_key
    public float scalarMod; // m_scalar_mod

    public AppliedAppearanceKey() {

    }

    public AppliedAppearanceKey(AC2Reader data) {
        key = (AppearanceKey)data.ReadUInt32();
        scalarMod = data.ReadSingle();
    }

    public void write(AC2Writer data) {
        data.Write((uint)key);
        data.Write(scalarMod);
    }
}
