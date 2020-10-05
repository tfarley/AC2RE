using System.Collections.Generic;

namespace AC2E.Utils {

    public static class DictionaryExtensions {

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TKey : notnull where TValue : new() {
            TValue val;

            if (!dict.TryGetValue(key, out val)) {
                val = new TValue();
                dict.Add(key, val);
            }

            return val;
        }
    }
}
