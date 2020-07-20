namespace AC2E.Def {

    public class WLib : DbObj {

        public ByteStream byteStream; // m_bstream

        public WLib(AC2Reader data) : base(data) {
            byteStream = new ByteStream(data);
        }
    }
}
