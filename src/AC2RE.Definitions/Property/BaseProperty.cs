using System.IO;
using System.Numerics;

namespace AC2RE.Definitions;

public class BaseProperty : IHeapObject {

    public NativeType nativeType => NativeType.BaseProperty;

    public PropertyName name; // m_propertyName
    public PropertyType type; // m_propertyType
    public PropertyGroupName group; // m_propertyGroup
    public object value; // m_propertyValue

    public BaseProperty() {

    }

    public BaseProperty(AC2Reader data) {
        name = data.ReadEnum<PropertyName>();
        BasePropertyDesc propertyDesc = MasterProperty.instance.properties[name];
        type = propertyDesc.type;
        group = propertyDesc.group;
        value = type switch {
            PropertyType.Bool => data.ReadBoolean(),
            PropertyType.Integer => data.ReadInt32(),
            PropertyType.Float => data.ReadSingle(),
            PropertyType.Vector => data.ReadVector(),
            PropertyType.Color => data.ReadRGBAColor(),
            PropertyType.String => data.ReadString(),
            PropertyType.Enum => data.ReadUInt32(),
            PropertyType.DataFile => data.ReadDataId(),
            PropertyType.Waveform => new Waveform(data),
            PropertyType.StringInfo => new StringInfo(data),
            PropertyType.PackageID => data.ReadEnum<PackageType>(),
            PropertyType.LongInteger => data.ReadInt64(),
            PropertyType.Position => new Position(data),
            _ => throw new InvalidDataException(type.ToString()),
        };
    }

    public void write(AC2Writer data) {
        data.WriteEnum(name);
        switch (type) {
            case PropertyType.Bool:
                data.Write((bool)value);
                break;
            case PropertyType.Integer:
                data.Write((int)value);
                break;
            case PropertyType.Float:
                data.Write((float)value);
                break;
            case PropertyType.Vector:
                data.Write((Vector3)value);
                break;
            case PropertyType.Color:
                data.Write((RGBAColor)value);
                break;
            case PropertyType.String:
                data.Write((string)value);
                break;
            case PropertyType.Enum:
                data.Write((uint)value);
                break;
            case PropertyType.DataFile:
                data.Write((DataId)value);
                break;
            case PropertyType.Waveform:
                ((Waveform)value).write(data);
                break;
            case PropertyType.StringInfo:
                ((StringInfo)value).write(data);
                break;
            case PropertyType.PackageID:
                data.Write((uint)value);
                break;
            case PropertyType.LongInteger:
                data.Write((long)value);
                break;
            case PropertyType.Position:
                ((Position)value).write(data);
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }
}
