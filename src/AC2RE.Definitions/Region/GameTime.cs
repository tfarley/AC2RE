using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class GameTime {

        public class TimeOfDay {

            public float begin; // begin
            public bool isNight; // is_night
            public uint timeOfDayToken; // time_of_day_token

            public TimeOfDay(AC2Reader data) {
                begin = data.ReadSingle();
                isNight = data.ReadBoolean();
                timeOfDayToken = data.ReadUInt32();
            }
        }

        public class Season {

            public uint seasonToken; // season_token
            public uint begin; // begin

            public Season(AC2Reader data) {
                begin = data.ReadUInt32();
                seasonToken = data.ReadUInt32();
            }
        }

        public DataId did; // m_DID
        public float dayLength; // day_length
        public uint daysPerYear; // days_per_year
        public double zeroTimeOfYear; // zero_time_of_year
        public uint zeroYear; // zero_year
        public List<TimeOfDay> timesOfDay; // times_of_day
        public List<uint> daysOfTheWeek; // days_of_the_week
        public List<Season> seasons; // seasons
        public uint yearSpec; // year_spec

        public GameTime(AC2Reader data) {
            did = data.ReadDataId();
            zeroTimeOfYear = data.ReadDouble();
            zeroYear = data.ReadUInt32();
            dayLength = data.ReadSingle();
            daysPerYear = data.ReadUInt32();
            yearSpec = data.ReadUInt32();
            timesOfDay = data.ReadList(() => new TimeOfDay(data));
            daysOfTheWeek = data.ReadList(data.ReadUInt32);
            seasons = data.ReadList(() => new Season(data));
        }
    }
}
