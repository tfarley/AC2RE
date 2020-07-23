namespace AC2E.Def {

    public class RenderSurface {

        public DataId did; // m_DID
        public uint unk1; // m_pSurfaceBits?
        public uint width; // width
        public uint height; // height
        public PixelFormat pixelFormat; // pfDesc
        public uint size; // size
        public byte[] content;

        public RenderSurface(AC2Reader data) {
            did = data.ReadDataId();
            unk1 = data.ReadUInt32();
            width = data.ReadUInt32();
            height = data.ReadUInt32();
            pixelFormat = (PixelFormat)data.ReadUInt32();
            size = data.ReadUInt32();
            content = data.ReadBytes((int)size);
        }
    }
}
