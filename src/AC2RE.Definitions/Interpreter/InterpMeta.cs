using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace AC2RE.Definitions;

public static class InterpMeta {

    private static readonly Dictionary<Type, FieldDesc[]> fieldDescCache = new();
    private static readonly Dictionary<Type, uint> sizeCache = new();

    private static readonly Dictionary<Type, Type> TYPE_REPLACEMENTS = new() {
        { typeof(DataId), typeof(uint) },
        { typeof(CellId), typeof(uint) },
        { typeof(EnumId), typeof(uint) },
        { typeof(EffectId), typeof(uint) },
        { typeof(QuestUpdateType), typeof(uint) },

        { typeof(InstanceId), typeof(ulong) },
    };

    private static readonly HashSet<Type> HEAP_OBJECT_TYPES = new() {
        typeof(IHeapObject),
        typeof(IEnumerable),
        typeof(Vector3),
    };

    private static readonly Dictionary<Type, uint> TYPE_TO_NUM_WORDS = new() {
        { typeof(bool), 1 },
        { typeof(byte), 1 },
        { typeof(short), 1 },
        { typeof(ushort), 1 },
        { typeof(int), 1 },
        { typeof(uint), 1 },
        { typeof(float), 1 },
        { typeof(IHeapObject), 1 },

        { typeof(long), 2 },
        { typeof(ulong), 2 },
        { typeof(double), 2 },
    };

    private static void addFieldsInOrder(Type type, List<FieldInfo> orderedFieldInfos) {
        if (type.BaseType != null) {
            addFieldsInOrder(type.BaseType, orderedFieldInfos);
        }

        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        // Undocumented way to sort in declaration order
        Array.Sort(fieldInfos, (f1, f2) => f1.MetadataToken.CompareTo(f2.MetadataToken));
        foreach (FieldInfo fieldInfo in fieldInfos) {
            if (fieldInfo.GetCustomAttribute<HeapObjectIgnoreAttribute>() != null) {
                continue;
            }
            orderedFieldInfos.Add(fieldInfo);
        }
    }

    public static FieldDesc[] getFieldDescs(Type type) {
        if (!fieldDescCache.TryGetValue(type, out FieldDesc[] fieldDescs)) {
            List<FieldInfo> orderedFieldInfos = new();
            addFieldsInOrder(type, orderedFieldInfos);
            fieldDescs = new FieldDesc[orderedFieldInfos.Count];
            uint totalSize = 0;
            for (int i = 0; i < orderedFieldInfos.Count; i++) {
                FieldInfo fieldInfo = orderedFieldInfos[i];
                Type fieldType = fieldInfo.FieldType;
                foreach ((Type originalType, Type replacementType) in TYPE_REPLACEMENTS) {
                    if (originalType.IsAssignableFrom(fieldType)) {
                        fieldType = replacementType;
                        break;
                    }
                }

                StackType stackType = StackType.Undef;
                bool isHeapObjectType = false;
                foreach (Type heapObjectType in HEAP_OBJECT_TYPES) {
                    if (heapObjectType.IsAssignableFrom(fieldType)) {
                        fieldType = typeof(IHeapObject);
                        stackType = StackType.Reference;
                        isHeapObjectType = true;
                        break;
                    }
                }
                if (!isHeapObjectType) {
                    if (typeof(Enum).IsAssignableFrom(fieldType)) {
                        fieldType = Enum.GetUnderlyingType(fieldType);
                    }
                    stackType = StackType.Undef;
                }
                uint size = TYPE_TO_NUM_WORDS[fieldType];
                totalSize += size;
                fieldDescs[i] = new(stackType, size);
            }
            fieldDescCache[type] = fieldDescs;
            sizeCache[type] = totalSize;
        }
        return fieldDescs;
    }

    public static uint getSize(Type type) {
        FieldDesc[] _ = getFieldDescs(type);
        return sizeCache[type];
    }
}
