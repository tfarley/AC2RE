using System;
using System.Numerics;

namespace AC2RE.Utils;

public static class MathUtil {

    public const float DEG_TO_RAD = MathF.PI / 180.0f;
    public const float RAD_TO_DEG = 180.0f / MathF.PI;

    public static Quaternion quaternionFromAxisAngleLeftHanded(Vector3 axis, float angle) {
        return Quaternion.CreateFromAxisAngle(-axis, angle);
    }
}
