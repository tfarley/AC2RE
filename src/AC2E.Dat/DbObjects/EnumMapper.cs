using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Dat.DbObjects {

    public class EnumMapper : DbObject {

        public DataId baseEnumMapperDid;
        public Dictionary<uint, string> idToString;

        public EnumMapper(BinaryReader data) : base(data) {
            baseEnumMapperDid = data.ReadUInt32();
            idToString = data.ReadDictionary(data.ReadUInt32, () => data.ReadEncryptedString());
        }
    }
}
