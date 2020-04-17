using AC2E.Utils.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Dat.DbObjects {

    public class EnumMapper : DbObject {

        public uint baseEnumMapperDid;
        public Dictionary<uint, string> idToString;

        public EnumMapper(BinaryReader data) : base(data) {
            baseEnumMapperDid = data.ReadUInt32();
            idToString = data.ReadDictionary(data => data.ReadUInt32(), data => data.ReadEncryptedString());
        }
    }
}
