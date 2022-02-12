using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ExportPackageArgs {

    public class CheckpointExportData {

        // CheckpointExportData
        public uint offset; // m_offset
        public uint tag; // m_tag

        public CheckpointExportData(AC2Reader data) {
            offset = data.ReadUInt32();
            tag = data.ReadUInt32();
        }
    }

    // ExportPackageArgs
    public string name; // m_name
    public string baseName; // m_base_name
    public TypeFlag flags; // m_flags
    public uint size; // m_size
    public uint checksum; // m_checksum
    public PackageType packageType; // m_pkg_id
    public int parentIndex; // m_parent_index
    public Dictionary<string, CheckpointExportData> checkpoints; // m_checkpoint

    public ExportPackageArgs(AC2Reader data) {
        name = data.ReadString();
        baseName = data.ReadString();
        flags = data.ReadEnum<TypeFlag>();
        size = data.ReadUInt32();
        checksum = data.ReadUInt32();
        packageType = data.ReadEnum<PackageType>();
        parentIndex = data.ReadInt32();
        checkpoints = data.ReadDictionary(data.ReadString, () => new CheckpointExportData(data));
    }
}
