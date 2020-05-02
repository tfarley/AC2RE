using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using System.IO;

namespace AC2E.Dat.DbObjects {

    public abstract class DbObj {

        public DataId did; // m_DID

        public DbObj(BinaryReader data) {
            did = data.ReadDataId();
        }
    }
}
