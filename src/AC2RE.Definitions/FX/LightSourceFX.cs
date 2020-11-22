namespace AC2RE.Definitions {

    public class LightSourceFX {

        public EntityDesc lightEntityDesc; // m_LightEntityDesc
        public float lifetime; // m_fLifespan
        public float fadeTime; // m_fFadeTime

        public LightSourceFX() {

        }

        public LightSourceFX(AC2Reader data) {
            lightEntityDesc = new(data);
            lifetime = data.ReadSingle();
            fadeTime = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            lightEntityDesc.write(data);
            data.Write(lifetime);
            data.Write(fadeTime);
        }
    }
}
