﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class IconDesc : IHeapObject {

    public NativeType nativeType => NativeType.IconDesc;

    public List<IconLayerDesc> layers; // m_layers

    public IconDesc(AC2Reader data) {
        layers = data.ReadList(() => new IconLayerDesc(data));
    }

    public void write(AC2Writer data) {
        data.Write(layers, v => v.write(data));
    }
}
