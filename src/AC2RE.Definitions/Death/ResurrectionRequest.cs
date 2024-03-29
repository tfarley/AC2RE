﻿namespace AC2RE.Definitions;

public class ResurrectionRequest : IHeapObject {

    public PackageType packageType => PackageType.ResurrectionRequest;

    public InstanceId rezzerId; // m_rezzerID
    public StringInfo rezzerName; // m_rezzerName
    public float focusLossMod; // m_focusLossMod
    public FxId fx; // m_fx

    public ResurrectionRequest() {

    }

    public ResurrectionRequest(AC2Reader data) {
        rezzerId = data.ReadInstanceId();
        data.ReadHO<StringInfo>(v => rezzerName = v);
        focusLossMod = data.ReadSingle();
        fx = data.ReadEnum<FxId>();
    }

    public void write(AC2Writer data) {
        data.Write(rezzerId);
        data.WriteHO(rezzerName);
        data.Write(focusLossMod);
        data.WriteEnum(fx);
    }
}
