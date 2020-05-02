using AC2E.Def.Extensions;
using System.IO;
using System.Text;

namespace AC2E.Def.Structs {

    public class CharacterIdentity {

        public InstanceId id; // id_
        public string name; // name_
        public uint secondsGreyedOut; // secondsGreyedOut_
        public VisualDesc vDesc; // vDesc_

        public CharacterIdentity() {

        }

        public CharacterIdentity(BinaryReader data) {
            id = data.ReadInstanceId();
            name = data.ReadEncryptedString(Encoding.Unicode);
            secondsGreyedOut = data.ReadUInt32();
            vDesc = new VisualDesc(data);
        }

        public void write(BinaryWriter data) {
            data.Write(id);
            data.WriteEncryptedString(name, Encoding.Unicode);
            data.Write(secondsGreyedOut);
            vDesc.write(data);
        }
    }
}
