using AC2E.Dat;
using AC2E.Def;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AC2E.Interp {

    public static class InterpMeta {

        private static readonly Dictionary<Type, FieldDesc[]> fieldDescCache = new Dictionary<Type, FieldDesc[]>();
        private static readonly Dictionary<Type, bool> hasReferencesCache = new Dictionary<Type, bool>();

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
            { typeof(long), 2 },
            { typeof(ulong), 2 },
            { typeof(double), 2 },
            { typeof(InstanceId), 2 },

            { typeof(TextType), 1 },
        };

        public static FieldDesc[] getFieldDescs(Type type) {
            if (!fieldDescCache.TryGetValue(type, out FieldDesc[] fieldDescs)) {
                FieldInfo[] fieldInfos = type.GetFields();
                fieldDescs = new FieldDesc[fieldInfos.Length];
                for (int i = 0; i < fieldInfos.Length; i++) {
                    FieldInfo fieldInfo = fieldInfos[i];
                    Type fieldType = fieldInfo.FieldType;
                    StackType stackType;
                    if (typeof(IPackage).IsAssignableFrom(fieldType)) {
                        fieldType = typeof(IPackage);
                        stackType = StackType.REFERENCE;
                    } else {
                        stackType = StackType.UNDEF;
                    }
                    fieldDescs[i] = new FieldDesc(stackType, TYPE_TO_NUM_WORDS[fieldType]);
                }
                fieldDescCache[type] = fieldDescs;
            }
            return fieldDescs;
        }

        public static bool hasReferences(Type type) {
            if (!hasReferencesCache.TryGetValue(type, out bool hasReferences)) {
                FieldInfo[] fieldInfos = type.GetFields();
                hasReferences = false;
                for (int i = 0; i < fieldInfos.Length; i++) {
                    FieldInfo fieldInfo = fieldInfos[i];
                    Type fieldType = fieldInfo.FieldType;
                    if (typeof(IPackage).IsAssignableFrom(fieldType)) {
                        hasReferences = true;
                        break;
                    }
                }
                hasReferencesCache[type] = hasReferences;
            }
            return hasReferences;
        }
    }
}
