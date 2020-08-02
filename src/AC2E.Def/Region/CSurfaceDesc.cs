using System.Collections.Generic;

namespace AC2E.Def {

    public class CSurfaceDesc {

        public class LScapeVertexColorDesc {

            public RGBAColor farVertexColor; // far_vertex_color
            public RGBAColor vertexColor; // vertex_color
            public float minHue; // min_hue
            public float maxHue; // max_hue
            public uint huePower; // hue_power
            public float minSaturate; // min_saturate
            public float maxSaturate; // max_saturate
            public uint saturatePower; // saturate_power
            public float minBrightness; // min_bright
            public float maxBrightness; // max_bright
            public uint brightnessPower; // bright_power
            public float frequency; // frequency

            public LScapeVertexColorDesc(AC2Reader data) {
                farVertexColor = data.ReadRGBAColor();
                vertexColor = data.ReadRGBAColor();
                minHue = data.ReadSingle();
                maxHue = data.ReadSingle();
                huePower = data.ReadUInt32();
                minSaturate = data.ReadSingle();
                maxSaturate = data.ReadSingle();
                saturatePower = data.ReadUInt32();
                minBrightness = data.ReadSingle();
                maxBrightness = data.ReadSingle();
                brightnessPower = data.ReadUInt32();
                frequency = data.ReadSingle();
            }
        }

        public class LScapeMaterialDesc {

            public DataId materialDid; // material_id
            public float frequency; // frequency
            public float tileWidth; // tile_width

            public LScapeMaterialDesc(AC2Reader data) {
                materialDid = data.ReadDataId();
                frequency = data.ReadSingle();
                tileWidth = data.ReadSingle();
            }
        }

        public class LScapeAlphaDesc {

            public DataId textureDid; // texture_id
            public float frequency; // frequency
            public float tileWidth; // tile_width

            public LScapeAlphaDesc(AC2Reader data) {
                textureDid = data.ReadDataId();
                frequency = data.ReadSingle();
                tileWidth = data.ReadSingle();
            }
        }

        public class MaterialGroup {

            public float minPitch; // min_pitch
            public float maxPitch; // max_pitch
            public float colorDistance; // color_distance
            public bool colorInterp; // color_interp
            public bool autoEdge; // auto_edge
            public DataId physicsMatDid; // physics_mat_id
            public List<LScapeVertexColorDesc> vertexColor; // vertex_color
            public float vcolorFreqCount; // vcolor_freq_count
            public List<LScapeMaterialDesc> baseMaterials; // base_materials
            public float baseFreqCount; // base_freq_count
            public List<LScapeAlphaDesc> cornerAlphaTextures; // corner_alpha_textures
            public float cornerFreqCount; // corner_freq_count
            public List<LScapeAlphaDesc> sideAlphaTextures; // side_alpha_textures
            public float sideFreqCount; // side_freq_count
            public List<LScapeAlphaDesc> threeCornerAlphaTextures; // three_corner_alpha_textures
            public float threeCornerFreqCount; // three_corner_freq_count
            public List<LScapeAlphaDesc> diagonalAlphaTextures; // diagonal_alpha_textures
            public float diagonalFreqCount; // diagonal_freq_count
            public List<LScapeMaterialDesc> detailMaterials; // detail_materials
            public float detailFreqCount; // detail_freq_count
            public List<LScapeMaterialDesc> bumpMaterials; // bump_materials
            public float bumpFreqCount; // bump_freq_count

            public MaterialGroup(AC2Reader data) {
                minPitch = data.ReadSingle();
                maxPitch = data.ReadSingle();
                colorDistance = data.ReadSingle();
                colorInterp = data.ReadBoolean();
                autoEdge = data.ReadBoolean();
                physicsMatDid = data.ReadDataId();
                vertexColor = data.ReadList(() => new LScapeVertexColorDesc(data));
                vcolorFreqCount = data.ReadSingle();
                baseMaterials = data.ReadList(() => new LScapeMaterialDesc(data));
                baseFreqCount = data.ReadSingle();
                cornerAlphaTextures = data.ReadList(() => new LScapeAlphaDesc(data));
                cornerFreqCount = data.ReadSingle();
                threeCornerAlphaTextures = data.ReadList(() => new LScapeAlphaDesc(data));
                threeCornerFreqCount = data.ReadSingle();
                diagonalAlphaTextures = data.ReadList(() => new LScapeAlphaDesc(data));
                diagonalFreqCount = data.ReadSingle();
                sideAlphaTextures = data.ReadList(() => new LScapeAlphaDesc(data));
                sideFreqCount = data.ReadSingle();
                detailMaterials = data.ReadList(() => new LScapeMaterialDesc(data));
                detailFreqCount = data.ReadSingle();
                bumpMaterials = data.ReadList(() => new LScapeMaterialDesc(data));
                bumpFreqCount = data.ReadSingle();
            }
        }

        public class LScapeSurfaceType {

            public uint surfIndex; // surf_index
            public bool emptyRoad; // empty_road
            public List<MaterialGroup> terrainMaterials; // terrain_materials

            public LScapeSurfaceType(AC2Reader data) {
                surfIndex = data.ReadUInt32();
                terrainMaterials = data.ReadList(() => new MaterialGroup(data));
                emptyRoad = data.ReadBoolean();
            }
        }

        public DataId did; // m_DID
        public uint version; // version
        public DataId farTextureDid; // m_FarTextureID
        public float farTextureScale; // m_fFarTextureScale
        public DataId farWaterMaterialDid; // m_FarWaterMaterialID
        public DataId cloudTextureDid; // m_CloudTextureID
        public float cloudTextureScale; // m_fCloudTextureScale
        public float cloudScrollU; // m_fCloudScrollU
        public float cloudScrollV; // m_fCloudScrollV
        public List<LScapeSurfaceType> surfaces; // surfaces
        public List<LScapeSurfaceType> roads; // roads

        public CSurfaceDesc(AC2Reader data) {
            version = data.ReadUInt32();
            did = data.ReadDataId();
            farTextureDid = data.ReadDataId();
            farTextureScale = data.ReadSingle();
            farWaterMaterialDid = data.ReadDataId();
            cloudTextureDid = data.ReadDataId();
            cloudTextureScale = data.ReadSingle();
            cloudScrollU = data.ReadSingle();
            cloudScrollV = data.ReadSingle();
            surfaces = data.ReadList(() => new LScapeSurfaceType(data));
            roads = data.ReadList(() => new LScapeSurfaceType(data));
        }
    }
}
