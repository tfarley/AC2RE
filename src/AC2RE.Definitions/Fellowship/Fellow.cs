namespace AC2RE.Definitions;

public class Fellow : IHeapObject {

    public PackageType packageType => PackageType.Fellow;

    public double joinTime; // m_join_ts
    public uint level; // m_level
    public FellowVitals vitals; // m_vitals
    public StringInfo name; // m_name

    public Fellow() {

    }

    public Fellow(AC2Reader data) {
        joinTime = data.ReadDouble();
        level = data.ReadUInt32();
        data.ReadHO<FellowVitals>(v => vitals = v);
        data.ReadHO<StringInfo>(v => name = v);
    }

    public void write(AC2Writer data) {
        data.Write(joinTime);
        data.Write(level);
        data.WriteHO(vitals);
        data.WriteHO(name);
    }
}
