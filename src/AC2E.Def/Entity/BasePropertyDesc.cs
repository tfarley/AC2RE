namespace AC2E.Def {

    public class BasePropertyDesc {

        // Enum PropertyWeenieReportType::Type
        public enum PropertyWeenieReportType {
            REPORTABLE = 0,
            INQUIRABLE = 1,
            IGNORED = 2,
        }

        public PropertyType type; // m_type
        public PropertyGroupName group; // m_group
        public uint propData; // m_data
        public PropertyWeenieReportType reportToWeenie; // m_report_to_weenie
        public bool required; // m_bRequired
        public BaseProperty defaultValue; // m_defaultValue
        public BaseProperty minValue; // m_minValue
        public BaseProperty maxValue; // m_maxValue

        public BasePropertyDesc(AC2Reader data) {
            type = (PropertyType)data.ReadUInt32();
            group = (PropertyGroupName)data.ReadUInt32();
            propData = data.ReadUInt32();
            reportToWeenie = (PropertyWeenieReportType)data.ReadUInt32();
            required = data.ReadBoolean();
        }
    }
}
