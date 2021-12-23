using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AC2RE.Utils;

public static class Util {

    public static int indexOfPattern(byte[] array, int start, byte[] pattern) {
        int maxStart = array.Length - pattern.Length;
        byte patternFirstValue = pattern[0];

        for (; start <= maxStart; start++) {
            if (array[start] == patternFirstValue) {
                for (int matchLen = 1; ; matchLen++) {
                    if (matchLen == pattern.Length) {
                        return start;
                    } else if (array[start + matchLen] != pattern[matchLen]) {
                        break;
                    }
                }
            }
        }

        return -1;
    }

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

    public enum PropertyInclusion {
        NONE,
        WRITABLE,
        ALL,
    }

    public static string objectToString(object? target, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, PropertyInclusion propertyInclusion = PropertyInclusion.ALL) {
        StringBuilder stringBuilder = new();
        objectToString(stringBuilder, new(), 0, target, bindingFlags, propertyInclusion);
        return stringBuilder.ToString();
    }

    private static void objectToString(StringBuilder stringBuilder, HashSet<object> visited, int indentLevel, object? target, BindingFlags bindingFlags, PropertyInclusion propertyInclusion) {
        HashSet<object> levelVisited = new(visited);

        if (target == null) {
            stringBuilder.Append("null");
            return;
        }

        Type targetType = target.GetType();

        if (target is IDelegateToString delegateToStringTarget) {
            objectToString(stringBuilder, levelVisited, indentLevel, delegateToStringTarget.delegatedToStringObject, bindingFlags, propertyInclusion);
            return;
        }

        if (targetType.IsPrimitive) {
            stringBuilder.Append(target.ToString());
            return;
        }

        if (target is Enum) {
            string? enumString = target.ToString();
            if (enumString != null && enumString.Length > 0) {
                char firstDigit = enumString[0];
                if ((char.IsDigit(firstDigit) || firstDigit == '-') && targetType.GetCustomAttribute<FlagsAttribute>() != null) {
                    // TODO: Cache the valid flags mask per-enum, maybe turn the object printer into a stateful object for sharing
                    ulong validFlagsMask = 0;
                    Type enumUnderlyingType = Enum.GetUnderlyingType(targetType);
                    ulong enumAllValue;
                    if (enumUnderlyingType == typeof(byte)) {
                        enumAllValue = byte.MaxValue;
                    } else if (enumUnderlyingType == typeof(ushort)) {
                        enumAllValue = ushort.MaxValue;
                    } else if (enumUnderlyingType == typeof(uint)) {
                        enumAllValue = uint.MaxValue;
                    } else if (enumUnderlyingType == typeof(ulong)) {
                        enumAllValue = ulong.MaxValue;
                    } else {
                        throw new NotImplementedException(enumUnderlyingType.ToString());
                    }
                    foreach (var value in Enum.GetValues(targetType)) {
                        ulong validValue = Convert.ToUInt64(value);
                        if (validValue != enumAllValue) {
                            validFlagsMask |= validValue;
                        }
                    }
                    ulong targetValue = Convert.ToUInt64(target);
                    ulong validValues = (targetValue & validFlagsMask);
                    ulong invalidValues = (targetValue & (~validFlagsMask));
                    enumString = $"{Enum.ToObject(targetType, validValues)} & Extra {invalidValues}";
                }
            }
            stringBuilder.Append(enumString);
            return;
        }

        if (target is string targetString) {
            stringBuilder.Append('"')
                .Append(targetString)
                .Append('"');
            return;
        }

        if (!targetType.IsValueType && !levelVisited.Add(target)) {
            stringBuilder.Append($"Circular ref: {target}");
            return;
        }

        if (target is IDictionary dictionaryTarget) {
            if (dictionaryTarget.Count > 0) {
                stringBuilder.AppendLine("{");
                foreach (DictionaryEntry entry in dictionaryTarget) {
                    stringBuilder.Append(' ', indentLevel + 2);
                    objectToString(stringBuilder, levelVisited, indentLevel + 2, entry.Key, bindingFlags, propertyInclusion);
                    stringBuilder.Append(" : ");
                    objectToString(stringBuilder, levelVisited, indentLevel + 2, entry.Value, bindingFlags, propertyInclusion);
                    stringBuilder.AppendLine();
                }
                stringBuilder.Append(' ', indentLevel)
                    .Append('}');
            } else {
                stringBuilder.Append("{ }");
            }
            return;
        }

        if (target is IEnumerable enumerableTarget) {
            if (((IEnumerable)target).GetEnumerator().MoveNext()) {
                stringBuilder.AppendLine("[");
                bool first = true;
                foreach (object? val in enumerableTarget) {
                    if (!first) {
                        stringBuilder.AppendLine(",");
                    }
                    stringBuilder.Append(' ', indentLevel + 2);
                    objectToString(stringBuilder, levelVisited, indentLevel + 2, val, bindingFlags, propertyInclusion);
                    first = false;
                }
                stringBuilder.AppendLine()
                    .Append(' ', indentLevel)
                    .Append(']');
            } else {
                stringBuilder.Append("[ ]");
            }
            return;
        }

        foreach (MethodInfo methodInfo in targetType.GetMethods()) {
            if (methodInfo.Name == "ToString" && methodInfo.DeclaringType == targetType) {
                string? str = target.ToString();
                if (str == null) {
                    stringBuilder.Append(str);
                    return;
                }
                using (StringReader toStringReader = new(str)) {
                    bool first = true;
                    string? line;
                    while ((line = toStringReader.ReadLine()) != null) {
                        if (!first) {
                            stringBuilder.AppendLine()
                                .Append(' ', indentLevel);
                        }
                        stringBuilder.Append(line);
                        first = false;
                    }
                }
                return;
            }
        }

        FieldInfo[] fieldInfos = targetType.GetFields(bindingFlags);
        PropertyInfo[] propertyInfos = propertyInclusion != PropertyInclusion.NONE ? targetType.GetProperties(bindingFlags) : Array.Empty<PropertyInfo>();
        if (fieldInfos.Length > 0 || propertyInfos.Length > 0) {
            stringBuilder.AppendLine("{");
            string fieldIndent = new(' ', indentLevel + 2);
            if (fieldInfos.Length > 0) {
                foreach (FieldInfo fieldInfo in fieldInfos) {
                    if (!fieldInfo.IsStatic) {
                        stringBuilder.Append($"{fieldIndent}{fieldInfo.Name} = ");
                        objectToString(stringBuilder, levelVisited, indentLevel + 2, fieldInfo.GetValue(target), bindingFlags, propertyInclusion);
                        stringBuilder.AppendLine();
                    }
                }
            }
            if (propertyInfos.Length > 0) {
                foreach (PropertyInfo propertyInfo in propertyInfos) {
                    if (propertyInclusion == PropertyInclusion.ALL || propertyInfo.CanWrite) {
                        stringBuilder.Append($"{fieldIndent}{propertyInfo.Name} = ");
                        objectToString(stringBuilder, levelVisited, indentLevel + 2, propertyInfo.GetValue(target), bindingFlags, propertyInclusion);
                        stringBuilder.AppendLine();
                    }
                }
            }
            stringBuilder.Append(' ', indentLevel)
                .Append('}');
        } else {
            stringBuilder.Append("{ }");
        }
    }
}
