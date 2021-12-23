using AC2RE.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AC2RE.Server;

public class EntityDef {

    public readonly EntityType type;
    public readonly DataId dataId;
    public readonly PackageType packageType;
    public readonly Dictionary<PropertyName, bool> bools = new();
    public readonly Dictionary<PropertyName, int> ints = new();
    public readonly Dictionary<PropertyName, float> floats = new();
    public readonly Dictionary<PropertyName, Vector3> vectors = new();
    public readonly Dictionary<PropertyName, RGBAColor> colors = new();
    public readonly Dictionary<PropertyName, string> strings = new();
    public readonly Dictionary<PropertyName, uint> enums = new();
    public readonly Dictionary<PropertyName, DataId> dids = new();
    public readonly Dictionary<PropertyName, Waveform> waveforms = new();
    public readonly Dictionary<PropertyName, StringInfo> stringInfos = new();
    public readonly Dictionary<PropertyName, PackageId> packageIds = new();
    public readonly Dictionary<PropertyName, long> longs = new();
    public readonly Dictionary<PropertyName, Position> poss = new();

    public bool usageHeroOnly => bools.GetValueOrDefault(PropertyName.Usage_HeroOnly);
    public QuestId usageRequiredQuest => (QuestId)enums.GetValueOrDefault(PropertyName.Usage_RequiredQuest);
    public QuestStatus usageRequiredQuestStatus => (QuestStatus)enums.GetValueOrDefault(PropertyName.Usage_RequiredQuestStatus);
    public int usageMinLevel => ints.GetValueOrDefault(PropertyName.Usage_MinLevel);

    public bool canUseWhileMoving => bools.GetValueOrDefault(PropertyName.Item_CanUseWhileMoving);
    public UsagePermissionType usagePermission => (UsagePermissionType)enums.GetValueOrDefault(PropertyName.Item_UsagePermission);
    public UsageActionType usageAction => (UsageActionType)enums.GetValueOrDefault(PropertyName.Item_UsageAction);

    public EntityDef(EntityDef entityDef) {
        type = entityDef.type;
        dataId = entityDef.dataId;
        packageType = entityDef.packageType;
    }

    public EntityDef(EntityDesc entityDesc) {
        type = entityDesc.type;
        dataId = entityDesc.dataId;
        packageType = entityDesc.packageType;
        if (entityDesc.packFlags.HasFlag(EntityDesc.PackFlag.PROPERTIES)) {
            foreach (PropertyGroup propertyGroup in entityDesc.properties.groups) {
                foreach (BaseProperty property in propertyGroup.properties) {
                    switch (property.type) {
                        case PropertyType.Bool:
                            bools[property.name] = (bool)property.value;
                            break;
                        case PropertyType.Integer:
                            ints[property.name] = (int)property.value;
                            break;
                        case PropertyType.Float:
                            floats[property.name] = (float)property.value;
                            break;
                        case PropertyType.Vector:
                            vectors[property.name] = (Vector3)property.value;
                            break;
                        case PropertyType.Color:
                            colors[property.name] = (RGBAColor)property.value;
                            break;
                        case PropertyType.String:
                            strings[property.name] = (string)property.value;
                            break;
                        case PropertyType.Enum:
                            enums[property.name] = (uint)property.value;
                            break;
                        case PropertyType.DataFile:
                            dids[property.name] = (DataId)property.value;
                            break;
                        case PropertyType.Waveform:
                            waveforms[property.name] = (Waveform)property.value;
                            break;
                        case PropertyType.StringInfo:
                            stringInfos[property.name] = (StringInfo)property.value;
                            break;
                        case PropertyType.PackageID:
                            packageIds[property.name] = (PackageId)property.value;
                            break;
                        case PropertyType.LongInteger:
                            longs[property.name] = (long)property.value;
                            break;
                        case PropertyType.Position:
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
