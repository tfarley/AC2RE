using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EntityGroupDesc {

    // EntityGroupDesc
    public List<EntityDesc> entities; // m_entities
    public List<EntityLinkDesc> links; // m_links

    public EntityGroupDesc(AC2Reader data) {
        uint unk1 = data.ReadUInt32();
        entities = data.ReadList(() => new EntityDesc(data));
        links = data.ReadList(() => new EntityLinkDesc(data));
    }
}
