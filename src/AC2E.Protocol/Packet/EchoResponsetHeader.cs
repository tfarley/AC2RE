using System.IO;

namespace AC2E.Protocol.Packet {

    public class EchoResponseHeader {

        // CEchoResponseHeader
        public float localTime; // m_LocalTimeConstructed
        public float localToServerTimeDelta; // m_LocalTimeConstructed

        public void write(BinaryWriter data) {
            data.Write(localTime);
            data.Write(localToServerTimeDelta);
        }
    }
}
