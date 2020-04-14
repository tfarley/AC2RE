using System.Collections.Generic;
using System.IO;

public class EnumMapper : DbObj {

    public uint baseEnumMapperDid;
    public Dictionary<uint, string> idToString;

    public EnumMapper(BinaryReader data) : base(data) {
        baseEnumMapperDid = data.ReadUInt32();
        idToString = data.ReadDictionary(data => data.ReadUInt32(), data => data.ReadEncryptedString());
    }
}
