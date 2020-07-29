using System.Collections.Generic;

namespace AC2E.Def {

    public class MaterialProperty {

        public uint nameId; // nameID
        public RMDataType dataType; // dataType
        public byte[] dataBytes; // data
        public List<MaterialField> fields; // fields

        public MaterialProperty(AC2Reader data) {
            nameId = data.ReadUInt32();
            dataType = (RMDataType)data.ReadUInt32();
            uint dataLength = data.ReadUInt32();
            dataBytes = data.ReadBytes((int)dataLength);
            fields = data.ReadList(() => new MaterialField(data));
        }
    }
}
