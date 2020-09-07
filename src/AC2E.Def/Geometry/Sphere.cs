﻿using System.Numerics;

namespace AC2E.Def {

    public class Sphere {

        public Vector3 center; // center
        public float radius; // radius

        public Sphere(AC2Reader data) {
            center = data.ReadVector();
            radius = data.ReadSingle();
        }
    }
}
