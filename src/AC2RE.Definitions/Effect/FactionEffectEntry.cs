namespace AC2RE.Definitions;

public class FactionEffectEntry : IHeapObject {

    public PackageType packageType => PackageType.FactionEffectEntry;

    public SingletonPkg<Effect> effect; // m_eff
    public uint rating; // m_rating

    public FactionEffectEntry(AC2Reader data) {
        data.ReadHO<Effect>(v => effect = v);
        rating = data.ReadUInt32();
    }
}
