using System.IO;

namespace AC2E.Def.Structs {

    public class VisualDesc {

        public uint baseSetupId; // TODO: Not sure if this is correct property/name

        public void write(BinaryWriter data) {
            data.Write((uint)2); // TODO: Unknown - flags?
            data.Write(baseSetupId);
        }
    }
}
