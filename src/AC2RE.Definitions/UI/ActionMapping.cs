namespace AC2RE.Definitions;

public class ActionMapping {

    // ActionMapping
    public InputAction action; // act
    public uint semantic; // semantic
    public string name; // name

    public ActionMapping(AC2Reader data) {
        action = data.ReadEnum<InputAction>();
        semantic = data.ReadUInt32();
        name = data.ReadString();
    }
}
