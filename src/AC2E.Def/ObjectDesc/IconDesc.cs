using System.Collections.Generic;

namespace AC2E.Def {

    public class IconDesc : IPackage {

        public NativeType nativeType => NativeType.ICONDESC;

        public class IconLayerDesc {

            public uint iconLayerId; // m_iconLayerID
            public DataId imageDid; // m_imageDID
            public RGBAColor shiftColor; // m_shiftColor

            public IconLayerDesc(AC2Reader data) {
                iconLayerId = data.ReadUInt32();
                imageDid = data.ReadDataId();
                shiftColor = data.ReadRGBAColor();
            }

            public void write(AC2Writer data) {
                data.Write(iconLayerId);
                data.Write(imageDid);
                data.Write(shiftColor);
            }
        }

        public List<IconLayerDesc> layers; // m_layers

        public IconDesc(AC2Reader data) {
            layers = data.ReadList(() => new IconLayerDesc(data));
        }

        public void write(AC2Writer data) {
            data.Write(layers, v => v.write(data));
        }
    }
}
