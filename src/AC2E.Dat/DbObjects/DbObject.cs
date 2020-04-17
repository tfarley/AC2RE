using System.IO;

namespace AC2E.Dat.DbObjects {

    public abstract class DbObject {

        public uint did;

        public DbObject(BinaryReader data) {
            did = data.ReadUInt32();
        }
    }
}
