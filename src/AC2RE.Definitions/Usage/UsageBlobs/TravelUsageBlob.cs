namespace AC2RE.Definitions;

public class TravelUsageBlob : UsageBlob {

    public override PackageType packageType => PackageType.TravelUsageBlob;

    public IHeapObject travelRec; // m_travelRec
    public uint scene; // m_siScene // TODO: SceneId?

    public TravelUsageBlob() : base() {

    }

    public TravelUsageBlob(AC2Reader data) : base(data) {
        data.ReadHO<IHeapObject>(v => travelRec = v); // TODO: TravelRecord
        scene = data.ReadUInt32();
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.WriteHO(travelRec);
        data.Write(scene);
    }
}
