namespace AC2RE.Definitions;

public class POriginalSourceFileInfo {

    // POriginalSourceFileInfo
    public string fileName; // Filename
    public string text; // Text

    public POriginalSourceFileInfo(AC2Reader data) {
        fileName = data.ReadString();
        text = data.ReadString();
    }
}
