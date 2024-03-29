﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CKeyMap {

    // CKeyMap
    public DataId did; // m_DID
    public GUID guid; // mapguid
    public string name; // name
    public List<ActionMapping> mappings; // mappings

    public CKeyMap(AC2Reader data) {
        did = data.ReadDataId();
        name = data.ReadString();
        guid = new(data);
        mappings = data.ReadList(() => new ActionMapping(data));
    }
}
