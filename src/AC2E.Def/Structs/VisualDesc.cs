using System.IO;

namespace AC2E.Def.Structs {

    public class VisualDesc {

        public uint unk1; // TODO: Unknown - flags?
        public uint baseSetupId; // TODO: Not sure if this is correct property/name

        public VisualDesc() {

        }

        public VisualDesc(BinaryReader data) {
            unk1 = data.ReadUInt32();
            baseSetupId = data.ReadUInt32();
        }

        public void write(BinaryWriter data) {
            data.Write((uint)2);
            data.Write(baseSetupId);
        }
    }
}
