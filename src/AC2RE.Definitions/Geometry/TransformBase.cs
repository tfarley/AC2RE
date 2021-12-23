using System.Numerics;

namespace AC2RE.Definitions;

public class TransformBase {

    public Vector3 offset; // offset
    public Vector3 scale; // scale
    public Quaternion rot; // qt

    public TransformBase(AC2Reader data) {
        offset = data.ReadVector();
        scale = data.ReadVector();
        rot = data.ReadQuaternion();
    }
}
