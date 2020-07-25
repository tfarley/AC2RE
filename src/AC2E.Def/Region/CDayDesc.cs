using System.Collections.Generic;

namespace AC2E.Def {

    public class CDayDesc {

        public class LensFlarePoint {

            public float axisPos; // m_fAxisPos
            public DataId textureDid; // m_TextureID
            public float size; // m_fSize

            public LensFlarePoint(AC2Reader data) {
                axisPos = data.ReadSingle();
                textureDid = data.ReadDataId();
                size = data.ReadSingle();
            }
        }

        public class LensFlareEffect {

            public float fadeSpeed; // m_fFadeSpeed
            public List<LensFlarePoint> pointList; // m_PointList
            public float opacity; // m_fOpacity
            public double lastDrawTime; // m_dLastDrawTime
            public uint pixelScanID; // m_nPixelScanID
            public uint pixelScanFrameStamp; // m_nPixelScanFrameStamp

            public LensFlareEffect(AC2Reader data) {
                fadeSpeed = data.ReadSingle();
                pointList = data.ReadList(() => new LensFlarePoint(data));
            }
        }

        public class SkyObject {

            public uint objName; // obj_name
            public uint defaultGfx; // default_gfx
            public float beginTime; // begin_time
            public float endTime; // end_time
            public float beginPitch; // begin_pitch
            public float endPitch; // end_pitch
            public float beginHeading; // begin_heading
            public float endHeading; // end_heading
            public float heightMod; // height_mod
            public bool westToEast; // west_to_east
            public bool after; // after
            public bool fixedPos; // fixed_pos
            public float fixedX; // fixed_x
            public float fixedY; // fixed_y
            public bool waterEffect; // water_effect
            public LensFlareEffect lensFlareEffect; // m_LensFlareEffect

            public SkyObject(AC2Reader data) {
                objName = data.ReadUInt32();
                defaultGfx = data.ReadUInt32();
                beginTime = data.ReadSingle();
                endTime = data.ReadSingle();
                beginPitch = data.ReadSingle();
                endPitch = data.ReadSingle();
                beginHeading = data.ReadSingle();
                endHeading = data.ReadSingle();
                heightMod = data.ReadSingle();
                after = data.ReadBoolean();
                fixedPos = data.ReadBoolean();
                waterEffect = data.ReadBoolean();
                fixedX = data.ReadSingle();
                fixedY = data.ReadSingle();
                westToEast = data.ReadBoolean();
                lensFlareEffect = new LensFlareEffect(data);
            }
        }

        public class SkyObjectReplace {

            public uint objName; // obj_name
            public uint replaceGfx; // replace_gfx
            public float headingOffset; // heading_offset
            public float pitchOffset; // pitch_offset
            public RGBAColor color; // color

            public SkyObjectReplace(AC2Reader data) {
                objName = data.ReadUInt32();
                replaceGfx = data.ReadUInt32();
                pitchOffset = data.ReadSingle();
                headingOffset = data.ReadSingle();
                color = data.ReadRGBAColor();
            }
        }

        public class SkyTimeOfDay {

            public float beginTime; // m_nBeginTime
            public RGBAColor fogColor; // m_fogColor
            public float fogMinDist; // m_nFogMinDist
            public float fogMaxDist; // m_nFogMaxDist
            public float fogSky; // m_nFogSky
            public RGBAColor directColor; // m_directColor
            public float directHeading; // m_nDirectHeading
            public float directPitch; // m_nDirectPitch
            public float directBright; // m_nDirectBright
            public RGBAColor ambientColor; // m_ambientColor
            public float ambientBright; // m_nAmbientBright
            public RGBAColor shadowColor; // m_shadowColor
            public float rainAmount; // m_nRainAmount
            public float rainWindX; // m_nRainWindX
            public float rainWindY; // m_nRainWindY
            public uint setMask; // m_set_mask
            public List<SkyObjectReplace> replacements; // m_replacements

            public SkyTimeOfDay(AC2Reader data) {
                beginTime = data.ReadSingle();
                fogMinDist = data.ReadSingle();
                fogMaxDist = data.ReadSingle();
                fogSky = data.ReadSingle();
                fogColor = data.ReadRGBAColor();
                directBright = data.ReadSingle();
                directHeading = data.ReadSingle();
                directPitch = data.ReadSingle();
                directColor = data.ReadRGBAColor();
                ambientBright = data.ReadSingle();
                ambientColor = data.ReadRGBAColor();
                shadowColor = data.ReadRGBAColor();
                replacements = data.ReadList(() => new SkyObjectReplace(data));
                rainAmount = data.ReadSingle();
                rainWindX = data.ReadSingle();
                rainWindY = data.ReadSingle();
            }
        }

        public DataId did; // m_DID
        public uint version; // version
        public List<SkyTimeOfDay> hours; // hours
        public List<SkyObject> objects; // objects

        public CDayDesc(AC2Reader data) {
            version = data.ReadUInt32();
            did = data.ReadDataId();
            hours = data.ReadList(() => new SkyTimeOfDay(data));
            objects = data.ReadList(() => new SkyObject(data));
        }
    }
}
