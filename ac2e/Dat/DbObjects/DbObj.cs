
using System.IO;

public class DbObj {

    public uint did;

    public DbObj(BinaryReader data) {
        did = data.ReadUInt32();
    }
}
