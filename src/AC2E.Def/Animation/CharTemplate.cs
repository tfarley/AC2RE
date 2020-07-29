using System.Collections.Generic;

namespace AC2E.Def {

    public class CharTemplate {

        // Enum Slider::Controller
        public enum SliderController : uint {

            CLIENT_CONTROLLED = 0,
            WEENIE_CONTROLLED = 1,
        }

        public DataId did; // m_DID
        public Dictionary<uint, ulong> behaviorFlags; // behaviors
        public Dictionary<uint, SliderController> sliderControllers; // sliders
        public Dictionary<uint, bool> modeIsCombat; // modes
        public List<uint> userControlledSliders; // userControlledSliders
        public uint defaultElementId; // defaultElementID
        public uint defaultModeId; // defaultModeID

        public CharTemplate(AC2Reader data) {
            did = data.ReadDataId();
            behaviorFlags = data.ReadDictionary(data.ReadUInt32, data.ReadUInt64);
            sliderControllers = data.ReadDictionary(data.ReadUInt32, () => (SliderController)data.ReadUInt32());
            modeIsCombat = data.ReadDictionary(data.ReadUInt32, data.ReadBoolean);
            defaultElementId = data.ReadUInt32();
            defaultModeId = data.ReadUInt32();
            userControlledSliders = data.ReadList(data.ReadUInt32);
        }
    }
}
