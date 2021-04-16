using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class PropertyMapper : IPackage {

        public PackageType packageType => PackageType.PropertyMapper;

        public Dictionary<PropertyName, uint> propServer; // m_hashPropServer
        public Dictionary<PropertyName, uint> propClient; // m_hashPropClient

        public PropertyMapper(AC2Reader data) {
            data.ReadPkg<AAHash>(v => propServer = v.to<PropertyName, uint>());
            data.ReadPkg<AAHash>(v => propClient = v.to<PropertyName, uint>());
        }
    }
}
