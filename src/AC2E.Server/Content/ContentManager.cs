using AC2E.Def;
using AC2E.Server.Database;
using System;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class ContentManager {

        private readonly DatReader portalDatReader;
        private CharGenMatrix charGenMatrix;
        private Dictionary<DataId, EntityDesc> entityDescCache = new Dictionary<DataId, EntityDesc>();
        private Dictionary<DataId, VisualDesc> visualDescCache = new Dictionary<DataId, VisualDesc>();

        public ContentManager() {
            portalDatReader = new DatReader("G:\\Asheron's Call 2\\portal.dat");

            if (MasterProperty.instance == null) {
                using (AC2Reader data = portalDatReader.getFileReader(DbTypeDef.TYPE_TO_DEF[DbType.MASTER_PROPERTY].baseDid)) {
                    MasterProperty.instance = new MasterProperty(data);
                }
            }
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

        public EntityDesc getEntityDesc(DataId did) {
            if (!entityDescCache.TryGetValue(did, out EntityDesc entityDesc)) {
                using (AC2Reader data = portalDatReader.getFileReader(did)) {
                    entityDesc = new EntityDesc(data);
                    entityDescCache[did] = entityDesc;
                }
            }
            return entityDesc;
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
