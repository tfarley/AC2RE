using AC2E.Def;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AC2E.Server {

    public class EntityDef {

        public readonly EntityType type;
        public readonly DataId dataId;
        public readonly PackageType packageType;
        public readonly Dictionary<PropertyName, bool> bools = new();
        public readonly Dictionary<PropertyName, int> ints = new();
        public readonly Dictionary<PropertyName, float> floats = new();
        public readonly Dictionary<PropertyName, Vector3> vectors = new();
        public readonly Dictionary<PropertyName, RGBAColor> colors = new();
        public readonly Dictionary<PropertyName, StringInfo> strings = new();
        public readonly Dictionary<PropertyName, uint> enums = new();
        public readonly Dictionary<PropertyName, DataId> dids = new();
        public readonly Dictionary<PropertyName, Waveform> waveforms = new();
        public readonly Dictionary<PropertyName, long> longs = new();
        public readonly Dictionary<PropertyName, Position> poss = new();

        public EntityDef(EntityDesc entityDesc) {
            type = entityDesc.type;
            dataId = entityDesc.dataId;
            packageType = entityDesc.packageType;
            if (entityDesc.packFlags.HasFlag(EntityDesc.PackFlag.PROPERTIES)) {
                foreach (PropertyGroup propertyGroup in entityDesc.properties.groups) {
                    foreach (BaseProperty property in propertyGroup.properties) {
                        switch (property.type) {
                            case PropertyType.BOOL:
                                bools[property.name] = (bool)property.value;
                                break;
                            case PropertyType.INTEGER:
                                ints[property.name] = (int)property.value;
                                break;
                            case PropertyType.FLOAT:
                                floats[property.name] = (float)property.value;
                                break;
                            case PropertyType.VECTOR:
                                vectors[property.name] = (Vector3)property.value;
                                break;
                            case PropertyType.COLOR:
                                colors[property.name] = (RGBAColor)property.value;
                                break;
                            case PropertyType.STRING:
                                strings[property.name] = new((string)property.value);
                                break;
                            case PropertyType.ENUM:
                                enums[property.name] = (uint)property.value;
                                break;
                            case PropertyType.DATA_FILE:
                                dids[property.name] = (DataId)property.value;
                                break;
                            case PropertyType.WAVEFORM:
                                waveforms[property.name] = (Waveform)property.value;
                                break;
                            case PropertyType.STRING_INFO:
                                strings[property.name] = (StringInfo)property.value;
                                break;
                            case PropertyType.PACKAGE_ID:
                                ints[property.name] = (int)property.value;
                                break;
                            case PropertyType.LONG_INTEGER:
                                longs[property.name] = (long)property.value;
                                break;
                            case PropertyType.POSITION:
                                poss[property.name] = (Position)property.value;
                                break;
                            default:
                                throw new InvalidDataException(type.ToString());
                        }
                    }
                }
            }
        }
    }
}
