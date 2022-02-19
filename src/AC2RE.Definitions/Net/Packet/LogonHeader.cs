namespace AC2RE.Definitions;

public class LogonHeader {

    // CLogonHeader::HandshakeWireData
    public string clientVersion; // ClientVersion
    public uint length; // cbAuthData
    public NetAuthenticator netAuth; // cbAuthData

    public LogonHeader(AC2Reader data) {
        clientVersion = data.ReadString();
        length = data.ReadUInt32();
        netAuth = new(data);
    }
}
