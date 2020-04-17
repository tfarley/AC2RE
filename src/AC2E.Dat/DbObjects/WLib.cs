using AC2E.Interp;
using System.IO;

namespace AC2E.Dat.DbObjects {

    public class WLib : DbObject {

        public ByteStream byteStream;

        public WLib(BinaryReader data) : base(data) {
            byteStream = new ByteStream(data);
        }
    }
}
