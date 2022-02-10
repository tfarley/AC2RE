using System.Numerics;

namespace AC2RE.Definitions;

public class VisualDescInfo : IHeapObject {

    public PackageType packageType => PackageType.VisualDescInfo;

    public Vector3 scale; // m_scale
    public AppInfoHash appInfoHash; // m_appInfoHash
    public VisualDesc cachedVisualDesc; // m_cachedVisualDesc

    public VisualDescInfo() {

    }

    public VisualDescInfo(AC2Reader data) {
        data.ReadHO<VectorHeapObject>(v => scale = v.v);
        data.ReadHO<AppInfoHash>(v => appInfoHash = v);
        data.ReadHO<VisualDesc>(v => cachedVisualDesc = v);
    }

    public void write(AC2Writer data) {
        data.WriteHO(new VectorHeapObject(scale));
        data.WriteHO(appInfoHash);
        data.WriteHO(cachedVisualDesc);
    }
}
