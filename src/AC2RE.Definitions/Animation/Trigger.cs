namespace AC2RE.Definitions {

    public class Trigger {

        public uint name; // mName
        public bool stop; // mStop
        public float time; // mTime
        public float intensity; // mIntensity
        public float intensityVar; // mIntensityVar
        public float probability; // mProbability
        public bool play; // mPlay

        public Trigger(AC2Reader data) {
            name = data.ReadUInt32();
            time = data.ReadSingle();
            stop = data.ReadBoolean();
            probability = data.ReadSingle();
            intensity = data.ReadSingle();
            intensityVar = data.ReadSingle();
            play = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(name);
            data.Write(time);
            data.Write(stop);
            data.Write(probability);
            data.Write(intensity);
            data.Write(intensityVar);
            data.Write(play);
        }
    }
}
