using System.Collections.Generic;

namespace AC2E.Def {

    public class ExportPackageArgs {

        public class CheckpointExportData {

            public uint offset; // m_offset
            public uint tag; // m_tag

            public CheckpointExportData(AC2Reader data) {
                offset = data.ReadUInt32();
                tag = data.ReadUInt32();
            }
        }

        public string name; // m_name
        public string baseName; // m_base_name
        public uint checksum; // m_checksum
        public uint size; // m_size
        public TypeFlag flags; // m_flags
        public PackageTypeId packageTypeId; // m_pkg_id
        public int parentIndex; // m_parent_index
        public Dictionary<string, CheckpointExportData> checkpoints; // m_checkpoint

        public ExportPackageArgs(AC2Reader data) {
            name = data.ReadString();
            baseName = data.ReadString();
            checksum = data.ReadUInt32();
            size = data.ReadUInt32();
            flags = (TypeFlag)data.ReadUInt32();
            packageTypeId = data.ReadPackageTypeId();
            parentIndex = data.ReadInt32();
            checkpoints = data.ReadDictionary(data.ReadString, () => new CheckpointExportData(data));
        }
    }
}
