using System.Collections.Generic;

namespace AC2E.Def {

    public class PEmitterDesc {

        public uint maxParticles; // m_nMaxParticles
        public WaveformVector3 origin; // m_wvOrigin
        public uint shape; // m_Shape
        public uint particleShape; // m_ParticleShape
        public Waveform particleScale; // m_wParticleScale
        public Waveform scale; // m_wScale
        public bool explodingDir; // m_bExplodingDir
        public float birthRate; // m_BirthRate
        public Waveform lifetime; // m_wLifespan
        public Waveform vel; // m_wVelocity
        public Waveform streak; // m_wStreak
        public WaveformVector3 rot; // m_wvRotation
        public WaveformVector3 worldRot; // m_wvWorldRotation
        public WaveformVector3 rotateVel; // m_wvRotateVelocity
        public WaveformVector3 direction; // m_wvDirection
        public Waveform minSpread; // m_wMinSpread
        public Waveform maxSpread; // m_wMaxSpread
        public uint emissionLimit; // m_nEmissionLimit
        public uint blastCount; // m_nBlastCount
        public float startTime; // m_fStartTime
        public float timeLimit; // m_fTimeLimit
        public float emissionDist; // m_fEmissionDistance
        public Waveform particleSnap; // m_wParticleSnap
        public bool inclusiveShape; // m_bInclusiveShape
        public bool active; // m_bActive
        public float fadeIn; // m_fFadeIn
        public float fadeOut; // m_fFadeOut
        public bool[] constrainAxes = new bool[12]; // m_bConstrainAxes
        public List<PEKeyframeDesc> keyframes; // m_Keyframes

        public PEmitterDesc(AC2Reader data) {
            maxParticles = data.ReadUInt32();
            origin = new(data);
            shape = data.ReadUInt32();
            particleShape = data.ReadUInt32();
            scale = new(data);
            particleScale = new(data);
            explodingDir = data.ReadBoolean();
            inclusiveShape = data.ReadBoolean();
            active = data.ReadBoolean();
            birthRate = data.ReadSingle();
            lifetime = new(data);
            vel = new(data);
            streak = new(data);
            rot = new(data);
            worldRot = new(data);
            rotateVel = new(data);
            direction = new(data);
            minSpread = new(data);
            maxSpread = new(data);
            emissionLimit = data.ReadUInt32();
            blastCount = data.ReadUInt32();
            startTime = data.ReadSingle();
            timeLimit = data.ReadSingle();
            emissionDist = data.ReadSingle();
            fadeIn = data.ReadSingle();
            fadeOut = data.ReadSingle();
            particleSnap = new(data);
            for (int i = 0; i < constrainAxes.Length; i++) {
                constrainAxes[i] = data.ReadByte() != 0;
            }
            keyframes = data.ReadList(() => new PEKeyframeDesc(data));
        }
    }
}
