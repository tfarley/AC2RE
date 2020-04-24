using AC2E.Def.Extensions;
using System.IO;
using System.Text;

namespace AC2E.Def.Structs {

    public class CharacterIdentity {

        public InstanceId id;
        public string name;
        public uint greyedOutForSeconds;
        public VisualDesc vDesc;

        public void write(BinaryWriter data) {
            data.Write(id);
            data.WriteEncryptedString(name, Encoding.Unicode);
            data.Write(greyedOutForSeconds);
            vDesc.write(data);
        }
    }
}
