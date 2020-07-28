using System.IO;

namespace AC2E.Def {

    public class AttributeDesc {

        // Const ATTRIBUTE_*
        public enum AttributeType : uint {
            UNDEF,
            ID,
            INT,
            FLOAT,
            BOOL,
            ASCII,
            STRING,
            DATAID,
        }

        public uint id; // attributeID
        public AttributeType type; // type
        public uint valStrId; // val_strID
        public uint valId; // val_ID
        public int valInt; // val_Int
        public float valFloat; // val_Float
        public bool valBool; // val_Bool
        public string valAscii; // val_ASCII
        public DataId valDid; // val_DataID

        public AttributeDesc() {

        }

        public AttributeDesc(AC2Reader data) {
            id = data.ReadUInt32();
            type = (AttributeType)data.ReadUInt32();
            switch (type) {
                case AttributeType.ID:
                    valId = data.ReadUInt32();
                    break;
                case AttributeType.INT:
                    valInt = data.ReadInt32();
                    break;
                case AttributeType.FLOAT:
                    valFloat = data.ReadSingle();
                    break;
                case AttributeType.BOOL:
                    valBool = data.ReadBoolean();
                    break;
                case AttributeType.ASCII:
                    valAscii = data.ReadString();
                    break;
                case AttributeType.STRING:
                    valStrId = data.ReadUInt32();
                    break;
                case AttributeType.DATAID:
                    valDid = data.ReadDataId();
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }

        public void write(AC2Writer data) {
            data.Write(id);
            data.Write((uint)type);
            switch (type) {
                case AttributeType.ID:
                    data.Write(valId);
                    break;
                case AttributeType.INT:
                    data.Write(valInt);
                    break;
                case AttributeType.FLOAT:
                    data.Write(valFloat);
                    break;
                case AttributeType.BOOL:
                    data.Write(valBool);
                    break;
                case AttributeType.ASCII:
                    data.Write(valAscii);
                    break;
                case AttributeType.STRING:
                    data.Write(valStrId);
                    break;
                case AttributeType.DATAID:
                    data.Write(valDid);
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }
    }
}
