namespace AC2RE.Definitions;

public class ReferenceIdWrapper {

    // ReferenceIDWrapper
    public ReferenceId id; // m_id
    public uint cachedHashValue; // m_CachedHashValue
    public bool useCache; // m_bUseCache

    public ReferenceIdWrapper(ReferenceId id) {
        this.id = id;
    }

    public ReferenceIdWrapper(AC2Reader data) {
        id = data.ReadReferenceId();
        cachedHashValue = data.ReadUInt32();
        useCache = data.ReadBoolean();
    }

    public void write(AC2Writer data) {
        data.WriteHO(data.heapObjectRegistry.get<IHeapObject>(id));
        data.Write(cachedHashValue);
        data.Write(useCache);
    }
}
