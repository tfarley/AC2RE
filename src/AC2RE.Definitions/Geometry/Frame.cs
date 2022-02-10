using System.Numerics;

namespace AC2RE.Definitions;

public struct Frame : IHeapObject {

    public NativeType nativeType => NativeType.Frame;

    public Vector3 pos;
    public Quaternion rot;

    public Frame(Vector3 pos, Quaternion rot) {
        this.pos = pos;
        this.rot = rot;
    }

    public Frame(AC2Reader data) {
        pos = data.ReadVector();
        rot = data.ReadQuaternion();
    }

    public void write(AC2Writer data) {
        data.Write(pos);
        data.Write(rot);
    }
}
