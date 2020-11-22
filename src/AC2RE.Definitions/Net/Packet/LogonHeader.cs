namespace AC2RE.Definitions {

    public class LogonHeader {

        // CLogonHeader::HandshakeWireData
        public string clientVersion; // ClientVersion
        public uint length; // cbAuthData
        public NetAuthenticator netAuth; // cbAuthData

        public LogonHeader(AC2Reader data) {
            clientVersion = data.ReadString();
            length = data.ReadUInt32();

            long authDataStart = data.BaseStream.Position;

            netAuth = new(data);

            // TODO: Skip the rest - unknown data
            netAuth.extraData = data.ReadBytes((int)length - (int)(data.BaseStream.Position - authDataStart));
        }
    }
}
