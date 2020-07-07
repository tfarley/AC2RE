using AC2E.Def;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Interp {

    public class ByteStream {

        public static readonly int BS_VERSION_NUM = 15;

        public class VersionTable {

            public List<uint> versions; // m_versions

            public VersionTable(BinaryReader data) {
                versions = data.ReadList(data.ReadUInt32);
            }
        }

        public class OpcodeStream {

            public uint size; // m_size
            public byte[] opcodeBytes; // m_pOpcodeBytes

            public OpcodeStream(BinaryReader data) {
                size = data.ReadUInt32();
                opcodeBytes = data.ReadBytes((int)size);
            }
        }

        public class StringLitTable {

            public List<string> strings; // m_strtbl
            public List<List<uint>> offsets; // m_offsets

            public StringLitTable(BinaryReader data) {
                strings = data.ReadList(() => data.ReadEncryptedString(Encoding.Unicode));
                offsets = data.ReadList(() => data.ReadList(data.ReadUInt32));
            }
        }

        public class ImportData {

            public string packageName; // m_pkgName
            public List<ImportSymbolInfo> symbols; // m_syms
            public List<uint> packageOffsets; // m_pkgOffsets

            public ImportData(BinaryReader data) {
                packageName = data.ReadEncryptedString();
                symbols = data.ReadList(() => new ImportSymbolInfo(data));
                packageOffsets = data.ReadList(data.ReadUInt32);
            }
        }

        public class ImportSymbolInfo {

            public string name; // m_name
            public List<uint> offsets; // m_offsets
            public uint fromAddr; // m_from_addr

            public ImportSymbolInfo(BinaryReader data) {
                name = data.ReadEncryptedString();
                offsets = data.ReadList(data.ReadUInt32);
                fromAddr = data.ReadUInt32(); // TODO: Might be a bool
            }
        }

        public class ExportData {

            public ExportPackageArgs args; // m_args
            public List<ExportFunctionData> funcs; // m_funcs

            public ExportData(BinaryReader data) {
                args = new ExportPackageArgs(data);
                funcs = data.ReadList(() => new ExportFunctionData(data));
            }
        }

        public class ExportPackageArgs {

            public string name; // m_name
            public string baseName; // m_base_name
            public uint checksum; // m_checksum
            public uint size; // m_size
            public TypeFlag flags; // m_flags
            public PackageId packageId; // m_pkg_id
            public uint parentIndex; // m_parent_index
            public Dictionary<string, CheckpointExportData> checkpoint; // m_checkpoint

            public ExportPackageArgs(BinaryReader data) {
                name = data.ReadEncryptedString();
                baseName = data.ReadEncryptedString();
                checksum = data.ReadUInt32();
                size = data.ReadUInt32();
                flags = (TypeFlag)data.ReadUInt32();
                packageId = data.ReadPackageId();
                parentIndex = data.ReadUInt32();
                checkpoint = data.ReadDictionary(() => data.ReadEncryptedString(), () => new CheckpointExportData(data));
            }
        }

        public class CheckpointExportData {

            public uint offset; // m_offset
            public uint tag; // m_tag

            public CheckpointExportData(BinaryReader data) {
                offset = data.ReadUInt32();
                tag = data.ReadUInt32();
            }
        }

        public class ExportFunctionData {

            public string name; // m_name
            public FunctionId funcId; // m_fid
            public uint offset; // m_offset
            public uint size; // m_size
            public FuncFlag flags; // m_flags
            public List<VTableId> deps; // m_deps

            public ExportFunctionData(BinaryReader data) {
                name = data.ReadEncryptedString();
                funcId = new FunctionId(data.ReadUInt32());
                offset = data.ReadUInt32();
                size = data.ReadUInt32();
                flags = (FuncFlag)data.ReadUInt32();
                deps = data.ReadList(() => new VTableId(data.ReadUInt32()));
            }
        }

        public class VTableSection {

            public List<List<VTableId>> funcMapper; // m_funcMapper
            public List<List<uint>> vTable; // m_vtbl
            public List<PackageInfo> packageInfo; // m_pkgInfo
            public List<PackageId> packageIdMap; // m_pkgIdMap
            public Dictionary<string, PackageId> packageIdStrMap; // m_pkgIdStrMap

            public VTableSection(BinaryReader data) {
                funcMapper = data.ReadList(() => data.ReadList(() => new VTableId(data.ReadUInt32())));
                vTable = data.ReadList(() => data.ReadList(data.ReadUInt32));
                packageInfo = data.ReadList(() => new PackageInfo(data));
                packageIdMap = data.ReadList(data.ReadPackageId);
                packageIdStrMap = data.ReadDictionary(() => data.ReadEncryptedString(), data.ReadPackageId);
            }
        }

        public class PackageInfo {

            public uint size; // size
            public uint checksum; // checksum

            public PackageInfo(BinaryReader data) {
                // TODO: Size and checksum might be swapped
                size = data.ReadUInt32();
                checksum = data.ReadUInt32();
            }
        }

        public class FunctionLocationInfo {

            public string functionName; // FunctionName
            public uint offset; // Offset

            public FunctionLocationInfo(BinaryReader data) {
                functionName = data.ReadNullTermString();
                offset = data.ReadUInt32();
            }
        }

        public class OriginalSourceFileInfo {

            public uint fileName; // FilenameIdx
            public string text; // Text

            public OriginalSourceFileInfo(BinaryReader data) {
                // TODO: fileName and text might be swapped
                fileName = data.ReadUInt32();
                text = data.ReadNullTermString();
            }
        }

        public class PLineOffsetList {

            public string sourceFileName; // SourceFilename
            public List<PLineOffsetInfo> lineOffsets;

            public PLineOffsetList(BinaryReader data) {
                sourceFileName = data.ReadEncryptedString();
                lineOffsets = data.ReadList(() => new PLineOffsetInfo(data));
            }
        }

        public class PLineOffsetInfo {

            public uint offset; // Offset
            public uint lineNum; // LineNum

            public PLineOffsetInfo(BinaryReader data) {
                offset = data.ReadUInt32();
                lineNum = data.ReadUInt32();
            }
        }

        public class FrameDebugInfo {

            public enum FrameType : uint {
                UNDEF = 0,
                FUNCTION = 1,
                PACKAGE = 2,
            }

            public string name; // m_name
            public FrameType type; // m_type
            public uint size; // m_size
            public List<FrameMemberDebugInfo> members; // m_refMembers

            public FrameDebugInfo(BinaryReader data) {
                name = data.ReadEncryptedString();
                type = (FrameType)data.ReadUInt32();
                size = data.ReadUInt32();
                members = data.ReadList(() => new FrameMemberDebugInfo(data));
            }
        }

        public class FrameMemberDebugInfo {

            // Enum FrameMemberType
            public enum FrameMemberType : uint {
                UNDEF = 0,
                VOID = 1,
                INT = 2,
                FLOAT = 3,
                MIXED = 4,
                TIME = 5,
                TYPE_NAME = 6,
                STRING = 7,
                ENUM = 8,
                PACKAGE = 9,
            }

            public uint offset; // Offset
            public FrameMemberType type; // Type
            public VarFlag flags; // Flags
            public string name; // Name
            public string typeName; // TypeName

            public FrameMemberDebugInfo(BinaryReader data) {
                offset = data.ReadUInt32();
                type = (FrameMemberType)data.ReadUInt32();
                flags = (VarFlag)data.ReadUInt32();
                name = data.ReadEncryptedString();
                typeName = data.ReadEncryptedString();
            }
        }

        public byte[] magic;
        public ushort unk1;
        public uint byteStreamVersion;
        public VersionTable versionInfo; // m_version
        public OpcodeStream opcodeStream; // m_opcodeStream
        public StringLitTable stringLitTable; // m_strtbl
        public List<ImportData> imports; // m_imptbl
        public List<ExportData> exports; // m_exptbl
        public VTableSection vTable; // m_vtbl
        public Dictionary<FunctionId, uint> validEvents; // m_validEvents
        public List<FunctionLocationInfo> funcLocs; // m_functionLocs
        public List<OriginalSourceFileInfo> originalSourceText; // m_originalSourceText
        public List<PLineOffsetList> lineOffsets; // m_lineOffsets
        public List<FrameDebugInfo> frames; // m_frames

        public ByteStream(BinaryReader data) {
            magic = data.ReadBytes(2);
            unk1 = data.ReadUInt16();
            byteStreamVersion = data.ReadUInt32();
            while (data.BaseStream.Position < data.BaseStream.Length) {
                SectionType sectionType = (SectionType)data.ReadUInt32();
                uint sectionSize = data.ReadUInt32();
                switch (sectionType) {
                    case SectionType.VERSION_INFO:
                        versionInfo = new VersionTable(data);
                        break;
                    case SectionType.OPCODE:
                        opcodeStream = new OpcodeStream(data);
                        break;
                    case SectionType.STRING_LIT_TABLE:
                        stringLitTable = new StringLitTable(data);
                        break;
                    case SectionType.IMPORT_TABLE:
                        imports = data.ReadList(() => new ImportData(data));
                        break;
                    case SectionType.EXPORT_TABLE:
                        exports = data.ReadList(() => new ExportData(data));
                        break;
                    case SectionType.VTABLE_INFO:
                        vTable = new VTableSection(data);
                        break;
                    case SectionType.VALID_EVENT_TABLE:
                        validEvents = data.ReadDictionary(() => new FunctionId(data.ReadUInt32()), data.ReadUInt32);
                        break;
                    case SectionType.FUNCTION_LOC_DEBUG:
                        funcLocs = new List<FunctionLocationInfo>();
                        long startPos = data.BaseStream.Position;
                        while ((data.BaseStream.Position - startPos) < sectionSize) {
                            funcLocs.Add(new FunctionLocationInfo(data));
                        }
                        break;
                    case SectionType.SOURCE_FILE_DEBUG:
                        originalSourceText = data.ReadList(() => new OriginalSourceFileInfo(data));
                        break;
                    case SectionType.LINE_NUM_DEBUG:
                        lineOffsets = data.ReadList(() => new PLineOffsetList(data));
                        break;
                    case SectionType.FRAME_DEBUG_INFO:
                        frames = data.ReadList(() => new FrameDebugInfo(data));
                        break;
                    default:
                        Log.Warning($"Unhandled ByteStream section: {sectionType}");
                        data.BaseStream.Seek(sectionSize, SeekOrigin.Current);
                        break;
                }
            }
        }
    }
}
