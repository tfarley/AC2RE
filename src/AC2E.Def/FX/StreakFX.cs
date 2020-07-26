namespace AC2E.Def {

    public class StreakFX {

        public RGBAColor color; // m_cColor
        public Vector startPoint; // m_vStartPoint
        public Vector endPoint; // m_vEndPoint
        public DataId materialDid; // m_MaterialInstanceID
        public float lifespan; // m_fLifespan
        public float trailDuration; // m_fTrailDuration
        public float minSegmentTime; // m_fMinSegmentTime
        public float minSegmentDistance; // m_fMinSegmentDistance
        public float textureScale; // m_fTextureScale

        public StreakFX() {

        }

        public StreakFX(AC2Reader data) {
            color = data.ReadRGBAColor();
            startPoint = data.ReadVector();
            endPoint = data.ReadVector();
            materialDid = data.ReadDataId();
            lifespan = data.ReadSingle();
            trailDuration = data.ReadSingle();
            minSegmentTime = data.ReadSingle();
            minSegmentDistance = data.ReadSingle();
            textureScale = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(color);
            data.Write(startPoint);
            data.Write(endPoint);
            data.Write(materialDid);
            data.Write(lifespan);
            data.Write(trailDuration);
            data.Write(minSegmentTime);
            data.Write(minSegmentDistance);
            data.Write(textureScale);
        }
    }
}
