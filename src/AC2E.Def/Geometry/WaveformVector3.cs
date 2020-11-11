namespace AC2E.Def {

    public class WaveformVector3 {

        public Waveform x;
        public Waveform y;
        public Waveform z;

        public WaveformVector3() {

        }

        public WaveformVector3(AC2Reader data) {
            x = new(data);
            y = new(data);
            z = new(data);
        }

        public void write(AC2Writer data) {
            x.write(data);
            y.write(data);
            z.write(data);
        }
    }
}
