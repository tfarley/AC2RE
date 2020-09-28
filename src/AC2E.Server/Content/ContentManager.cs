﻿using AC2E.Def;
using System;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class ContentManager : IDisposable {

        private readonly DatReader portalDatReader;
        private CharacterGenSystem characterGenSystem;
        private CharGenMatrix charGenMatrix;
        private Dictionary<DataId, EntityDef> entityDefCache = new Dictionary<DataId, EntityDef>();
        private Dictionary<DataId, CBaseQualities> qualitiesCache = new Dictionary<DataId, CBaseQualities>();
        private Dictionary<DataId, WState> weenieStateCache = new Dictionary<DataId, WState>();
        private Dictionary<DataId, VisualDesc> visualDescCache = new Dictionary<DataId, VisualDesc>();

        public ContentManager() {
            portalDatReader = new DatReader("G:\\Asheron's Call 2\\portal.dat_server");

            MasterProperty.loadMasterProperties(portalDatReader);
            PackageManager.loadPackageTypes(portalDatReader);
        }

        public void Dispose() {
            portalDatReader.Dispose();
        }

        public CharacterGenSystem getCharacterGenSystem() {
            if (characterGenSystem == null) {
                using (AC2Reader data = portalDatReader.getFileReader(new DataId(0x70000096))) {
                    WState wState = new WState(data);
                    characterGenSystem = (CharacterGenSystem)wState.package;
                }
            }

            return characterGenSystem;
        }

        public CharGenMatrix getCharGenMatrix() {
            if (charGenMatrix == null) {
                using (AC2Reader data = portalDatReader.getFileReader(new DataId(0x70000390))) {
                    WState wState = new WState(data);
                    charGenMatrix = (CharGenMatrix)wState.package;
                }
            }

            return charGenMatrix;
        }

        public EntityDef getEntityDef(DataId did) {
            if (!entityDefCache.TryGetValue(did, out EntityDef entityDef)) {
                using (AC2Reader data = portalDatReader.getFileReader(did)) {
                    EntityDesc entityDesc = new EntityDesc(data);
                    entityDef = new EntityDef(entityDesc);
                    entityDefCache[did] = entityDef;
                }
            }
            return entityDef;
        }

        public CBaseQualities getQualities(DataId did) {
            if (!qualitiesCache.TryGetValue(did, out CBaseQualities qualities)) {
                using (AC2Reader data = portalDatReader.getFileReader(did)) {
                    qualities = new CBaseQualities(data);
                    qualitiesCache[did] = qualities;
                }
            }
            return qualities;
        }

        public WState getWeenieState(DataId did) {
            if (!weenieStateCache.TryGetValue(did, out WState weenieState)) {
                using (AC2Reader data = portalDatReader.getFileReader(did)) {
                    weenieState = new WState(data);
                    weenieStateCache[did] = weenieState;
                }
            }
            return weenieState;
        }

        public VisualDesc getVisualDesc(DataId did) {
            if (!visualDescCache.TryGetValue(did, out VisualDesc visualDesc)) {
                using (AC2Reader data = portalDatReader.getFileReader(did)) {
                    visualDesc = new VisualDesc(data);
                    visualDescCache[did] = visualDesc;
                }
            }
            return visualDesc;
        }
    }
}
