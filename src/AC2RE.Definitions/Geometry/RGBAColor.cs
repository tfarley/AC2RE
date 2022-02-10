namespace AC2RE.Definitions;

public struct RGBAColor {

    // RGBAColor
    public float r; // r
    public float g; // g
    public float b; // b
    public float a; // a

    public RGBAColor(float r, float g, float b, float a) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public override string ToString() {
        return $"<{r}, {g}, {b}, {a}>";
    }
}
