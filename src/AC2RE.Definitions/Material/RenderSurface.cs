namespace AC2RE.Definitions {

    public class RenderSurface {

        public DataId did; // m_DID
        public uint unk1;
        public uint width; // sourceData.width
        public uint height; // sourceData.height
        public uint imageSize; // sourceData.imageSize
        public byte[] sourceData; // sourceData.sourceBits
        public PixelFormat pixelFormat; // sourceData.pfDesc.format

        public RenderSurface(AC2Reader data) {
            did = data.ReadDataId();
            unk1 = data.ReadUInt32();
            width = data.ReadUInt32();
            height = data.ReadUInt32();
            pixelFormat = (PixelFormat)data.ReadUInt32();
            imageSize = data.ReadUInt32();
            sourceData = data.ReadBytes((int)imageSize);
        }
    }
}
