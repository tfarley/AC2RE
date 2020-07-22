namespace AC2E.Def {

    public class Waveform {

        // Enum WaveformType
        public enum WaveformType {
            INVALID = 0,
            NONE = 1,
            SPEED = 2,
            NOISE = 3,
            SINE = 4,
            SQUARE = 5,
            BOUNCE = 6,
            PERLIN = 7,
            FRACTAL = 8,
            FRAMELOOP = 9,
        }

        public WaveformType type; // type
        public float baseValue; // base
        public float baseVel; // base_vel
        public float amplitude; // amplitude
        public float amplitudeVel; // amplitude_vel
        public float phase; // phase
        public float phaseVel; // phase_vel
        public float frequency; // frequency
        public float frequencyVel; // frequency_vel
        public float roughness; // roughness
        public float roughnessVel; // roughness_vel

        public Waveform() {

        }

        public Waveform(AC2Reader data) {
            type = (WaveformType)data.ReadUInt32();
            baseValue = data.ReadSingle();
            baseVel = data.ReadSingle();
            amplitude = data.ReadSingle();
            amplitudeVel = data.ReadSingle();
            phase = data.ReadSingle();
            phaseVel = data.ReadSingle();
            frequency = data.ReadSingle();
            frequencyVel = data.ReadSingle();
            roughness = data.ReadSingle();
            roughnessVel = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(baseValue);
            data.Write(baseVel);
            data.Write(amplitude);
            data.Write(amplitudeVel);
            data.Write(phase);
            data.Write(phaseVel);
            data.Write(frequency);
            data.Write(frequencyVel);
            data.Write(roughness);
            data.Write(roughnessVel);
        }
    }
}
