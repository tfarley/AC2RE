namespace AC2RE.Definitions {

    public class RepulsorFX {

        public float accel; // m_fAcceleration
        public float range; // m_fRange
        public float lifetime; // m_fLifetime
        public float minVel; // m_fMinimumVelocity
        public bool attractor; // m_bAttractor

        public RepulsorFX() {

        }

        public RepulsorFX(AC2Reader data) {
            accel = data.ReadSingle();
            range = data.ReadSingle();
            lifetime = data.ReadSingle();
            minVel = data.ReadSingle();
            attractor = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(accel);
            data.Write(range);
            data.Write(lifetime);
            data.Write(minVel);
            data.Write(attractor);
        }
    }
}
