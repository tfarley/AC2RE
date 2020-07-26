using System.Collections.Generic;

namespace AC2E.Def {

    public class MaterialModifier {

        // Enum RMFieldType
        public enum RMFieldType : uint {
            AMBIENT,
            DIFFUSE,
            SPECULAR,
            SPECULARPOWER,
            xx_RESERVED_xx,
            CULLMODE,
            DEPTHTEST,
            DEPTHWRITE,
            ALPHATEST,
            ALPHATESTREF,
            STAGE_TEXTURE,
            STAGE_ADDRESSMODEU,
            STAGE_ADDRESSMODEV,
            MOD_ORIGIN_XTRANSLATE,
            MOD_ORIGIN_YTRANSLATE,
            MOD_ORIGIN_ZTRANSLATE,
            MOD_ORIGIN_XSCALE,
            MOD_ORIGIN_YSCALE,
            MOD_ORIGIN_ZSCALE,
            MOD_ORIGIN_ORIGINPHASE,
            MOD_ORIGIN_NORMALPHASE,
            MOD_NORMAL_XTRANSLATE,
            MOD_NORMAL_YTRANSLATE,
            MOD_NORMAL_ZTRANSLATE,
            MOD_NORMAL_XSCALE,
            MOD_NORMAL_YSCALE,
            MOD_NORMAL_ZSCALE,
            MOD_NORMAL_ORIGINPHASE,
            MOD_NORMAL_NORMALPHASE,
            MOD_DIFFUSE_R,
            MOD_DIFFUSE_G,
            MOD_DIFFUSE_B,
            MOD_DIFFUSE_A,
            MOD_UVTRANSLATE_UTRANSLATE,
            MOD_UVTRANSLATE_VTRANSLATE,
            MOD_UVROTATE_ROTATE,
            MOD_UVSCALE_USCALE,
            MOD_UVSCALE_VSCALE,
        }

        // Enum RMDataType
        public enum RMDataType : uint {
            WAVEFORM = 1000,
            COLOR = 2000,
            TEXTURE = 3000,
            BOOL = 4000,
            INVALID = 0x7FFFFFFF,
        }

        public class MaterialField {

            public RMFieldType fieldType; // fieldType
            public RMDataType dataType; // dataType
            public uint layerIndex; // layerIndex
            public uint indices; // tcIndex, stageIndex, modifierIndex

            public MaterialField(AC2Reader data) {
                fieldType = (RMFieldType)data.ReadUInt32();
                dataType = (RMDataType)data.ReadUInt32();
                layerIndex = data.ReadUInt32();
                indices = data.ReadUInt32();
            }
        }

        public class MaterialProperty {

            public uint nameId; // nameID
            public RMDataType dataType; // dataType
            public byte[] dataBytes; // data
            public List<MaterialField> fields; // fields

            public MaterialProperty(AC2Reader data) {
                nameId = data.ReadUInt32();
                dataType = (RMDataType)data.ReadUInt32();
                uint dataLength = data.ReadUInt32();
                dataBytes = data.ReadBytes((int)dataLength);
                fields = data.ReadList(() => new MaterialField(data));
            }
        }

        public DataId did; // m_DID
        public List<MaterialProperty> properties; // properties

        public MaterialModifier(AC2Reader data) {
            did = data.ReadDataId();
            properties = data.ReadList(() => new MaterialProperty(data));
        }
    }
}
