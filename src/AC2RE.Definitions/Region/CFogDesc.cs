using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CFogDesc {

    // Enum FogCombine::FogCombineType
    public enum FogCombineType : uint {
        MULTIPLY_COLORS, // MULTIPLY_COLORS
        ADD_COLORS, // ADD_COLORS
        USE_FIRST_COLOR, // USE_FIRST_COLOR
        USE_SECOND_COLOR, // USE_SECOND_COLOR
        USE_MAXIMUM_COLOR, // USE_MAXIMUM_COLOR
        USE_MINIMUM_COLOR, // USE_MINIMUM_COLOR
    }

    public class FogCombine {

        // FogCombine
        public FogCombineType combineType; // combine_type
        public float colorMultFirst; // color_mult_first
        public float colorMultSecond; // color_mult_second

        public FogCombine(FogCombineType combineType, AC2Reader data) {
            this.combineType = combineType;
            if (combineType == FogCombineType.ADD_COLORS) {
                colorMultFirst = data.ReadSingle();
                colorMultSecond = data.ReadSingle();
            }
        }
    }

    public class FogInfo {

        // FogInfo
        public float fogMin; // fog_min
        public float fogMax; // fog_max
        public float fogSky; // fog_sky
        public float fogDensity; // fog_density
        public LScapeFogType fogType; // fog_type
        public RGBAColor fogColor; // fog_color

        public FogInfo(AC2Reader data) {
            fogMin = data.ReadSingle();
            fogMax = data.ReadSingle();
            fogSky = data.ReadSingle();
            fogDensity = data.ReadSingle();
            fogType = data.ReadEnum<LScapeFogType>();
            fogColor = data.ReadRGBAColor();
        }
    }

    public class LightInfo {

        // LightInfo
        public float ambientBrightness; // amb_bright
        public RGBAColor ambientColor; // amb_color
        public float directionalBrightness; // dir_bright
        public RGBAColor directionalColor; // dir_color
        public RGBAColor shadowColor; // shadow_color

        public LightInfo(AC2Reader data) {
            directionalBrightness = data.ReadSingle();
            directionalColor = data.ReadRGBAColor();
            ambientBrightness = data.ReadSingle();
            ambientColor = data.ReadRGBAColor();
        }
    }

    public class LightFogInfo {

        // LightFogInfo
        public FogCombine combineAmbientBrightness; // m_combineAmbientBrightness
        public FogCombine combineAmbientColor; // m_combineAmbientColor
        public FogCombine combineDirectBrightness; // m_combineDirectBrightness
        public FogCombine combineDirectColor; // m_combineDirectColor
        public FogCombine combineDayLight; // m_combineDayLight
        public FogCombine combineDayFog; // m_combineDayFog
        public FogCombine combineDistanceFog; // m_combineDistanceFog
        public FogInfo fogInfo; // m_fogInfo
        public LightInfo lightInfo; // m_lightInfo

        public LightFogInfo(AC2Reader data) {
            fogInfo = new(data);
            lightInfo = new(data);
            uint packedCombineTypes = data.ReadUInt32();
            combineAmbientBrightness = new((FogCombineType)(packedCombineTypes & 0b111), data);
            packedCombineTypes >>= 3;
            combineAmbientColor = new((FogCombineType)(packedCombineTypes & 0b111), data);
            packedCombineTypes >>= 3;
            combineDirectBrightness = new((FogCombineType)(packedCombineTypes & 0b111), data);
            packedCombineTypes >>= 3;
            combineDirectColor = new((FogCombineType)(packedCombineTypes & 0b111), data);
            packedCombineTypes >>= 3;
            combineDayLight = new((FogCombineType)(packedCombineTypes & 0b111), data);
            packedCombineTypes >>= 3;
            combineDayFog = new((FogCombineType)(packedCombineTypes & 0b111), data);
            packedCombineTypes >>= 3;
            combineDistanceFog = new((FogCombineType)(packedCombineTypes & 0b111), data);
        }
    }

    public class FogType {

        // FogType
        public uint index; // index
        public LightFogInfo fogLighting; // fog_lighting

        public FogType(AC2Reader data) {
            index = data.ReadUInt32();
            fogLighting = new(data);
        }
    }

    // CFogDesc
    public DataId did; // m_DID
    public uint version; // version
    public DataId distanceFogMapDid; // distance_fog_map
    public DataId volumeFogMapDid; // volume_fog_map
    public DataId bottomFogMapDid; // bottom_fog_map
    public DataId topFogMapDid; // top_fog_map
    public FogType defaultFog; // default_fog
    public List<FogType> fogTypes; // fog_types

    public CFogDesc(AC2Reader data) {
        version = data.ReadUInt32();
        did = data.ReadDataId();
        distanceFogMapDid = data.ReadDataId();
        volumeFogMapDid = data.ReadDataId();
        bottomFogMapDid = data.ReadDataId();
        topFogMapDid = data.ReadDataId();
        bool hasDefaultFog = data.ReadBoolean();
        if (hasDefaultFog) {
            defaultFog = new(data);
        }
        fogTypes = data.ReadList(() => new FogType(data));
    }
}
