using System.IO;

public class EchoRequestHeader {

    public float localTime;

    public EchoRequestHeader(BinaryReader data) {
        localTime = data.ReadSingle();
    }
}
