using System.Collections.Generic;

namespace AC2E.Def {

    public class RenderTexture {

        // Const TEXTURETYPE_*
        public enum TextureType : uint {
            UNDEFINED = 1,
            TWO_D = 2,
            THREE_D = 3,
            CUBE = 4,
        }

        public DataId did; // m_DID
        public TextureType textureType; // m_SourceTextureType
        public List<DataId> levelDids; // m_SourceLevels

        public RenderTexture(AC2Reader data) {
            did = data.ReadDataId();
            textureType = (TextureType)data.ReadUInt32();
            levelDids = data.ReadList(data.ReadDataId);
        }
    }
}
