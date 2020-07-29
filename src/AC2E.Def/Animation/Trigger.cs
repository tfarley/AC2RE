namespace AC2E.Def {

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
            stop = data.ReadBoolean();
            time = data.ReadSingle();
            intensity = data.ReadSingle();
            intensityVar = data.ReadSingle();
            probability = data.ReadSingle();
            play = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(name);
            data.Write(stop);
            data.Write(time);
            data.Write(intensity);
            data.Write(intensityVar);
            data.Write(probability);
            data.Write(play);
        }
    }
}
