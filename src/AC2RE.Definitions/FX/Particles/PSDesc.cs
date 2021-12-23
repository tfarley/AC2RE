using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PSDesc {

    public DataId did; // m_DID
    public uint maxParticles; // m_MaxParticles
    public float physicsTimeStep; // m_PhysicsTimeStep
    public float fastForwardTime; // m_FastForwardTime
    public float physicsDuration; // m_PhysicsDuration
    public uint scaleType; // m_ScaleType
    public bool worldSpace; // m_bWorldSpace
    public bool forceDraw; // m_bForceDraw
    public float startFadeDist; // m_fStartFadeDistance
    public float stopFadeDist; // m_fStopFadeDistance
    public List<PEmitterDesc> peDescs; // m_PEDescs
    public DataId materialDid; // m_MaterialID

    public PSDesc(AC2Reader data) {
        did = data.ReadDataId();
        maxParticles = data.ReadUInt32();
        scaleType = data.ReadUInt32();
        physicsTimeStep = data.ReadSingle();
        fastForwardTime = data.ReadSingle();
        physicsDuration = data.ReadSingle();
        startFadeDist = data.ReadSingle();
        stopFadeDist = data.ReadSingle();
        worldSpace = data.ReadBoolean();
        forceDraw = data.ReadBoolean();
        peDescs = data.ReadList(() => new PEmitterDesc(data));
        materialDid = data.ReadDataId();
    }
}
