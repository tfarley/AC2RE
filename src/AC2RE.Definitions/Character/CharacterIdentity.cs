using System.Text;

namespace AC2RE.Definitions;

public class CharacterIdentity {

    // CharacterIdentity
    public InstanceId id; // id_
    public string name; // name_
    public uint secondsGreyedOut; // secondsGreyedOut_
    public VisualDesc visualDesc; // vDesc_

    public CharacterIdentity() {

    }

    public CharacterIdentity(AC2Reader data) {
        id = data.ReadInstanceId();
        name = data.ReadString(Encoding.Unicode);
        secondsGreyedOut = data.ReadUInt32();
        visualDesc = new(data);
    }

    public void write(AC2Writer data) {
        data.Write(id);
        data.Write(name, Encoding.Unicode);
        data.Write(secondsGreyedOut);
        visualDesc.write(data);
    }
}
