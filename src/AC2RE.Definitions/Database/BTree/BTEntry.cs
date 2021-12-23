namespace AC2RE.Definitions;

public class BTEntry {

    public static readonly int SIZE = sizeof(uint) * 4;

    public DataId did; // GID_
    public uint offset; // Offset_
    public int size; // size_
    public uint date; // date_

    public BTEntry(AC2Reader data) {
        did = data.ReadDataId();
        offset = data.ReadUInt32();
        size = data.ReadInt32();
        date = data.ReadUInt32();
    }
}
