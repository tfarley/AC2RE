namespace AC2RE.Definitions;

public class ShardUsageBlob : UsageBlob {

    public override PackageType packageType => PackageType.ShardUsageBlob;

    public IHeapObject travelRec; // m_travelRec
    public uint scene; // m_siScene
    public uint sceneId; // m_sceneID // TODO: SceneId?

    public ShardUsageBlob() : base() {

    }

    public ShardUsageBlob(AC2Reader data) : base(data) {
        data.ReadHO<IHeapObject>(v => travelRec = v); // TODO: TravelRecord
        scene = data.ReadUInt32();
        sceneId = data.ReadUInt32();
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.WriteHO(travelRec);
        data.Write(scene);
        data.Write(sceneId);
    }
}
