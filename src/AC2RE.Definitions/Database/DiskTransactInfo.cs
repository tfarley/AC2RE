namespace AC2RE.Definitions;

public class DiskTransactInfo {

    // DiskTransactInfo
    public uint type; // ulType_
    public uint[] ul = new uint[10]; // ul1_ - ul10_

    public DiskTransactInfo(AC2Reader data) {
        type = data.ReadUInt32();
        for (int i = 0; i < ul.Length; i++) {
            ul[i] = data.ReadUInt32();
        }
    }
}
