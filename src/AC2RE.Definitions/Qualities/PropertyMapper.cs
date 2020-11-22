using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class PropertyMapper : IPackage {

        public PackageType packageType => PackageType.PropertyMapper;

        public Dictionary<uint, uint> propServer; // m_hashPropServer
        public Dictionary<uint, uint> propClient; // m_hashPropClient

        public PropertyMapper(AC2Reader data) {
            data.ReadPkg<AAHash>(v => propServer = v);
            data.ReadPkg<AAHash>(v => propClient = v);
        }
    }
}
