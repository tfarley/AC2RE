using System.Text;

namespace AC2RE.Definitions;

public class GMKeyframe : IPackage {

    public NativeType nativeType => NativeType.gmKeyframe;

    public SceneId sceneId; // m_sceneID
    public uint frameNum; // m_uiFrameNum
    public double duration; // m_ttDuration
    public DataId videoFragmentDid; // m_didVideoFragment
    public DataId audioFragmentDid; // m_didAudioFragment
    public DataId voiceFragmentDid; // m_didVoiceFragment
    public StringInfo textFragment; // m_siTextFragment
    public string movieFragmentFileName; // m_strMovieFragmentFilename
    public uint definedTracks; // m_definedTracks

    public GMKeyframe() {

    }

    public GMKeyframe(AC2Reader data) {
        sceneId = (SceneId)data.ReadUInt32();
        frameNum = data.ReadUInt32();
        duration = data.ReadDouble();
        videoFragmentDid = data.ReadDataId();
        audioFragmentDid = data.ReadDataId();
        voiceFragmentDid = data.ReadDataId();
        textFragment = new(data);
        movieFragmentFileName = data.ReadString(Encoding.Unicode);
        definedTracks = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write((uint)sceneId);
        data.Write(frameNum);
        data.Write(duration);
        data.Write(videoFragmentDid);
        data.Write(audioFragmentDid);
        data.Write(voiceFragmentDid);
        textFragment.write(data);
        data.Write(movieFragmentFileName, Encoding.Unicode);
        data.Write(definedTracks);
    }
}
