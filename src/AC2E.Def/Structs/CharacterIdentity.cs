using AC2E.Def.Extensions;
using System.IO;
using System.Text;

namespace AC2E.Def.Structs {

    public class CharacterIdentity {

        public InstanceId id;
        public string name;
        public uint greyedOutForSeconds;
        public VisualDesc vDesc;

        public CharacterIdentity() {

        }

        public CharacterIdentity(BinaryReader data) {
            id = data.ReadInstanceId();
            name = data.ReadEncryptedString(Encoding.Unicode);
            greyedOutForSeconds = data.ReadUInt32();
            vDesc = new VisualDesc(data);
        }

        public void write(BinaryWriter data) {
            data.Write(id);
            data.WriteEncryptedString(name, Encoding.Unicode);
            data.Write(greyedOutForSeconds);
            vDesc.write(data);
        }
    }
}
