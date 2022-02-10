using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RenderTexture {

    // Const TEXTURETYPE_*
    public enum TextureType : uint {
        UNDEF = 0,
        UNDEFINED = 1, // TEXTURETYPE_UNDEFINED
        TWO_D = 2, // TEXTURETYPE_2D
        THREE_D = 3, // TEXTURETYPE_3D
        CUBE = 4, // TEXTURETYPE_CUBE
    }

    // RenderTexture
    public DataId did; // m_DID
    public TextureType textureType; // m_SourceTextureType
    public List<DataId> levelSurfaceDids; // m_SourceLevels

    public RenderTexture(AC2Reader data) {
        did = data.ReadDataId();
        textureType = (TextureType)data.ReadUInt32();
        levelSurfaceDids = data.ReadList(data.ReadDataId);
    }
}
