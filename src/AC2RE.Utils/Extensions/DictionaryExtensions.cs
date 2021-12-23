using System.Collections.Generic;

namespace AC2RE.Utils;

public static class DictionaryExtensions {

    public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TKey : notnull where TValue : new() {
        if (!dict.TryGetValue(key, out TValue val)) {
            val = new();
            dict.Add(key, val);
        }

        return val;
    }
}
