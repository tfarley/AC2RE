using System.IO;

public class WLib : DbObj {

    public ByteStream byteStream;

    public WLib(BinaryReader data) : base(data) {
        byteStream = new ByteStream(data);
    }
}
