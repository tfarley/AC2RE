using System.IO;

public class CharacterIdentity {

    public ulong id;
    public string name;
    public uint greyedOutForSeconds;
    public VisualDesc vDesc;

    public void write(BinaryWriter data) {
        data.Write(id);
        data.WriteEncryptedString(name);
        data.Write(greyedOutForSeconds);
        vDesc.write(data);
    }
}
