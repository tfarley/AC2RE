using System;
using System.Collections;
using System.Collections.Generic;
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
            if (target == null) {
                stringBuilder.Append("null");
                return;
            }

            Type targetType = target.GetType();

            if (targetType.IsPrimitive || target is Enum) {
                stringBuilder.Append(target.ToString());
                return;
            }

            IDictionary dictionaryValue = target as IDictionary;
            if (dictionaryValue != null) {
                if (!visited.Add(target)) {
                    stringBuilder.Append($"Circular ref: {target}");
                    return;
                }

                stringBuilder.AppendLine("{");
                foreach (DictionaryEntry entry in dictionaryValue) {
                    stringBuilder.Append(' ', indentLevel + 2);
                    int startLen = stringBuilder.Length;
                    objectToString(stringBuilder, visited, indentLevel + 2, entry.Key);
                    stringBuilder.Append(" : ");
                    objectToString(stringBuilder, visited, indentLevel + 2 + (stringBuilder.Length - startLen), entry.Value);
                    stringBuilder.AppendLine();
                }
                stringBuilder.Append(' ', indentLevel);
                stringBuilder.Append('}');
                return;
            }

            IEnumerable enumerableValue = target as IEnumerable;
            if (enumerableValue != null && !(target is string)) {
                if (!visited.Add(target)) {
                    stringBuilder.Append($"Circular ref: {target}");
                    return;
                }

                stringBuilder.AppendLine("[");
                bool first = true;
                foreach (object val in enumerableValue) {
                    if (!first) {
                        stringBuilder.AppendLine(",");
                    }
                    stringBuilder.Append(' ', indentLevel + 2);
                    objectToString(stringBuilder, visited, indentLevel + 2, val);
                    first = false;
                }
                stringBuilder.AppendLine();
                stringBuilder.Append(' ', indentLevel);
                stringBuilder.Append(']');
                return;
            }

            foreach (MethodInfo methodInfo in targetType.GetMethods()) {
                if (methodInfo.Name == "ToString" && methodInfo.DeclaringType == targetType) {
                    stringBuilder.Append(target.ToString());
                    return;
                }
            }

            if (!visited.Add(target)) {
                stringBuilder.Append($"Circular ref: {target}");
                return;
            }

            FieldInfo[] fieldInfos = targetType.GetFields();
            stringBuilder.AppendLine("{");
            string fieldIndent = new string(' ', indentLevel + 2);
            foreach (FieldInfo fieldInfo in fieldInfos) {
                string fieldLine = $"{fieldIndent}{fieldInfo.Name} = ";
                stringBuilder.Append(fieldLine);
                objectToString(stringBuilder, visited, fieldLine.Length, fieldInfo.GetValue(target));
                stringBuilder.AppendLine();
            }
            stringBuilder.Append(' ', indentLevel);
            stringBuilder.Append('}');
        }
    }
}
