﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class MasterProperty {

        public static MasterProperty instance;

        public static void loadMasterProperties(DatReader datReader) {
            if (instance == null) {
                using (AC2Reader data = datReader.getFileReader(DbTypeDef.TYPE_TO_DEF[DbType.MASTER_PROPERTY].baseDid)) {
                    instance = new MasterProperty(data);
                }
            }
        }

        public DataId did; // m_DID
        public EnumMapper enumMapper; // m_emapper
        public Dictionary<uint, BasePropertyDesc> properties; // m_properties

        public MasterProperty(AC2Reader data) {
            did = data.ReadDataId();
            enumMapper = new EnumMapper(data);
            properties = data.ReadDictionary(data.ReadUInt32, () => new BasePropertyDesc(data));
        }
    }
}
