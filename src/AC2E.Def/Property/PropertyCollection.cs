using System.Collections.Generic;

namespace AC2E.Def {

    public class PropertyCollection : IPackage {

        public NativeType nativeType => NativeType.PROPERTYCOLLECTION;

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
}
