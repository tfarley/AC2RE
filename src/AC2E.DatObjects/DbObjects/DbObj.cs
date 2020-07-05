using System.IO;

namespace AC2E.Dat {

    public abstract class DbObj {

        public DataId did; // m_DID

        public DbObj(BinaryReader data) {
            did = data.ReadDataId();
        }
    }
}
