using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class IconDesc : IPackage {

        public NativeType nativeType => NativeType.ICONDESC;

        public class IconLayerDesc {

            public uint iconLayerId; // m_iconLayerID
            public DataId imageDid; // m_imageDID
            public RGBAColor shiftColor; // m_shiftColor

            public IconLayerDesc(BinaryReader data) {
                iconLayerId = data.ReadUInt32();
                imageDid = data.ReadDataId();
                shiftColor = data.ReadRGBAColor();
            }

            public void write(BinaryWriter data) {
                data.Write(iconLayerId);
                data.Write(imageDid);
                data.Write(shiftColor);
            }
        }

        public List<IconLayerDesc> layers; // m_layers

        public IconDesc(BinaryReader data) {
            layers = data.ReadList(() => new IconLayerDesc(data));
        }

        public void write(BinaryWriter data) {
            data.Write(layers, v => v.write(data));
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            write(data);
        }
    }
}
