//#define DEEPTRACE

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AC2RE.Definitions;

public class HeapObjectRegistry {

    public struct HeapObjectMeta {

        public ReferenceId id;
        public ReferenceEntry referenceEntry;
        public IHeapObject heapObject;
    }

    private readonly Dictionary<IHeapObject, HeapObjectMeta> registryByHeapObject = new();
    private readonly Dictionary<ReferenceId, HeapObjectMeta> registryById = new();
    private uint referenceIdCounter;

    private readonly List<Action> resolvers = new();
    private readonly List<StackTrace> resolverDebugs = new();
    public readonly List<IHeapObject> references = new();

    public bool contains(IHeapObject heapObject) {
        return registryByHeapObject.ContainsKey(heapObject);
    }

    public bool contains(ReferenceId referenceId) {
        return registryById.ContainsKey(referenceId);
    }

    public HeapObjectMeta register(IHeapObject heapObject) {
        ReferenceId referenceId = new(referenceIdCounter);
        referenceIdCounter++;

        return register(referenceId, heapObject);
    }

    public HeapObjectMeta register(ReferenceId referenceId, IHeapObject heapObject) {
        return register(referenceId, heapObject, HeapObjectManager.getReferenceEntry(heapObject.GetType()));
    }

    public HeapObjectMeta register(ReferenceId referenceId, IHeapObject heapObject, ReferenceEntry referenceEntry) {
        HeapObjectMeta meta = new() {
            id = referenceId,
            referenceEntry = referenceEntry,
            heapObject = heapObject,
        };
        registryByHeapObject[heapObject] = meta;
        registryById[referenceId] = meta;
        return meta;
    }

    public ReferenceId getId(IHeapObject heapObject) {
        if (heapObject == null) {
            return ReferenceId.NULL;
        }

        return getMeta(heapObject).id;
    }

    public T get<T>(ReferenceId referenceId) where T : IHeapObject {
        if (referenceId == ReferenceId.NULL) {
            return default;
        }

        return (T)getMeta(referenceId).heapObject;
    }

    public HeapObjectMeta getMeta(IHeapObject heapObject) {
        if (heapObject == null) {
            return default;
        }

        if (!registryByHeapObject.TryGetValue(heapObject, out HeapObjectMeta meta)) {
            meta = register(heapObject);
        }
        return meta;
    }

    public HeapObjectMeta getMeta(ReferenceId referenceId) {
        if (referenceId == ReferenceId.NULL) {
            return default;
        }

        if (registryById.TryGetValue(referenceId, out HeapObjectMeta meta)) {
            return meta;
        }
        throw new KeyNotFoundException();
    }

    public void addResolver(Action resolver) {
        resolvers.Add(resolver);
#if DEEPTRACE
        resolverDebugs.Add(new(true));
#endif
    }

    public void executeResolvers() {
        // Reverse resolvers to ensure we convert the nested heap objects before the parents
        resolvers.Reverse();
#if DEEPTRACE
        resolverDebugs.Reverse();
#endif
        for (int i = 0; i < resolvers.Count; i++) {
#if DEEPTRACE
            StackTrace resolverDebug = resolverDebugs[i];
#endif
            resolvers[i].Invoke();
        }
        resolvers.Clear();
#if DEEPTRACE
        resolverDebugs.Clear();
#endif
    }
}
