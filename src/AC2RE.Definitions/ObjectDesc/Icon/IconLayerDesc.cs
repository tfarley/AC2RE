namespace AC2RE.Definitions;

public class IconLayerDesc {

    public IconLayer id; // m_iconLayerID
    public DataId imageDid; // m_imageDID
    public RGBAColor shiftColor; // m_shiftColor

    public IconLayerDesc(AC2Reader data) {
        id = data.ReadEnum<IconLayer>();
        imageDid = data.ReadDataId();
        shiftColor = data.ReadRGBAColor();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(id);
        data.Write(imageDid);
        data.Write(shiftColor);
    }
}
