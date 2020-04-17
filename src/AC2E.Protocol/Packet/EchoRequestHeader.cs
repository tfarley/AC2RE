using System.IO;

namespace AC2E.Protocol.Packet {

    public class EchoRequestHeader {

        public float localTime;

        public EchoRequestHeader(BinaryReader data) {
            localTime = data.ReadSingle();
        }
    }
}
