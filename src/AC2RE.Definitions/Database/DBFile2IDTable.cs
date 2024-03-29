﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class DBFile2IDTable {

    public class TFileEntry : IWritable {

        // TFileEntry
        public string path; // m_pPath
        public string fileName; // m_pFileName

        public TFileEntry(AC2Reader data) {
            path = data.ReadString();
            fileName = data.ReadString();
        }
    }

    public class TDBTypeEntry : IWritable {

        // TDBTypeEntry
        public string unk1;
        public Dictionary<DataId, TFileEntry> didToEntry; // DIDs

        public TDBTypeEntry(AC2Reader data) {
            unk1 = data.ReadString();
            didToEntry = data.ReadDictionary(data.ReadDataId, () => new TFileEntry(data));
        }
    }

    // DBFile2IDTable
    public DataId did; // m_DID
    public Dictionary<uint, TDBTypeEntry> cacheByDid; // m_CacheByDID

    public DBFile2IDTable(AC2Reader data) {
        did = data.ReadDataId();
        cacheByDid = data.ReadDictionary(data.ReadUInt32, () => new TDBTypeEntry(data));
    }
}
