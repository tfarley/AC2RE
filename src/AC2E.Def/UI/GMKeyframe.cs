using System.IO;
using System.Text;

namespace AC2E.Def {

    public class GMKeyframe : IPackage {

        public NativeType nativeType => NativeType.GMKEYFRAME;

        public uint m_sceneID;
        public uint m_uiFrameNum;
        public double m_ttDuration;
        public DataId m_didVideoFragment;
        public DataId m_didAudioFragment;
        public DataId m_didVoiceFragment;
        public StringInfo m_siTextFragment;
        public string m_strMovieFragmentFilename;
        public uint m_definedTracks;

        public GMKeyframe() {

        }

        public GMKeyframe(BinaryReader data) {
            m_sceneID = data.ReadUInt32();
            m_uiFrameNum = data.ReadUInt32();
            m_ttDuration = data.ReadDouble();
            m_didVideoFragment = data.ReadDataId();
            m_didAudioFragment = data.ReadDataId();
            m_didVoiceFragment = data.ReadDataId();
            m_siTextFragment = new StringInfo(data);
            m_strMovieFragmentFilename = data.ReadEncryptedString(Encoding.Unicode);
            m_definedTracks = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_sceneID);
            data.Write(m_uiFrameNum);
            data.Write(m_ttDuration);
            data.Write(m_didVideoFragment);
            data.Write(m_didAudioFragment);
            data.Write(m_didVoiceFragment);
            m_siTextFragment.write(data);
            data.WriteEncryptedString(m_strMovieFragmentFilename, Encoding.Unicode);
            data.Write(m_definedTracks);
        }
    }
}
