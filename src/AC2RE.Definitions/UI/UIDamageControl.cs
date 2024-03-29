﻿namespace AC2RE.Definitions;

public class UIDamageControl : IHeapObject {

    public PackageType packageType => PackageType.UIDamageControl;

    public float defaultFloatyNumberDuration; // m_fDefaultFloatyNumberDuration

    public UIDamageControl(AC2Reader data) {
        defaultFloatyNumberDuration = data.ReadSingle();
    }
}
