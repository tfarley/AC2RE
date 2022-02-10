﻿namespace AC2RE.Definitions;

public class MaterialLayer {

    // MaterialLayer
    public uint trueFlags; // trueFlags
    public uint falseFlags; // falseFlags
    //public List<LayerStage> stages; // stages
    //public List<LayerModifier> modifiers; // modifiers

    public MaterialLayer(AC2Reader data) {
        trueFlags = data.ReadUInt32();
        falseFlags = data.ReadUInt32();
        //stages = data.ReadList(() => new LayerStage(data));
        //modifiers = data.ReadList(() => new LayerModifier(data));
        // TODO: Read the rest
    }
}
