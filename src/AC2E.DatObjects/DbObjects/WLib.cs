using AC2E.Interp;
using System.IO;

namespace AC2E.Dat {

    public class WLib : DbObj {

        public ByteStream byteStream; // m_bstream

        public WLib(BinaryReader data) : base(data) {
            byteStream = new ByteStream(data);
        }
    }
}
