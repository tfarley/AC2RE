using System.IO;

namespace AC2E.Protocol.NetBlob {

    public class NetBlobFrag {

        public static readonly int MAX_SIZE = 464;

        public NetBlobId blobId;
        public ushort fragCount;
        public ushort fragSize;
        public ushort fragIndex;
        public NetQueue queueId;
        public byte[] payload;

        public NetBlobFrag() {

        }

        public NetBlobFrag(BinaryReader data) {
            blobId = data.ReadUInt64();
            fragCount = data.ReadUInt16();
            fragSize = data.ReadUInt16();
            fragIndex = data.ReadUInt16();
            queueId = (NetQueue)data.ReadUInt16();

            payload = data.ReadBytes(fragSize - 16);
        }

        public void writeHeader(BinaryWriter data) {
            fragSize = (ushort)(payload.Length + 16);

            data.Write(blobId.id);
            data.Write(fragCount);
            data.Write(fragSize);
            data.Write(fragIndex);
            data.Write((ushort)queueId);
        }

        public void writePayload(BinaryWriter data) {
            data.Write(payload);
        }

        public override string ToString() {
            return $"b {blobId} c {fragCount} l {fragSize} i {fragIndex} q {queueId}";
        }
    }
}
