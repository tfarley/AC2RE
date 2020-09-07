﻿using System;
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

        public static FieldDesc[] getFieldDescs(Type type) {
            if (!fieldDescCache.TryGetValue(type, out FieldDesc[] fieldDescs)) {
                FieldInfo[] fieldInfos = type.GetFields();
                fieldDescs = new FieldDesc[fieldInfos.Length];
                for (int i = 0; i < fieldInfos.Length; i++) {
                    FieldInfo fieldInfo = fieldInfos[i];
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
