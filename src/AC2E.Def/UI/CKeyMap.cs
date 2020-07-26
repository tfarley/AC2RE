using System.Collections.Generic;

namespace AC2E.Def {

    public class CKeyMap {

        public class ActionMapping {

            public uint act; // act
            public uint semantic; // semantic
            public string name; // name

            public ActionMapping(AC2Reader data) {
                act = data.ReadUInt32();
                semantic = data.ReadUInt32();
                name = data.ReadString();
            }
        }

        public DataId did; // m_DID
        public GUID guid; // mapguid
        public string name; // name
        public List<ActionMapping> mappings; // mappings

        public CKeyMap(AC2Reader data) {
            did = data.ReadDataId();
            name = data.ReadString();
            guid = new GUID(data);
            mappings = data.ReadList(() => new ActionMapping(data));
        }
    }
}
