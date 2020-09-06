using System;

namespace AC2E.RenderCommon {

    public struct Mat4x4 {

        private static readonly float DEG_TO_RAG = (float)Math.PI / 180.0f;

        public float[,] m;

        public static Mat4x4 identity() {
            return new Mat4x4 {
                m = new float[,] {
                    { 1.0f, 0.0f, 0.0f, 0.0f},
                    { 0.0f, 1.0f, 0.0f, 0.0f},
                    { 0.0f, 0.0f, 1.0f, 0.0f},
                    { 0.0f, 0.0f, 0.0f, 1.0f},
                }
            };
        }

        public static Mat4x4 perspective(float vFov, float width, float height, float nearClip, float farClip) {
            float aspect = height / width;
            float vertical = 1.0f / (float)Math.Tan(vFov * DEG_TO_RAG / 2.0f);
            float horizontal = vertical * aspect;
            float frustDist = farClip - nearClip;
            // X right, Y forward, Z up input -> X right, Y up, Z backward output (OpenGL)
            return new Mat4x4 {
                m = new float[,] {
                    { horizontal, 0.0f, 0.0f, 0.0f },
                    { 0.0f, 0.0f, farClip / frustDist, 1.0f },
                    { 0.0f, vertical, 0.0f, 0.0f },
                    { 0.0f, 0.0f, -(farClip * nearClip) / frustDist, 0.0f },
                }
            };
        }

        public void translate(float x, float y, float z) {
            m[3, 0] += x;
            m[3, 1] += y;
            m[3, 2] += z;
        }

        public static Mat4x4 operator *(Mat4x4 m1, Mat4x4 m2) {
            // Multiplication order matches OpenGL (transforms applied right-to-left)
            return new Mat4x4 {
                m = new float[,] {
                    {
                        m2.m[0, 0] * m1.m[0, 0] + m2.m[0, 1] * m1.m[1, 0] + m2.m[0, 2] * m1.m[2, 0] + m2.m[0, 3] * m1.m[3, 0],
                        m2.m[0, 0] * m1.m[0, 1] + m2.m[0, 1] * m1.m[1, 1] + m2.m[0, 2] * m1.m[2, 1] + m2.m[0, 3] * m1.m[3, 1],
                        m2.m[0, 0] * m1.m[0, 2] + m2.m[0, 1] * m1.m[1, 2] + m2.m[0, 2] * m1.m[2, 2] + m2.m[0, 3] * m1.m[3, 2],
                        m2.m[0, 0] * m1.m[0, 3] + m2.m[0, 1] * m1.m[1, 3] + m2.m[0, 2] * m1.m[2, 3] + m2.m[0, 3] * m1.m[3, 3],
                    },
                    {
                        m2.m[1, 0] * m1.m[0, 0] + m2.m[1, 1] * m1.m[1, 0] + m2.m[1, 2] * m1.m[2, 0] + m2.m[1, 3] * m1.m[3, 0],
                        m2.m[1, 0] * m1.m[0, 1] + m2.m[1, 1] * m1.m[1, 1] + m2.m[1, 2] * m1.m[2, 1] + m2.m[1, 3] * m1.m[3, 1],
                        m2.m[1, 0] * m1.m[0, 2] + m2.m[1, 1] * m1.m[1, 2] + m2.m[1, 2] * m1.m[2, 2] + m2.m[1, 3] * m1.m[3, 2],
                        m2.m[1, 0] * m1.m[0, 3] + m2.m[1, 1] * m1.m[1, 3] + m2.m[1, 2] * m1.m[2, 3] + m2.m[1, 3] * m1.m[3, 3],
                    },
                    {
                        m2.m[2, 0] * m1.m[0, 0] + m2.m[2, 1] * m1.m[1, 0] + m2.m[2, 2] * m1.m[2, 0] + m2.m[2, 3] * m1.m[3, 0],
                        m2.m[2, 0] * m1.m[0, 1] + m2.m[2, 1] * m1.m[1, 1] + m2.m[2, 2] * m1.m[2, 1] + m2.m[2, 3] * m1.m[3, 1],
                        m2.m[2, 0] * m1.m[0, 2] + m2.m[2, 1] * m1.m[1, 2] + m2.m[2, 2] * m1.m[2, 2] + m2.m[2, 3] * m1.m[3, 2],
                        m2.m[2, 0] * m1.m[0, 3] + m2.m[2, 1] * m1.m[1, 3] + m2.m[2, 2] * m1.m[2, 3] + m2.m[2, 3] * m1.m[3, 3],
                    },
                    {
                        m2.m[3, 0] * m1.m[0, 0] + m2.m[3, 1] * m1.m[1, 0] + m2.m[3, 2] * m1.m[2, 0] + m2.m[3, 3] * m1.m[3, 0],
                        m2.m[3, 0] * m1.m[0, 1] + m2.m[3, 1] * m1.m[1, 1] + m2.m[3, 2] * m1.m[2, 1] + m2.m[3, 3] * m1.m[3, 1],
                        m2.m[3, 0] * m1.m[0, 2] + m2.m[3, 1] * m1.m[1, 2] + m2.m[3, 2] * m1.m[2, 2] + m2.m[3, 3] * m1.m[3, 2],
                        m2.m[3, 0] * m1.m[0, 3] + m2.m[3, 1] * m1.m[1, 3] + m2.m[3, 2] * m1.m[2, 3] + m2.m[3, 3] * m1.m[3, 3],
                    },
                }
            };
        }

        public override string ToString() {
            if (m == null) {
                return "null";
            }

            return $"{m[0, 0]} {m[0, 1]} {m[0, 2]} {m[0, 3]}\n{m[1, 0]} {m[1, 1]} {m[1, 2]} {m[1, 3]}\n{m[2, 0]} {m[2, 1]} {m[2, 2]} {m[2, 3]}\n{m[3, 0]} {m[3, 1]} {m[3, 2]} {m[3, 3]}";
        }
    }
}
