using System.Text;

namespace AC2RE.Definitions;

public class WPString : IHeapObject {

    public NativeType nativeType => NativeType.wpstring;

    public string contents;

    public WPString(string contents) {
        this.contents = contents;
    }

    public WPString(AC2Reader data) {
        contents = data.ReadString(Encoding.Unicode);
    }

    public void write(AC2Writer data) {
        data.Write(contents, Encoding.Unicode);
    }

    public override string ToString() {
        return contents;
    }
}
