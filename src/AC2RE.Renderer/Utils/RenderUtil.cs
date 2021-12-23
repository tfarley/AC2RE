using System;
using System.Numerics;

namespace AC2RE.Renderer;

internal static class RenderUtil {

    public const float DEG_TO_RAG = MathF.PI / 180.0f;

    public static Matrix4x4 perspective(float vFov, float width, float height, float nearClip, float farClip) {
        float aspect = height / width;
        float vertical = 1.0f / MathF.Tan(vFov * DEG_TO_RAG / 2.0f);
        float horizontal = vertical * aspect;
        float frustDist = farClip - nearClip;
        // X right, Y forward, Z up input -> X right, Y up, Z backward output (OpenGL)
        return new(
            horizontal, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, farClip / frustDist, 1.0f,
            0.0f, vertical, 0.0f, 0.0f,
            0.0f, 0.0f, -(farClip * nearClip) / frustDist, 0.0f
            );
    }
}
