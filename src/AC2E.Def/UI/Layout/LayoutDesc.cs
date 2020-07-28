using System.Collections.Generic;

namespace AC2E.Def {

    public class LayoutDesc {

        public DataId did; // m_DID
        public DataId stringTableDid; // stringTableID
        public ushort displayWidth; // displayWidth
        public ushort displayHeight; // displayHeight
        public List<ElementDesc> elements; // elements
        public List<DataId> subDids; // subIDArray

        public LayoutDesc() {

        }

        public LayoutDesc(AC2Reader data) {
            did = data.ReadDataId();
            subDids = data.ReadList(data.ReadDataId);
            stringTableDid = data.ReadDataId();
            displayWidth = data.ReadUInt16();
            displayHeight = data.ReadUInt16();
            elements = data.ReadList(() => new ElementDesc(data));
        }

        public void write(AC2Writer data) {
            data.Write(did);
            data.Write(subDids, data.Write);
            data.Write(stringTableDid);
            data.Write(displayWidth);
            data.Write(displayHeight);
            data.Write(elements, v => v.write(data));
        }
    }
}
