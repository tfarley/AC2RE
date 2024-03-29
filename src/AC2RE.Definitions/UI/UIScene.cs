﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class UIScene {

    // UIScene
    public DataId did; // m_DID
    public List<EntityDesc> entityDescs; // m_EntityDescs
    public bool wantFog; // m_bWantFog
    public RGBAColor fogColor; // m_FogColor
    public float fogNear; // m_fFogNear
    public float fogFar; // m_fFogFar

    public UIScene() {

    }

    public UIScene(AC2Reader data) {
        did = data.ReadDataId();
        entityDescs = data.ReadList(() => new EntityDesc(data));
        fogColor = data.ReadRGBAColor();
        wantFog = data.ReadBoolean();
        fogNear = data.ReadSingle();
        fogFar = data.ReadSingle();
    }

    public void write(AC2Writer data) {
        data.Write(did);
        data.Write(entityDescs, v => v.write(data));
        data.Write(fogColor);
        data.Write(wantFog);
        data.Write(fogNear);
        data.Write(fogFar);
    }
}
