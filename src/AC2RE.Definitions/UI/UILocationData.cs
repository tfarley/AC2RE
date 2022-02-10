namespace AC2RE.Definitions;

public class UILocationData {

    // UILocationData
    public float x0; // m_x0
    public float y0; // m_y0
    public float x1; // m_x1
    public float y1; // m_y1
    public bool show; // m_shown

    public UILocationData() {

    }

    public UILocationData(AC2Reader data) {
        x0 = data.ReadSingle();
        y0 = data.ReadSingle();
        x1 = data.ReadSingle();
        y1 = data.ReadSingle();
        show = data.ReadBoolean();
    }

    public void write(AC2Writer data) {
        data.Write(x0);
        data.Write(y0);
        data.Write(x1);
        data.Write(y1);
        data.Write(show);
    }
}
