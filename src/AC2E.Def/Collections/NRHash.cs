using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public class NRHash : Dictionary<IPackage, IPackage>, IPackage {

        public NativeType nativeType => NativeType.NRHASH;

        public Dictionary<K, V> to<K, V>() {
            return to(k => (K)k, v => (V)v);
        }

        public Dictionary<K, V> to<K, V>(Func<IPackage, K> keyConversion, Func<IPackage, V> valueConversion) {
            Dictionary<K, V> converted = new(Count);
            foreach ((var key, var value) in this) {
                converted[keyConversion.Invoke(key)] = valueConversion.Invoke(value);
            }
            return converted;
        }

        public static NRHash from<K, V>(Dictionary<K, V> source) where K : IPackage where V : IPackage {
            return from(source, k => k, v => v);
        }

        public static NRHash from<K, V>(Dictionary<K, V> source, Func<K, IPackage> keyConversion, Func<V, IPackage> valueConversion) {
            if (source == null) {
                return null;
            }

            NRHash converted = new(source.Count);
            foreach ((var key, var value) in source) {
                converted[keyConversion.Invoke(key)] = valueConversion.Invoke(value);
            }
            return converted;
        }

        private NRHash(int capacity) : base(capacity) {

        }

        public NRHash(AC2Reader data) {
            foreach ((var key, var value) in data.ReadDictionary(data.ReadPackageFullRef, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => this[data.packageRegistry.get<IPackage>(key)] = data.packageRegistry.get<IPackage>(value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.WritePkgFullRef, data.WritePkg);
        }
    }
}
