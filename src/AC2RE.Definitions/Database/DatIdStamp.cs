namespace AC2RE.Definitions;

public class DatIdStamp {

    public GUID majorVersion; // _maj_vnum
    public uint minorVersion; // _min_vnum

    public DatIdStamp(AC2Reader data) {
        majorVersion = new(data);
        minorVersion = data.ReadUInt32();
    }
}
