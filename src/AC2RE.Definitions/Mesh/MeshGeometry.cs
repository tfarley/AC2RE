using System.Collections.Generic;

namespace AC2RE.Definitions;

public class MeshGeometry {

    // MeshGeometry
    public uint meshType; // mesh_type
    public readonly List<ushort> indices = new();

    public MeshGeometry(uint meshType, AC2Reader data) {
        this.meshType = meshType;
        // TODO: Correct names and parsing
        uint numSubMeshes = data.ReadUInt32();
        for (int i = 0; i < numSubMeshes; i++) {
            uint unk1 = data.ReadUInt32();
            uint unk2 = data.ReadUInt32();

            // TODO: Is this CVertexSetData?
            // TODO: numMapEntries seems to equal the number of materialDids in CBaseMesh, so maybe key is material index and it would be better to keep as a map instead of shoving all indices into same list
            uint numMapEntries = data.ReadUInt32();
            for (int j = 0; j < numMapEntries; j++) {
                uint mapKey = data.ReadUInt32();
                uint numIndices = data.ReadUInt32();
                for (int k = 0; k < numIndices; k++) {
                    indices.Add(data.ReadUInt16());
                }
                data.Align(4);
            }
        }
        // TODO: Check to see if there is more to parse here
    }
}
