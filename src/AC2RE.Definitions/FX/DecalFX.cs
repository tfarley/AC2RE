using System.Numerics;

namespace AC2RE.Definitions;

public class DecalFX {

    // DecalFX
    public DataId materialDid; // m_MaterialDID
    public Vector3 origin; // m_vOrigin
    public float lifetime; // m_fLifespan
    public RGBAColor color; // m_cColor
    public Waveform size; // m_wSize
    public Waveform rot; // m_wRotation
    public Waveform posRadius; // m_wPositionRadius

    public DecalFX() {

    }

    public DecalFX(AC2Reader data) {
        materialDid = data.ReadDataId();
        origin = data.ReadVector();
        lifetime = data.ReadSingle();
        color = data.ReadRGBAColor();
        size = new(data);
        rot = new(data);
        posRadius = new(data);
    }

    public void write(AC2Writer data) {
        data.Write(materialDid);
        data.Write(origin);
        data.Write(lifetime);
        data.Write(color);
        size.write(data);
        rot.write(data);
        posRadius.write(data);
    }
}
