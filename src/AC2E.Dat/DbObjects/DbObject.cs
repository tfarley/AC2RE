using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using System.IO;

namespace AC2E.Dat.DbObjects {

    public abstract class DbObject {

        public DataId did;

        public DbObject(BinaryReader data) {
            did = data.ReadDataId();
        }
    }
}
