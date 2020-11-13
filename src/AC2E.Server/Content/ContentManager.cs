using AC2E.Def;
using System;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class ContentManager : IDisposable {

        private readonly DatReader portalDatReader;
        private CharacterGenSystem? characterGenSystem;
        private CharGenMatrix? charGenMatrix;
        private readonly Dictionary<DataId, EntityDef> entityDefCache = new();
        private readonly Dictionary<DataId, CBaseQualities> qualitiesCache = new();
        private readonly Dictionary<DataId, WState> weenieStateCache = new();
        private readonly Dictionary<DataId, VisualDesc> visualDescCache = new();

        public ContentManager() {
            portalDatReader = new("G:\\Asheron's Call 2\\portal.dat_server");

            MasterProperty.loadMasterProperties(portalDatReader);
            PackageManager.loadPackageTypes(portalDatReader);
        }

        public void Dispose() {
            portalDatReader.Dispose();
        }

        public CharacterGenSystem getCharacterGenSystem() {
            if (characterGenSystem == null) {
                using (AC2Reader data = portalDatReader.getFileReader(new(0x70000096))) {
                    WState wState = new(data);
                    characterGenSystem = (CharacterGenSystem)wState.package;
                }
            }

            return characterGenSystem;
        }

        public CharGenMatrix getCharGenMatrix() {
            if (charGenMatrix == null) {
                using (AC2Reader data = portalDatReader.getFileReader(new(0x70000390))) {
                    WState wState = new(data);
                    charGenMatrix = (CharGenMatrix)wState.package;
                }
            }

            return charGenMatrix;
        }

        public EntityDef getEntityDef(DataId did) {
            if (!entityDefCache.TryGetValue(did, out EntityDef? entityDef)) {
                using (AC2Reader data = portalDatReader.getFileReader(did)) {
                    EntityDesc entityDesc = new(data);
                    entityDef = new(entityDesc);
                    entityDefCache[did] = entityDef;
                }
            }
            return entityDef;
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

        private VisualDesc getVisualDesc(DataId did) {
            if (!visualDescCache.TryGetValue(did, out VisualDesc? visualDesc)) {
                using (AC2Reader data = portalDatReader.getFileReader(did)) {
                    visualDesc = new(data);
                    visualDescCache[did] = visualDesc;
                }
            }
            return visualDesc;
        }

        public VisualDesc getInheritedVisualDesc(VisualDesc visualDesc) {
            List<VisualDesc> parentDescs = new();
            parentDescs.Add(visualDesc);
            DataId parentDid = visualDesc.parentDid;
            while (parentDid != DataId.NULL) {
                VisualDesc parentVisualDesc = getVisualDesc(parentDid);
                parentDescs.Add(parentVisualDesc);
                parentDid = parentVisualDesc.parentDid;
            }

            VisualDesc inheritedVisualDesc = new();

            foreach (VisualDesc parentDesc in parentDescs) {
                mergeVisualDescs(parentDesc, inheritedVisualDesc);
            }

            return inheritedVisualDesc;
        }

        private void mergeVisualDescs(VisualDesc parentVisualDesc, VisualDesc childVisualDesc) {
            if (parentVisualDesc.globalAppearanceModifiers != null) {
                if (childVisualDesc.globalAppearanceModifiers == null) {
                    childVisualDesc.globalAppearanceModifiers = new() {
                        packFlags = PartGroupDataDesc.PackFlag.KEY | PartGroupDataDesc.PackFlag.APPHASH,
                        key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
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
    }
}
