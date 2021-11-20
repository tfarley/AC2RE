using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace AC2RE.Definitions {

    public static class InterpMeta {

        private static readonly Dictionary<Type, FieldDesc[]> fieldDescCache = new();

        private static readonly Dictionary<Type, Type> TYPE_REPLACEMENTS = new() {
            { typeof(DataId), typeof(uint) },
            { typeof(CellId), typeof(uint) },
            { typeof(EnumId), typeof(uint) },
            { typeof(EffectId), typeof(uint) },
            { typeof(QuestUpdateType), typeof(uint) },

            { typeof(InstanceId), typeof(ulong) },
        };

        private static readonly HashSet<Type> PACKAGE_TYPES = new() {
            typeof(IPackage),
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
            { typeof(IPackage), 1 },

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
                if (fieldInfo.GetCustomAttribute<PackageIgnoreAttribute>() != null) {
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
                    bool isPackageType = false;
                    foreach (Type packageType in PACKAGE_TYPES) {
                        if (packageType.IsAssignableFrom(fieldType)) {
                            fieldType = typeof(IPackage);
                            stackType = StackType.Reference;
                            isPackageType = true;
                            break;
                        }
                    }
                    if (!isPackageType) {
                        if (typeof(Enum).IsAssignableFrom(fieldType)) {
                            fieldType = Enum.GetUnderlyingType(fieldType);
                        }
                        stackType = StackType.Undef;
                    }
                    fieldDescs[i] = new(stackType, TYPE_TO_NUM_WORDS[fieldType]);
                }
                fieldDescCache[type] = fieldDescs;
            }
            return fieldDescs;
        }
    }
}
