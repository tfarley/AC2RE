namespace AC2RE.Definitions;

public class EchoResponseHeader {

    // CEchoResponseHeader
    public float localTime; // m_LocalTimeConstructed
    public float holdingTime; // m_LocalTimeConstructed

    public EchoResponseHeader() {

    }

    public EchoResponseHeader(AC2Reader data) {
        localTime = data.ReadSingle();
        holdingTime = data.ReadSingle();
    }

    public void write(AC2Writer data) {
        data.Write(localTime);
        data.Write(holdingTime);
    }
}
