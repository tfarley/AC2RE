﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CharTemplate {

    // Enum Slider::Controller
    public enum SliderController : uint {
        CLIENT_CONTROLLED, // CLIENT_CONTROLLED
        WEENIE_CONTROLLED, // WEENIE_CONTROLLED
    }

    // CharTemplate
    public DataId did; // m_DID
    public Dictionary<uint, ulong> behaviorFlags; // behaviors
    public Dictionary<uint, SliderController> sliderControllers; // sliders
    public Dictionary<ModeId, bool> modeIsCombat; // modes
    public List<uint> userControlledSliders; // userControlledSliders
    public uint defaultElementId; // defaultElementID
    public ModeId defaultModeId; // defaultModeID

    public CharTemplate(AC2Reader data) {
        did = data.ReadDataId();
        behaviorFlags = data.ReadDictionary(data.ReadUInt32, data.ReadUInt64);
        sliderControllers = data.ReadDictionary(data.ReadUInt32, data.ReadEnum<SliderController>);
        modeIsCombat = data.ReadDictionary(data.ReadEnum<ModeId>, data.ReadBoolean);
        defaultElementId = data.ReadUInt32();
        defaultModeId = data.ReadEnum<ModeId>();
        userControlledSliders = data.ReadList(data.ReadUInt32);
    }
}
