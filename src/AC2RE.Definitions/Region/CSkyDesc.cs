using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CSkyDesc {

        public class SkyDay {

            public DataId dayDescDid; // day
            public float frequency; // frequency

            public SkyDay(AC2Reader data) {
                dayDescDid = data.ReadDataId();
                frequency = data.ReadSingle();
            }
        }

        public DataId did; // m_DID
        public uint version; // version
        public double minTickSize; // min_tick_size
        public double maxTickSize; // max_tick_size
        public double minLightTickSize; // min_light_tick_size
        public double maxLightTickSize; // max_light_tick_size
        public DataId gameTimeDid; // game_time_id
        public List<SkyDay> days; // days
        public float freqCount; // freq_count

        public CSkyDesc(AC2Reader data) {
            version = data.ReadUInt32();
            did = data.ReadDataId();
            minTickSize = data.ReadDouble();
            maxTickSize = data.ReadDouble();
            minLightTickSize = data.ReadDouble();
            maxLightTickSize = data.ReadDouble();
            gameTimeDid = data.ReadDataId();
            days = data.ReadList(() => new SkyDay(data));
        }
    }
}
