﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class VTableSection {

        public class PackageInfo {

            public uint size; // size
            public uint checksum; // checksum

            public PackageInfo(AC2Reader data) {
                // TODO: Size and checksum might be swapped
                size = data.ReadUInt32();
                checksum = data.ReadUInt32();
            }
        }

        public List<List<VTableId>> funcMapper; // m_funcMapper
        public List<List<uint>> vTable; // m_vtbl
        public List<PackageInfo> packageInfo; // m_pkgInfo
        public List<PackageTypeId> packageTypeIdMap; // m_pkgIdMap
        public Dictionary<string, PackageTypeId> packageTypeIdStrMap; // m_pkgIdStrMap

        public VTableSection(AC2Reader data) {
            funcMapper = data.ReadList(() => data.ReadList(() => new VTableId(data.ReadUInt32())));
            vTable = data.ReadList(() => data.ReadList(data.ReadUInt32));
            packageInfo = data.ReadList(() => new PackageInfo(data));
            packageTypeIdMap = data.ReadList(data.ReadPackageTypeId);
            packageTypeIdStrMap = data.ReadDictionary(data.ReadString, data.ReadPackageTypeId);
        }
    }
}
