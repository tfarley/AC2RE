namespace AC2E.Def {

    public class LightSourceFX {

        public EntityDesc lightEntityDesc; // m_LightEntityDesc
        public float lifespan; // m_fLifespan
        public float fadeTime; // m_fFadeTime

        public LightSourceFX() {

        }

        public LightSourceFX(AC2Reader data) {
            lightEntityDesc = new EntityDesc(data);
            lifespan = data.ReadSingle();
            fadeTime = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            lightEntityDesc.write(data);
            data.Write(lifespan);
            data.Write(fadeTime);
        }
    }
}
