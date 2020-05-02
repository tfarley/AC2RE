using System.IO;

namespace AC2E.Protocol.Packet {

    public class EchoRequestHeader {

        // CEchoRequestHeader
        public float localTime; // m_LocalTime

        public EchoRequestHeader(BinaryReader data) {
            localTime = data.ReadSingle();
        }
    }
}
