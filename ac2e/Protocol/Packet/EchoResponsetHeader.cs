using System.IO;

public class EchoResponseHeader {

    public float localTime;
    public float localToServerTimeDelta;

    public void write(BinaryWriter data) {
        data.Write(localTime);
        data.Write(localToServerTimeDelta);
    }
}
