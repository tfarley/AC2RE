namespace AC2RE.Definitions {

    public class NetBlobFrag {

        public static readonly int MAX_SIZE = NetPacket.MAX_SIZE - 20 - 16; // Subtracts packet header size and fragment header size

        // BlobFragHeader_t
        public NetBlobId blobId; // blobID
        public ushort fragCount; // numFrags
        public ushort fragSize; // blobFragSize
        public ushort fragIndex; // blobNum
        public NetQueue queueId; // queueID
        public byte[] _payload;
        public byte[] payload {
            get => _payload;
            set {
                _payload = value;
                fragSize = (ushort)(payload.Length + 16);
            }
        }

        public NetBlobFrag() {

        }

        public NetBlobFrag(AC2Reader data) {
            blobId = new(data.ReadUInt64());
            fragCount = data.ReadUInt16();
            fragSize = data.ReadUInt16();
            fragIndex = data.ReadUInt16();
            queueId = (NetQueue)data.ReadUInt16();

            _payload = data.ReadBytes(fragSize - 16);
        }

        public void writeHeader(AC2Writer data) {
            data.Write(blobId.id);
            data.Write(fragCount);
            data.Write(fragSize);
            data.Write(fragIndex);
            data.Write((ushort)queueId);
        }

        public void writePayload(AC2Writer data) {
            data.Write(_payload);
        }

        public override string ToString() {
            return $"b {blobId} c {fragCount} l {fragSize} i {fragIndex} q {queueId}";
        }
    }
}
