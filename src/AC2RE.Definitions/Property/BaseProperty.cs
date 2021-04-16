using System.IO;
using System.Numerics;

namespace AC2RE.Definitions {

    public class BaseProperty : IPackage {

        public NativeType nativeType => NativeType.BASEPROPERTY;

        public PropertyName name; // m_propertyName
        public PropertyType type; // m_propertyType
        public PropertyGroupName group; // m_propertyGroup
        public object value; // m_propertyValue

        public BaseProperty() {

        }

        public BaseProperty(AC2Reader data) {
            name = (PropertyName)data.ReadUInt32();
            BasePropertyDesc propertyDesc = MasterProperty.instance.properties[name];
            type = propertyDesc.type;
            group = propertyDesc.group;
            value = type switch {
                PropertyType.BOOL => data.ReadBoolean(),
                PropertyType.INTEGER => data.ReadInt32(),
                PropertyType.FLOAT => data.ReadSingle(),
                PropertyType.VECTOR => data.ReadVector(),
                PropertyType.COLOR => data.ReadRGBAColor(),
                PropertyType.STRING => data.ReadString(),
                PropertyType.ENUM => data.ReadUInt32(),
                PropertyType.DATA_FILE => data.ReadDataId(),
                PropertyType.WAVEFORM => new Waveform(data),
                PropertyType.STRING_INFO => new StringInfo(data),
                PropertyType.PACKAGE_ID => (PackageType)data.ReadUInt32(),
                PropertyType.LONG_INTEGER => data.ReadInt64(),
                PropertyType.POSITION => new Position(data),
                _ => throw new InvalidDataException(type.ToString()),
            };
        }

        public void write(AC2Writer data) {
            data.Write((uint)name);
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
                    data.Write((uint)value);
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
