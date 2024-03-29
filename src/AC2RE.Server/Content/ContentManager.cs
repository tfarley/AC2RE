﻿using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AC2RE.Server;

internal class ContentManager : IDisposable {

    private static readonly DataId MASTER_SCENE_LIST_DID = new(0x700001A5);
    private static readonly DataId MASTER_USAGE_ACTION_LIST_DID = new(0x70000257);
    private static readonly DataId MASTER_USAGE_PERMISSION_LIST_DID = new(0x7000026E);
    private static readonly DataId MASTER_SKILL_LIST_DID = new(0x700016E2);
    private static readonly DataId MASTER_SKILL_PANEL_LIST_DID = new(0x70001976);
    private static readonly DataId MASTER_ABILITY_CALCULATOR_LIST_DID = new(0x7000199D);
    private static readonly DataId MASTER_ACT_LIST_DID = new(0x70001A89);

    private readonly DatReader portalDatReader;
    private readonly DatReader cell1DatReader;
    private readonly DatReader localDatReader;
    private CharacterGenSystem? characterGenSystem;
    private CharGenMatrix? charGenMatrix;
    private LevelTable? levelTable;
    private readonly Dictionary<SkillId, Skill> skillCache = new();
    private readonly Dictionary<DataId, AdvancementTable> advancementTableCache = new();
    private readonly Dictionary<DataId, EntityDef> entityDefCache = new();
    private readonly Dictionary<DataId, EntityDef> inheritedEntityDefCache = new();
    private readonly Dictionary<DataId, CBaseQualities> qualitiesCache = new();
    private readonly Dictionary<DataId, WState> weenieStateCache = new();
    private readonly Dictionary<DataId, VisualDesc> visualDescCache = new();
    private readonly Dictionary<DataId, VisualDesc> inheritedVisualDescCache = new();
    private readonly Dictionary<DataId, CEnvCell> envCellCache = new();
    private readonly Dictionary<DataId, EnumIdMap> idMapCache = new();
    private readonly Dictionary<DataId, StringTable> stringTableCache = new();

    public ContentManager() {
        portalDatReader = new("G:\\Asheron's Call 2\\portal.dat_server");
        cell1DatReader = new("G:\\Asheron's Call 2\\cell_1.dat_server");
        localDatReader = new("G:\\Asheron's Call 2\\local_English.dat_server");

        MasterProperty.loadMasterProperties(portalDatReader);
        PackageTypes.loadPackageTypes(portalDatReader);

        loadSkills(portalDatReader);
    }

    public void Dispose() {
        portalDatReader.Dispose();
    }

    private void loadSkills(DatReader datReader) {
        using (AC2Reader data = datReader.getFileReader(MASTER_SKILL_LIST_DID)) {
            WState masterSkillListWState = new(data);
            MasterList masterSkillList = (MasterList)masterSkillListWState.package;
            foreach (SingletonPkg<IHeapObject> singleton in masterSkillList.map.Values) {
                WState wstate = getWeenieState(singleton.wstateDid);
                Skill skill = (Skill)wstate.package;
                skillCache[(SkillId)skill.enumVal] = skill;
            }
        }
    }

    public bool contains(DataId did) {
        return portalDatReader.contains(did);
    }

    public CharacterGenSystem getCharacterGenSystem() {
        if (characterGenSystem == null) {
            using (AC2Reader data = portalDatReader.getFileReader(new(0x70000096))) {
                WState wstate = new(data);
                characterGenSystem = (CharacterGenSystem)wstate.package;
            }
        }

        return characterGenSystem;
    }

    public CharGenMatrix getCharGenMatrix() {
        if (charGenMatrix == null) {
            using (AC2Reader data = portalDatReader.getFileReader(new(0x70000390))) {
                WState wstate = new(data);
                charGenMatrix = (CharGenMatrix)wstate.package;
            }
        }

        return charGenMatrix;
    }

    public LevelTable getLevelTable() {
        if (levelTable == null) {
            using (AC2Reader data = portalDatReader.getFileReader(new(0x70000380))) {
                WState wstate = new(data);
                levelTable = (LevelTable)wstate.package;
            }
        }

        return levelTable;
    }

    public Skill getSkill(SkillId skillId) {
        return skillCache[skillId];
    }

    public AdvancementTable getAdvancementTable(DataId did) {
        WState wstate = getWeenieState(did);
        return (AdvancementTable)wstate.package;
    }

    private EntityDef getEntityDef(DataId did) {
        if (!entityDefCache.TryGetValue(did, out EntityDef? entityDef)) {
            using (AC2Reader data = portalDatReader.getFileReader(did)) {
                EntityDesc entityDesc = new(data);
                entityDef = new(entityDesc);
                entityDefCache[did] = entityDef;
            }
        }
        return entityDef;
    }

    public EntityDef getInheritedEntityDef(DataId did) {
        if (!inheritedEntityDefCache.TryGetValue(did, out EntityDef? inheritedEntityDef)) {
            EntityDef entityDef = getEntityDef(did);

            inheritedEntityDef = new(entityDef);

            List<EntityDef> parentDefs = new();
            parentDefs.Add(entityDef);
            EntityType entityType = entityDef.type;
            DataId parentDid = entityDef.dataId;
            while (entityType == EntityType.EntityDesc && parentDid != DataId.NULL) {
                EntityDef parentEntityDef = getEntityDef(parentDid);
                parentDefs.Add(parentEntityDef);
                entityType = parentEntityDef.type;
                parentDid = parentEntityDef.dataId;
            }

            foreach (EntityDef parentDef in parentDefs) {
                mergeEntityDefs(parentDef, inheritedEntityDef);
            }

            inheritedEntityDefCache[did] = inheritedEntityDef;
        }
        return inheritedEntityDef;
    }

    private void mergeEntityDefs(EntityDef parentEntityDef, EntityDef childEntityDef) {
        foreach ((PropertyName prop, bool value) in parentEntityDef.bools) {
            childEntityDef.bools[prop] = value;
        }
        foreach ((PropertyName prop, int value) in parentEntityDef.ints) {
            childEntityDef.ints[prop] = value;
        }
        foreach ((PropertyName prop, float value) in parentEntityDef.floats) {
            childEntityDef.floats[prop] = value;
        }
        foreach ((PropertyName prop, Vector3 value) in parentEntityDef.vectors) {
            childEntityDef.vectors[prop] = value;
        }
        foreach ((PropertyName prop, RGBAColor value) in parentEntityDef.colors) {
            childEntityDef.colors[prop] = value;
        }
        foreach ((PropertyName prop, string value) in parentEntityDef.strings) {
            childEntityDef.strings[prop] = value;
        }
        foreach ((PropertyName prop, uint value) in parentEntityDef.enums) {
            childEntityDef.enums[prop] = value;
        }
        foreach ((PropertyName prop, DataId value) in parentEntityDef.dids) {
            childEntityDef.dids[prop] = value;
        }
        foreach ((PropertyName prop, Waveform value) in parentEntityDef.waveforms) {
            childEntityDef.waveforms[prop] = value;
        }
        foreach ((PropertyName prop, StringInfo value) in parentEntityDef.stringInfos) {
            childEntityDef.stringInfos[prop] = value;
        }
        foreach ((PropertyName prop, ReferenceId value) in parentEntityDef.packageIds) {
            childEntityDef.packageIds[prop] = value;
        }
        foreach ((PropertyName prop, long value) in parentEntityDef.longs) {
            childEntityDef.longs[prop] = value;
        }
        foreach ((PropertyName prop, Position value) in parentEntityDef.poss) {
            childEntityDef.poss[prop] = value;
        }
    }

    public CBaseQualities getQualities(DataId did) {
        if (!qualitiesCache.TryGetValue(did, out CBaseQualities? qualities)) {
            using (AC2Reader data = portalDatReader.getFileReader(did)) {
                qualities = new(data);
                qualitiesCache[did] = qualities;
            }
        }
        return qualities;
    }

    public WState getWeenieState(DataId did) {
        if (!weenieStateCache.TryGetValue(did, out WState? weenieState)) {
            using (AC2Reader data = portalDatReader.getFileReader(did)) {
                weenieState = new(data);
                weenieStateCache[did] = weenieState;
            }
        }
        return weenieState;
    }

    public WState getWeenieStateFromEntityDid(DataId entityDid) {
        DataId weenieStateDid = new(0x71000000 + entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
        return getWeenieState(weenieStateDid);
    }

    public PackageType getWeenieStatePackageType(DataId did) {
        using (AC2Reader data = portalDatReader.getFileReader(did)) {
            data.BaseStream.Seek(-4, SeekOrigin.End);
            return data.ReadEnum<PackageType>();
        }
    }

    private VisualDesc getVisualDesc(DataId did) {
        if (!visualDescCache.TryGetValue(did, out VisualDesc? visualDesc)) {
            using (AC2Reader data = portalDatReader.getFileReader(did)) {
                visualDesc = new(data);
                visualDescCache[did] = visualDesc;
            }
        }
        return visualDesc;
    }

    public VisualDesc getInheritedVisualDesc(DataId did) {
        if (!inheritedVisualDescCache.TryGetValue(did, out VisualDesc? inheritedVisualDesc)) {
            VisualDesc visualDesc = getVisualDesc(did);

            // TODO: Need to set all other members except the inherited ones from the desc above on this new object
            inheritedVisualDesc = new();

            List<VisualDesc> parentDescs = new();
            parentDescs.Add(visualDesc);
            DataId parentDid = visualDesc.parentDid;
            while (parentDid != DataId.NULL) {
                VisualDesc parentVisualDesc = getVisualDesc(parentDid);
                parentDescs.Add(parentVisualDesc);
                parentDid = parentVisualDesc.parentDid;
            }

            foreach (VisualDesc parentDesc in parentDescs) {
                mergeVisualDescs(parentDesc, inheritedVisualDesc);
            }

            inheritedVisualDescCache[did] = inheritedVisualDesc;
        }
        return inheritedVisualDesc;
    }

    private void mergeVisualDescs(VisualDesc parentVisualDesc, VisualDesc childVisualDesc) {
        if (parentVisualDesc.globalAppearanceModifiers != null) {
            if (childVisualDesc.globalAppearanceModifiers == null) {
                childVisualDesc.globalAppearanceModifiers = new() {
                    key = PartGroupDataDesc.PartGroupKey.EntireTree,
                    appearanceInfos = new(),
                };
            }

            foreach ((DataId appDid, Dictionary<AppearanceKey, float> parentAppearances) in parentVisualDesc.globalAppearanceModifiers.appearanceInfos) {
                if (childVisualDesc.globalAppearanceModifiers.appearanceInfos.TryGetValue(appDid, out Dictionary<AppearanceKey, float>? childAppearances)) {
                    foreach ((AppearanceKey appKey, float appValue) in parentAppearances) {
                        childAppearances.TryAdd(appKey, appValue);
                    }
                } else {
                    childVisualDesc.globalAppearanceModifiers.appearanceInfos[appDid] = new(parentAppearances);
                }
            }
        }
    }

    public CEnvCell getEnvCell(DataId did) {
        if (!envCellCache.TryGetValue(did, out CEnvCell? envCell)) {
            using (AC2Reader data = cell1DatReader.getFileReader(did)) {
                envCell = new(data);
                envCellCache[did] = envCell;
            }
        }
        return envCell;
    }

    public EnumIdMap getIdMap(DataId did) {
        if (!idMapCache.TryGetValue(did, out EnumIdMap? idMap)) {
            using (AC2Reader data = portalDatReader.getFileReader(did)) {
                idMap = new(data);
                idMapCache[did] = idMap;
            }
        }
        return idMap;
    }

    public StringTable getStringTable(DataId did) {
        if (!stringTableCache.TryGetValue(did, out StringTable? stringTable)) {
            using (AC2Reader data = localDatReader.getFileReader(did)) {
                stringTable = new(data);
                stringTableCache[did] = stringTable;
            }
        }
        return stringTable;
    }

    public string getString(StringInfo stringInfo) {
        if (stringInfo.tableDid == DataId.NULL || stringInfo.stringId == StringId.NULL) {
            return "";
        }

        StringTable stringTable = getStringTable(stringInfo.tableDid);
        return stringTable.strings[stringInfo.stringId].strings[0];
    }

    public string translateNetError(NetError netError) {
        if (netError.tableEnumId.id == 0 || netError.stringId == StringId.NULL) {
            return "";
        }

        EnumIdMap idMap = getIdMap(new(0x28000005));
        DataId tableDid = idMap.enumIdToDid[netError.tableEnumId];
        StringInfo stringInfo = new(tableDid, netError.stringId);
        return getString(stringInfo);
    }
}
