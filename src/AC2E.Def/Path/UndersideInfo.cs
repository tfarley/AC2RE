using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Def {

    public struct UndersideInfo {

        public int minX; // m_min_x
        public int minY; // m_min_y
        public int maxX; // m_max_x
        public int maxY; // m_max_y
        public BBox box; // m_box
        public Vector3 pos; // m_pos
        public List<float> undersideProbes; // m_underside_probes

        public UndersideInfo(AC2Reader data) {
            minX = data.ReadInt32();
            minY = data.ReadInt32();
            maxX = data.ReadInt32();
            maxY = data.ReadInt32();
            box = new BBox(data);
            pos = data.ReadVector();
            undersideProbes = data.ReadList(data.ReadSingle);
        }

        public void write(AC2Writer data) {
            data.Write(minX);
            data.Write(minY);
            data.Write(maxX);
            data.Write(maxY);
            box.write(data);
            data.Write(pos);
            data.Write(undersideProbes, data.Write);
        }
    }
}
