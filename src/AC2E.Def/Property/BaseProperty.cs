using System.IO;
using System.Numerics;

namespace AC2E.Def {

    public class BaseProperty : IPackage {

        NativeType nativeType => NativeType.BASEPROPERTY;

        public uint name; // m_propertyName
        [PackageIgnore]
        public string nameStr;
        public PropertyType type; // m_propertyType
        public PropertyGroupName group; // m_propertyGroup
        public object value; // m_propertyValue

        public BaseProperty() {

        }

        public BaseProperty(AC2Reader data) {
            name = data.ReadUInt32();
            BasePropertyDesc propertyDesc = MasterProperty.instance.properties[name];
            nameStr = MasterProperty.instance.enumMapper.idToString[name];
            type = propertyDesc.type;
            group = propertyDesc.group;
            switch (type) {
                case PropertyType.BOOL:
                    value = data.ReadBoolean();
                    break;
                case PropertyType.INTEGER:
                    value = data.ReadInt32();
                    break;
                case PropertyType.FLOAT:
                    value = data.ReadSingle();
                    break;
                case PropertyType.VECTOR:
                    value = data.ReadVector();
                    break;
                case PropertyType.COLOR:
                    value = data.ReadRGBAColor();
                    break;
                case PropertyType.STRING:
                    value = data.ReadString();
                    break;
                case PropertyType.ENUM:
                    value = data.ReadUInt32();
                    break;
                case PropertyType.DATA_FILE:
                    value = data.ReadDataId();
                    break;
                case PropertyType.WAVEFORM:
                    value = new Waveform(data);
                    break;
                case PropertyType.STRING_INFO:
                    value = new StringInfo(data);
                    break;
                case PropertyType.PACKAGE_ID:
                    value = data.ReadPackageId();
                    break;
                case PropertyType.LONG_INTEGER:
                    value = data.ReadInt64();
                    break;
                case PropertyType.POSITION:
                    value = new Position(data);
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }

        public void write(AC2Writer data) {
            data.Write(name);
            switch (type) {
                case PropertyType.BOOL:
                    data.Write((bool)value);
                    break;
                case PropertyType.INTEGER:
                    data.Write((int)value);
                    break;
                case PropertyType.FLOAT:
                    data.Write((float)value);
                    break;
                case PropertyType.VECTOR:
                    data.Write((Vector3)value);
                    break;
                case PropertyType.COLOR:
                    data.Write((RGBAColor)value);
                    break;
                case PropertyType.STRING:
                    data.Write((string)value);
                    break;
                case PropertyType.ENUM:
                    data.Write((uint)value);
                    break;
                case PropertyType.DATA_FILE:
                    data.Write((DataId)value);
                    break;
                case PropertyType.WAVEFORM:
                    ((Waveform)value).write(data);
                    break;
                case PropertyType.STRING_INFO:
                    ((StringInfo)value).write(data);
                    break;
                case PropertyType.PACKAGE_ID:
                    data.Write((PackageId)value);
                    break;
                case PropertyType.LONG_INTEGER:
                    data.Write((long)value);
                    break;
                case PropertyType.POSITION:
                    ((Position)value).write(data);
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }
    }
}
