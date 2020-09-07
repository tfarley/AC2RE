using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Def {

    public class PEKeyframeDesc {

        public float time; // m_Time
        public uint keyFlags; // m_KeyFlags
        public float scaleX; // m_ScaleX
        public float scaleY; // m_ScaleY
        public RGBAColor color; // m_Color
        public float mass; // m_Mass
        public uint pcType; // m_PCType
        public uint keyframeFlags; // m_KeyframeFlags
        public WaveformVector3 pos; // m_wvPosition
        public List<Vector3> points; // m_aPoints

        public PEKeyframeDesc(AC2Reader data) {
            time = data.ReadSingle();
            keyFlags = data.ReadUInt32();
            scaleX = data.ReadSingle();
            scaleY = data.ReadSingle();
            color = data.ReadRGBAColorFull();
            mass = data.ReadSingle();
            pcType = data.ReadUInt32();
            keyframeFlags = data.ReadUInt32();
            pos = new WaveformVector3(data);
            points = data.ReadList(data.ReadVector);
        }
    }
}
