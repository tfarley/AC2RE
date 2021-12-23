using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PropertyGroup : IPackage {

    public NativeType nativeType => NativeType.PropertyGroup;

    public PropertyGroupName name; // m_groupName
    public List<BaseProperty> properties; // m_properties

    public PropertyGroup() {

    }

    public PropertyGroup(AC2Reader data) {
        name = (PropertyGroupName)data.ReadUInt32();
        properties = data.ReadList(() => new BaseProperty(data));
    }

    public void write(AC2Writer data) {
        data.Write((uint)name);
        data.Write(properties, v => v.write(data));
    }
}
