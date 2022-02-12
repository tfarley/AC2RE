using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PropertyGroup : IHeapObject {

    public NativeType nativeType => NativeType.PropertyGroup;

    public PropertyGroupName name; // m_groupName
    public List<BaseProperty> properties; // m_properties

    public PropertyGroup() {

    }

    public PropertyGroup(AC2Reader data) {
        name = data.ReadEnum<PropertyGroupName>();
        properties = data.ReadList(() => new BaseProperty(data));
    }

    public void write(AC2Writer data) {
        data.WriteEnum(name);
        data.Write(properties, v => v.write(data));
    }
}
