using System.Numerics;

namespace AC2RE.Definitions;

public class StreakFX {

    // StreakFX
    public RGBAColor color; // m_cColor
    public Vector3 startPoint; // m_vStartPoint
    public Vector3 endPoint; // m_vEndPoint
    public DataId materialDid; // m_MaterialInstanceID
    public float lifetime; // m_fLifespan
    public float trailDuration; // m_fTrailDuration
    public float minSegmentTime; // m_fMinSegmentTime
    public float minSegmentDist; // m_fMinSegmentDistance
    public float textureScale; // m_fTextureScale

    public StreakFX() {

    }

    public StreakFX(AC2Reader data) {
        color = data.ReadRGBAColor();
        startPoint = data.ReadVector();
        endPoint = data.ReadVector();
        materialDid = data.ReadDataId();
        lifetime = data.ReadSingle();
        trailDuration = data.ReadSingle();
        minSegmentTime = data.ReadSingle();
        minSegmentDist = data.ReadSingle();
        textureScale = data.ReadSingle();
    }

    public void write(AC2Writer data) {
        data.Write(color);
        data.Write(startPoint);
        data.Write(endPoint);
        data.Write(materialDid);
        data.Write(lifetime);
        data.Write(trailDuration);
        data.Write(minSegmentTime);
        data.Write(minSegmentDist);
        data.Write(textureScale);
    }
}
