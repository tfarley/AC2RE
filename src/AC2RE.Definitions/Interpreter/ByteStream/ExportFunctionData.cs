﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ExportFunctionData {

    // ExportFunctionData
    public string name; // m_name
    public FunctionId funcId; // m_fid
    public uint offset; // m_offset
    public uint size; // m_size
    public FuncFlag flags; // m_flags
    public List<VTableId> deps; // m_deps

    public ExportFunctionData(AC2Reader data) {
        name = data.ReadString();
        funcId = new(data.ReadUInt32());
        offset = data.ReadUInt32();
        size = data.ReadUInt32();
        flags = data.ReadEnum<FuncFlag>();
        deps = data.ReadList(() => new VTableId(data.ReadUInt32()));
    }
}
