namespace AC2RE.Definitions;

public class FellowshipControl : IHeapObject {

    public PackageType packageType => PackageType.FellowshipControl;

    public uint pad1;
    public uint pad2;
    public uint pad3;
    public uint pad4;
    public DataId fellowMarkerDid; // DIDFellowMarker_FellowGlobals
    public uint pad5;
    public uint pad6;
    public uint pad7;
    public uint pad8;
    public uint pad9;
    public uint pad10;
    public uint pad11;
    public uint pad12;
    public uint pad13;

    public FellowshipControl(AC2Reader data) {
        pad1 = data.ReadUInt32();
        pad2 = data.ReadUInt32();
        pad3 = data.ReadUInt32();
        pad4 = data.ReadUInt32();
        fellowMarkerDid = data.ReadDataId();
        pad5 = data.ReadUInt32();
        pad6 = data.ReadUInt32();
        pad7 = data.ReadUInt32();
        pad8 = data.ReadUInt32();
        pad9 = data.ReadUInt32();
        pad10 = data.ReadUInt32();
        pad11 = data.ReadUInt32();
        pad12 = data.ReadUInt32();
        pad13 = data.ReadUInt32();
    }
}
