namespace AC2RE.Definitions;

public class OriginalSourceFileInfo {

    public uint fileNameIndex; // FilenameIdx
    public string text; // Text

    public OriginalSourceFileInfo(AC2Reader data) {
        // TODO: fileName and text might be swapped
        fileNameIndex = data.ReadUInt32();
        text = data.ReadNullTermString();
    }
}
