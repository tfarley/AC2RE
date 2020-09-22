using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AC2E.Utils {

    public static class Util {

        public static byte[] hexStringToBytes(string hexStr) {
            hexStr = new Regex(@"[^0-9a-fA-F]|0x").Replace(hexStr, "");

            byte[] bytes = new byte[hexStr.Length / 2];

            for (int i = 0, byteIndex = 0; i < hexStr.Length; i += 2, byteIndex++) {
                byte highNibble = (byte)hexStr[i];
                byte lowNibble = (byte)hexStr[i + 1];
                bytes[byteIndex] = (byte)(((highNibble - (highNibble < 58 ? 48 : (highNibble < 97 ? 55 : 87))) << 4)
                    + (lowNibble - (lowNibble < 58 ? 48 : (lowNibble < 97 ? 55 : 87))));
            }

            return bytes;
        }

        public static ushort byteSwapped(ushort value) {
            return (ushort)(((value & 0x00FFU) << 8) | ((value & 0xFF00U) >> 8));
        }

        public static string objectToString(object target) {
            StringBuilder stringBuilder = new StringBuilder();
            objectToString(stringBuilder, new HashSet<object>(), 0, target);
            return stringBuilder.ToString();
        }

        private static void objectToString(StringBuilder stringBuilder, HashSet<object> visited, int indentLevel, object target) {
            HashSet<object> levelVisited = new HashSet<object>(visited);

            if (target == null) {
                stringBuilder.Append("null");
                return;
            }

            Type targetType = target.GetType();

            IDelegateToString delegateToString = target as IDelegateToString;
            if (delegateToString != null) {
                objectToString(stringBuilder, levelVisited, indentLevel, delegateToString.delegatedToStringObject);
                return;
            }

            if (targetType.IsPrimitive || target is Enum) {
                stringBuilder.Append(target.ToString());
                return;
            }

            if (!targetType.IsValueType && !levelVisited.Add(target)) {
                stringBuilder.Append($"Circular ref: {target}");
                return;
            }

            IDictionary dictionaryValue = target as IDictionary;
            if (dictionaryValue != null) {
                if (dictionaryValue.Count > 0) {
                    stringBuilder.AppendLine("{");
                    foreach (DictionaryEntry entry in dictionaryValue) {
                        stringBuilder.Append(' ', indentLevel + 2);
                        objectToString(stringBuilder, levelVisited, indentLevel + 2, entry.Key);
                        stringBuilder.Append(" : ");
                        objectToString(stringBuilder, levelVisited, indentLevel + 2, entry.Value);
                        stringBuilder.AppendLine();
                    }
                    stringBuilder.Append(' ', indentLevel);
                    stringBuilder.Append('}');
                } else {
                    stringBuilder.Append("{ }");
                }
                return;
            }

            IEnumerable enumerableValue = target as IEnumerable;
            if (enumerableValue != null && !(target is string)) {
                if (((IEnumerable)target).GetEnumerator().MoveNext()) {
                    stringBuilder.AppendLine("[");
                    bool first = true;
                    foreach (object val in enumerableValue) {
                        if (!first) {
                            stringBuilder.AppendLine(",");
                        }
                        stringBuilder.Append(' ', indentLevel + 2);
                        objectToString(stringBuilder, levelVisited, indentLevel + 2, val);
                        first = false;
                    }
                    stringBuilder.AppendLine();
                    stringBuilder.Append(' ', indentLevel);
                    stringBuilder.Append(']');
                } else {
                    stringBuilder.Append("[ ]");
                }
                return;
            }

            foreach (MethodInfo methodInfo in targetType.GetMethods()) {
                if (methodInfo.Name == "ToString" && methodInfo.DeclaringType == targetType) {
                    using (StringReader toStringReader = new StringReader(target.ToString())) {
                        bool first = true;
                        string line;
                        while ((line = toStringReader.ReadLine()) != null) {
                            if (!first) {
                                stringBuilder.AppendLine();
                                stringBuilder.Append(' ', indentLevel);
                            }
                            stringBuilder.Append(line);
                            first = false;
                        }
                    }
                    return;
                }
            }

            FieldInfo[] fieldInfos = targetType.GetFields();
            if (fieldInfos.Length > 0) {
                stringBuilder.AppendLine("{");
                string fieldIndent = new string(' ', indentLevel + 2);
                foreach (FieldInfo fieldInfo in fieldInfos) {
                    stringBuilder.Append($"{fieldIndent}{fieldInfo.Name} = ");
                    objectToString(stringBuilder, levelVisited, indentLevel + 2, fieldInfo.GetValue(target));
                    stringBuilder.AppendLine();
                }
                stringBuilder.Append(' ', indentLevel);
                stringBuilder.Append('}');
            } else {
                stringBuilder.Append("{ }");
            }
        }

        public static Quaternion quaternionFromAxisAngleLeftHanded(Vector3 axis, float angle) {
            return Quaternion.CreateFromAxisAngle(-axis, angle);
        }
    }
}
