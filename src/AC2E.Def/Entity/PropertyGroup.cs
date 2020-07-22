using System.Collections.Generic;

namespace AC2E.Def {

    public class PropertyGroup : IPackage {

        NativeType nativeType => NativeType.PROPERTYGROUP;

        public PropertyGroupName name; // m_groupName
        public List<BaseProperty> properties; // m_properties
        public List<BaseProperty> optionalProperties; // m_optionalProperties

        public PropertyGroup() {

        }

        public PropertyGroup(AC2Reader data) {
            name = (PropertyGroupName)data.ReadUInt32();
            properties = data.ReadList(() => new BaseProperty(data));
        }

        public void write(AC2Writer data) {
            data.Write((uint)name);
            data.Write(properties, v => v.write(data));
            data.Write(optionalProperties, v => v.write(data));
        }
    }
}
