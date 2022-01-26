namespace AC2RE.Definitions;

public class OriginalSourceFileInfo {

    public string fileName; // Filename
    public string text; // Text

    public OriginalSourceFileInfo(AC2Reader data) {
        fileName = data.ReadString();
        text = data.ReadString();
    }
}
