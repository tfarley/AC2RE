﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class VTableSection {

    public class PackageInfo {

        // PackageInfo
        public uint size; // size
        public uint checksum; // checksum

        public PackageInfo(AC2Reader data) {
            size = data.ReadUInt32();
            checksum = data.ReadUInt32();
        }
    }

    // VTableSection
    public List<List<VTableId>> funcMapper; // m_funcMapper
    public List<List<uint>> vTable; // m_vtbl
    public List<PackageInfo> packageInfo; // m_pkgInfo
    public List<PackageType> packageTypeId; // m_pkgIdMap
    public Dictionary<string, PackageType> packageNameToType; // m_pkgIdStrMap

    public VTableSection(AC2Reader data) {
        funcMapper = data.ReadList(() => data.ReadList(() => new VTableId(data.ReadUInt32())));
        vTable = data.ReadList(() => data.ReadList(data.ReadUInt32));
        packageInfo = data.ReadList(() => new PackageInfo(data));
        packageTypeId = data.ReadList(data.ReadEnum<PackageType>);
        packageNameToType = data.ReadDictionary(data.ReadString, data.ReadEnum<PackageType>);
    }
}
