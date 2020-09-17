using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace AC2E.Def {

    public static class InterpMeta {

        private static readonly Dictionary<Type, FieldDesc[]> fieldDescCache = new Dictionary<Type, FieldDesc[]>();

        private static readonly Dictionary<Type, Type> TYPE_REPLACEMENTS = new Dictionary<Type, Type> {
            { typeof(Vector3), typeof(VectorPkg) },
        };

        private static readonly Dictionary<Type, uint> TYPE_TO_NUM_WORDS = new Dictionary<Type, uint> {
            { typeof(bool), 1 },
            { typeof(byte), 1 },
            { typeof(short), 1 },
            { typeof(ushort), 1 },
            { typeof(int), 1 },
            { typeof(uint), 1 },
            { typeof(float), 1 },
            { typeof(IPackage), 1 },
            { typeof(DataId), 1 },
            { typeof(CellId), 1 },

            { typeof(long), 2 },
            { typeof(ulong), 2 },
            { typeof(double), 2 },
            { typeof(InstanceId), 2 },
        };

        private static void addFieldsInOrder(Type type, List<FieldInfo> orderedFieldInfos) {
            if (type.BaseType != null) {
                addFieldsInOrder(type.BaseType, orderedFieldInfos);
            }

            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            // Undocumented way to sort in declaration order
            Array.Sort(fieldInfos, (f1, f2) => f1.MetadataToken.CompareTo(f2.MetadataToken));
            foreach (FieldInfo fieldInfo in fieldInfos) {
                if (fieldInfo.GetCustomAttribute<PackageIgnoreAttribute>() != null) {
                    continue;
                }
                orderedFieldInfos.Add(fieldInfo);
            }
        }

        public static FieldDesc[] getFieldDescs(Type type) {
            if (!fieldDescCache.TryGetValue(type, out FieldDesc[] fieldDescs)) {
                List<FieldInfo> orderedFieldInfos = new List<FieldInfo>();
                addFieldsInOrder(type, orderedFieldInfos);
                fieldDescs = new FieldDesc[orderedFieldInfos.Count];
                for (int i = 0; i < orderedFieldInfos.Count; i++) {
                    FieldInfo fieldInfo = orderedFieldInfos[i];
                    Type fieldType = fieldInfo.FieldType;
                    fieldType = TYPE_REPLACEMENTS.GetValueOrDefault(fieldType, fieldType);
                    StackType stackType;
                    if (typeof(IPackage).IsAssignableFrom(fieldType)) {
                        fieldType = typeof(IPackage);
                        stackType = StackType.REFERENCE;
                    } else {
                        if (typeof(Enum).IsAssignableFrom(fieldType)) {
                            fieldType = Enum.GetUnderlyingType(fieldType);
                        }
                        stackType = StackType.UNDEF;
                    }
                    fieldDescs[i] = new FieldDesc(stackType, TYPE_TO_NUM_WORDS[fieldType]);
                }
                fieldDescCache[type] = fieldDescs;
            }
            return fieldDescs;
        }
    }
}
