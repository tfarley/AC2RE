namespace AC2RE.Definitions;

public class WaveformVector3 {

    // WaveformVector3
    public Waveform x; // x
    public Waveform y; // y
    public Waveform z; // z

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
