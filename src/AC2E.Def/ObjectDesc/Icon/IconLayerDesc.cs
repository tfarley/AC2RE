namespace AC2E.Def {

    public class IconLayerDesc {

        public uint id; // m_iconLayerID
        public DataId imageDid; // m_imageDID
        public RGBAColor shiftColor; // m_shiftColor

        public IconLayerDesc(AC2Reader data) {
            id = data.ReadUInt32();
            imageDid = data.ReadDataId();
            shiftColor = data.ReadRGBAColor();
        }

        public void write(AC2Writer data) {
            data.Write(id);
            data.Write(imageDid);
            data.Write(shiftColor);
        }
    }
}
