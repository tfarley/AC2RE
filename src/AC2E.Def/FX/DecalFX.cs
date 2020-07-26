namespace AC2E.Def {

    public class DecalFX {

        public DataId materialDid; // m_MaterialDID
        public Vector origin; // m_vOrigin
        public float lifespan; // m_fLifespan
        public RGBAColor color; // m_cColor
        public Waveform size; // m_wSize
        public Waveform rotation; // m_wRotation
        public Waveform positionRadius; // m_wPositionRadius

        public DecalFX() {

        }

        public DecalFX(AC2Reader data) {
            materialDid = data.ReadDataId();
            origin = data.ReadVector();
            lifespan = data.ReadSingle();
            color = data.ReadRGBAColor();
            size = new Waveform(data);
            rotation = new Waveform(data);
            positionRadius = new Waveform(data);
        }

        public void write(AC2Writer data) {
            data.Write(materialDid);
            data.Write(origin);
            data.Write(lifespan);
            data.Write(color);
            size.write(data);
            rotation.write(data);
            positionRadius.write(data);
        }
    }
}
