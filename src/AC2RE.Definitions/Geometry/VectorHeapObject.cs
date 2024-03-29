﻿using System.Numerics;

namespace AC2RE.Definitions;

public struct VectorHeapObject : IHeapObject {

    public NativeType nativeType => NativeType.Vector;

    public Vector3 v;

    public VectorHeapObject(Vector3 v) {
        this.v = v;
    }

    public void write(AC2Writer data) {
        data.Write(v);
    }

    public override string ToString() {
        return v.ToString();
    }
}
