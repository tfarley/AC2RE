using AC2E.Def.Extensions;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Interp {

    public class ByteStream {

        public static readonly int BS_VERSION_NUM = 15;

        public class VersionTable {

            public List<uint> versions;

            public VersionTable(BinaryReader data) {
                versions = data.ReadList(data.ReadUInt32);
            }
        }

        public class OpcodeStream {

            public uint bufSize;
            public byte[] opcodeBytes;

            public OpcodeStream(BinaryReader data) {
                bufSize = data.ReadUInt32();
                opcodeBytes = data.ReadBytes((int)bufSize);
            }
        }

        public class StringLitTable {

            public List<string> strings;
            public List<List<uint>> offsets;

            public StringLitTable(BinaryReader data) {
                strings = data.ReadList(() => data.ReadEncryptedString(Encoding.Unicode));
                offsets = data.ReadList(() => data.ReadList(data.ReadUInt32));
            }
        }

        public class ImportData {

            public string packageName;
            public List<ImportSymbolInfo> symbols;
            public List<uint> packageOffsets;

            public ImportData(BinaryReader data) {
                packageName = data.ReadEncryptedString();
                symbols = data.ReadList(() => new ImportSymbolInfo(data));
                packageOffsets = data.ReadList(data.ReadUInt32);
            }
        }

        public class ImportSymbolInfo {

            public string name;
            public List<uint> offsets;
            public uint fromAddr;

            public ImportSymbolInfo(BinaryReader data) {
                name = data.ReadEncryptedString();
                offsets = data.ReadList(data.ReadUInt32);
                fromAddr = data.ReadUInt32(); // TODO: Might be a bool
            }
        }

        public class ExportData {

            public ExportPackageArgs args;
            public List<ExportFunctionData> funcs;

            public ExportData(BinaryReader data) {
                args = new ExportPackageArgs(data);
                funcs = data.ReadList(() => new ExportFunctionData(data));
            }
        }

        public class ExportPackageArgs {

            public string name;
            public string baseName;
            public uint checksum;
            public uint size;
            public TypeFlag flags;
            public uint packageId;
            public uint parentIndex;
            public Dictionary<string, CheckpointExportData> checkpoint;

            public ExportPackageArgs(BinaryReader data) {
                name = data.ReadEncryptedString();
                baseName = data.ReadEncryptedString();
                checksum = data.ReadUInt32();
                size = data.ReadUInt32();
                flags = (TypeFlag)data.ReadUInt32();
                packageId = data.ReadUInt32();
                parentIndex = data.ReadUInt32();
                checkpoint = data.ReadDictionary(() => data.ReadEncryptedString(), () => new CheckpointExportData(data));
            }
        }

        public class CheckpointExportData {

            public uint offset;
            public uint tag;

            public CheckpointExportData(BinaryReader data) {
                offset = data.ReadUInt32();
                tag = data.ReadUInt32();
            }
        }

        public class ExportFunctionData {

            public string name;
            public FunctionId funcId;
            public uint offset;
            public uint size;
            public FuncFlag flags;
            public List<VTableId> deps;

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

            public List<List<VTableId>> funcMapper;
            public List<List<uint>> vTable;
            public List<PackageInfo> packageInfo;
            public List<uint> packageIdMap;
            public Dictionary<string, uint> packageIdStrMap;

            public VTableSection(BinaryReader data) {
                funcMapper = data.ReadList(() => data.ReadList(() => new VTableId(data.ReadUInt32())));
                vTable = data.ReadList(() => data.ReadList(data.ReadUInt32));
                packageInfo = data.ReadList(() => new PackageInfo(data));
                packageIdMap = data.ReadList(data.ReadUInt32);
                packageIdStrMap = data.ReadDictionary(() => data.ReadEncryptedString(), data.ReadUInt32);
            }
        }

        public class PackageInfo {

            public uint size;
            public uint checksum;

            public PackageInfo(BinaryReader data) {
                // TODO: Size and checksum might be swapped
                size = data.ReadUInt32();
                checksum = data.ReadUInt32();
            }
        }

        public class FunctionLocationInfo {

            public string functionName;
            public uint offset;

            public FunctionLocationInfo(BinaryReader data) {
                functionName = data.ReadNullTermString();
                offset = data.ReadUInt32();
            }
        }

        public class OriginalSourceFileInfo {

            public string fileName;
            public string text;

            public OriginalSourceFileInfo(BinaryReader data) {
                // TODO: FileName and text might be swapped
                fileName = data.ReadNullTermString();
                text = data.ReadNullTermString();
            }
        }

        public class LineOffsetList {

            public string sourceFilename;
            public List<LineOffsetInfo> lineOffsets;

            public LineOffsetList(BinaryReader data) {
                sourceFilename = data.ReadEncryptedString();
                lineOffsets = data.ReadList(() => new LineOffsetInfo(data));
            }
        }

        public class LineOffsetInfo {

            public uint offset;
            public uint lineNum;

            public LineOffsetInfo(BinaryReader data) {
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

            public string name;
            public FrameType type;
            public uint size;
            public List<FrameMemberDebugInfo> members;

            public FrameDebugInfo(BinaryReader data) {
                name = data.ReadEncryptedString();
                type = (FrameType)data.ReadUInt32();
                size = data.ReadUInt32();
                members = data.ReadList(() => new FrameMemberDebugInfo(data));
            }
        }

        public class FrameMemberDebugInfo {

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

            public uint offset;
            public FrameMemberType type;
            public VarFlag flags;
            public string name;
            public string typeName;

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
        public VersionTable versionInfo;
        public OpcodeStream opcodeStream;
        public StringLitTable stringLitTable;
        public List<ImportData> imports;
        public List<ExportData> exports;
        public VTableSection vTable;
        public Dictionary<FunctionId, uint> validEvents;
        public List<FunctionLocationInfo> funcLocs;
        public List<OriginalSourceFileInfo> originalSourceText;
        public List<LineOffsetList> lineOffsets;
        public List<FrameDebugInfo> frames;

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
                        lineOffsets = data.ReadList(() => new LineOffsetList(data));
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
