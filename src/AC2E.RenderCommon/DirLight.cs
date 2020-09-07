﻿using System.Numerics;

namespace AC2E.RenderCommon.OpenGL {

    public class DirLight {

        public Vector3 dir;
        public Vector3 color;

        public DirLight() {

        }

        public DirLight(DirLight dirLight) {
            dir = dirLight.dir;
            color = dirLight.color;
        }
    }
}
