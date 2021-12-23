using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2RE.Definitions;

public class ByteStream {

    public static readonly int BS_VERSION_NUM = 15;

    public class VersionTable {

        public List<uint> versions; // m_versions

        public VersionTable(AC2Reader data) {
            versions = data.ReadList(data.ReadUInt32);
        }
    }

    public class OpcodeStream {

        public uint size; // m_size
        public byte[] opcodeBytes; // m_pOpcodeBytes

        public OpcodeStream(AC2Reader data) {
            size = data.ReadUInt32();
            opcodeBytes = data.ReadBytes((int)size);
        }
    }

    public class StringLitTable {

        public List<string> strings; // m_strtbl
        public List<List<uint>> offsets; // m_offsets

        public StringLitTable(AC2Reader data) {
            strings = data.ReadList(() => data.ReadString(Encoding.Unicode));
            offsets = data.ReadList(() => data.ReadList(data.ReadUInt32));
        }
    }

    public class ImportData {

        public string packageName; // m_pkgName
        public List<ImportSymbolInfo> symbols; // m_syms
        public List<uint> packageOffsets; // m_pkgOffsets

        public ImportData(AC2Reader data) {
            packageName = data.ReadString();
            symbols = data.ReadList(() => new ImportSymbolInfo(data));
            packageOffsets = data.ReadList(data.ReadUInt32);
        }
    }

    public class ExportData {

        public ExportPackageArgs args; // m_args
        public List<ExportFunctionData> funcs; // m_funcs

        public ExportData(AC2Reader data) {
            args = new(data);
            funcs = data.ReadList(() => new ExportFunctionData(data));
        }
    }

    public class FunctionLocationInfo {

        public string functionName; // FunctionName
        public uint offset; // Offset

        public FunctionLocationInfo(AC2Reader data) {
            functionName = data.ReadNullTermString();
            offset = data.ReadUInt32();
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

    public ByteStream(AC2Reader data) {
        magic = data.ReadBytes(2);
        unk1 = data.ReadUInt16();
        byteStreamVersion = data.ReadUInt32();
        while (data.BaseStream.Position < data.BaseStream.Length) {
            SectionType sectionType = (SectionType)data.ReadUInt32();
            uint sectionSize = data.ReadUInt32();
            switch (sectionType) {
                case SectionType.VersionInfo:
                    versionInfo = new(data);
                    break;
                case SectionType.Opcode:
                    opcodeStream = new(data);
                    break;
                case SectionType.StringLitTable:
                    stringLitTable = new(data);
                    break;
                case SectionType.ImportTable:
                    imports = data.ReadList(() => new ImportData(data));
                    break;
                case SectionType.ExportTable:
                    exports = data.ReadList(() => new ExportData(data));
                    break;
                case SectionType.VTable:
                    vTable = new(data);
                    break;
                case SectionType.ValidEventTable:
                    validEvents = data.ReadDictionary(() => new FunctionId(data.ReadUInt32()), data.ReadUInt32);
                    break;
                case SectionType.FunctionLocDebug:
                    funcLocs = new();
                    long startPos = data.BaseStream.Position;
                    while ((data.BaseStream.Position - startPos) < sectionSize) {
                        funcLocs.Add(new(data));
                    }
                    break;
                case SectionType.SourceFileDebug:
                    originalSourceText = data.ReadList(() => new OriginalSourceFileInfo(data));
                    break;
                case SectionType.LineNumDebug:
                    lineOffsets = data.ReadList(() => new PLineOffsetList(data));
                    break;
                case SectionType.FrameDebugInfo:
                    frames = data.ReadList(() => new FrameDebugInfo(data));
                    break;
                default:
                    throw new InvalidDataException(sectionType.ToString());
            }
        }
    }
}
