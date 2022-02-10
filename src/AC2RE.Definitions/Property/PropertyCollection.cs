using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PropertyCollection : IHeapObject {

    public NativeType nativeType => NativeType.PropertyCollection;

    public List<PropertyGroup> groups; // m_propertyGroup

    public PropertyCollection() {

    }

    public PropertyCollection(AC2Reader data) {
        groups = data.ReadList(() => new PropertyGroup(data));
    }

    public void write(AC2Writer data) {
        data.Write(groups, v => v.write(data));
    }
}
