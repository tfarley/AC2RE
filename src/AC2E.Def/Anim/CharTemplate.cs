using System.Collections.Generic;

namespace AC2E.Def {

    public class CharTemplate {

        public DataId did; // m_DID
        public Dictionary<uint, Behavior> behaviors; // behaviors
        public Dictionary<uint, Slider> sliders; // sliders
        public Dictionary<uint, Mode> modes; // modes
        public List<uint> userControlledSliders; // userControlledSliders
        public uint defaultElementId; // defaultElementID
        public uint defaultModeId; // defaultModeID

        public CharTemplate(AC2Reader data) {
            did = data.ReadDataId();
            behaviors = data.ReadDictionary(data.ReadUInt32, () => new Behavior(data));
            sliders = data.ReadDictionary(data.ReadUInt32, () => new Slider(data));
            modes = data.ReadDictionary(data.ReadUInt32, () => new Mode(data));
            defaultElementId = data.ReadUInt32();
            defaultModeId = data.ReadUInt32();
            userControlledSliders = data.ReadList(data.ReadUInt32);
        }
    }
}
