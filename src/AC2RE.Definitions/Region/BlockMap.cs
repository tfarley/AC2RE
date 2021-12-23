using System.Text;

namespace AC2RE.Definitions;

public class BlockMap {

    public class BlockMapData {

        public uint height;
        public uint width;
        public byte[] map;

        public BlockMapData(uint height, uint width, byte[] map) {
            this.height = height;
            this.width = width;
            this.map = map;
        }

        public override string ToString() {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < height; i++) {
                stringBuilder.AppendLine();
                for (int j = 0; j < width; j++) {
                    stringBuilder.Append($"{map[i * width + j]:X2}");
                }
            }
            return stringBuilder.ToString();
        }
    }

    public DataId did; // m_DID
    public uint height; // height
    public uint width; // width
    public BlockMapData map; // map

    public BlockMap(AC2Reader data) {
        did = data.ReadDataId();
        height = data.ReadUInt32();
        width = data.ReadUInt32();
        map = new(height, width, data.ReadBytes((int)(height * width)));
    }
}
