using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CEncounterDesc {

        public class EncounterExcept {

            public uint index; // m_index
            public uint encounterId; // m_encount_id

            public EncounterExcept(AC2Reader data) {
                throw new NotImplementedException();
            }
        }

        public class EncounterElem {

            public uint index; // m_index
            public uint encounterId; // m_encount_id
            public float adjustChance; // m_adjust_chance
            public bool dayBased; // m_day_based
            public List<EncounterExcept> terrainExcept; // m_terrain_except
            public List<EncounterExcept> waterExcept; // m_water_except

            public EncounterElem(AC2Reader data) {
                throw new NotImplementedException();
            }
        }

        public class EncounterRow {

            public uint rowIndex; // m_row_index
            public string encounterName; // m_encounter_name
            public List<EncounterElem> encounters; // m_encounters

            public EncounterRow(AC2Reader data) {
                throw new NotImplementedException();
            }
        }

        public DataId did; // m_DID
        public uint version; // version
        public uint blockEncounterMap; // m_block_encounter_map
        public List<EncounterRow> rows; // m_rows

        public CEncounterDesc(AC2Reader data) {
            version = data.ReadUInt32();
            did = data.ReadDataId();
            blockEncounterMap = data.ReadUInt32();
            rows = data.ReadList(() => new EncounterRow(data));
        }
    }
}
