using System.Collections.Generic;

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
        public WaveformVector3 position; // m_wvPosition
        public List<Vector> points; // m_aPoints

        public PEKeyframeDesc(AC2Reader data) {
            time = data.ReadSingle();
            keyFlags = data.ReadUInt32();
            scaleX = data.ReadSingle();
            scaleY = data.ReadSingle();
            color = data.ReadRGBAColorFull();
            mass = data.ReadSingle();
            pcType = data.ReadUInt32();
            keyframeFlags = data.ReadUInt32();
            position = new WaveformVector3(data);
            points = data.ReadList(data.ReadVector);
        }
    }

    public class PEmitterDesc {

        public uint maxParticles; // m_nMaxParticles
        public WaveformVector3 origin; // m_wvOrigin
        public uint shape; // m_Shape
        public uint particleShape; // m_ParticleShape
        public Waveform particleScale; // m_wParticleScale
        public Waveform scale; // m_wScale
        public bool explodingDir; // m_bExplodingDir
        public float birthRate; // m_BirthRate
        public Waveform lifespan; // m_wLifespan
        public Waveform velocity; // m_wVelocity
        public Waveform streak; // m_wStreak
        public WaveformVector3 rotation; // m_wvRotation
        public WaveformVector3 worldRotation; // m_wvWorldRotation
        public WaveformVector3 rotateVelocity; // m_wvRotateVelocity
        public WaveformVector3 direction; // m_wvDirection
        public Waveform minSpread; // m_wMinSpread
        public Waveform maxSpread; // m_wMaxSpread
        public uint emissionLimit; // m_nEmissionLimit
        public uint blastCount; // m_nBlastCount
        public float startTime; // m_fStartTime
        public float timeLimit; // m_fTimeLimit
        public float emissionDistance; // m_fEmissionDistance
        public Waveform particleSnap; // m_wParticleSnap
        public bool inclusiveShape; // m_bInclusiveShape
        public bool active; // m_bActive
        public float fadeIn; // m_fFadeIn
        public float fadeOut; // m_fFadeOut
        public bool[] constrainAxes = new bool[12]; // m_bConstrainAxes
        public List<PEKeyframeDesc> keyframes; // m_Keyframes

        public PEmitterDesc(AC2Reader data) {
            maxParticles = data.ReadUInt32();
            origin = new WaveformVector3(data);
            shape = data.ReadUInt32();
            particleShape = data.ReadUInt32();
            scale = new Waveform(data);
            particleScale = new Waveform(data);
            explodingDir = data.ReadBoolean();
            inclusiveShape = data.ReadBoolean();
            active = data.ReadBoolean();
            birthRate = data.ReadSingle();
            lifespan = new Waveform(data);
            velocity = new Waveform(data);
            streak = new Waveform(data);
            rotation = new WaveformVector3(data);
            worldRotation = new WaveformVector3(data);
            rotateVelocity = new WaveformVector3(data);
            direction = new WaveformVector3(data);
            minSpread = new Waveform(data);
            maxSpread = new Waveform(data);
            emissionLimit = data.ReadUInt32();
            blastCount = data.ReadUInt32();
            startTime = data.ReadSingle();
            timeLimit = data.ReadSingle();
            emissionDistance = data.ReadSingle();
            fadeIn = data.ReadSingle();
            fadeOut = data.ReadSingle();
            particleSnap = new Waveform(data);
            for (int i = 0; i < constrainAxes.Length; i++) {
                constrainAxes[i] = data.ReadByte() != 0;
            }
            keyframes = data.ReadList(() => new PEKeyframeDesc(data));
        }
    }
}
