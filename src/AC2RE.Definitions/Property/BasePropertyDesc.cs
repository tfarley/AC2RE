namespace AC2RE.Definitions {

    public class BasePropertyDesc {

        // Enum PropertyWeenieReportType::Type
        public enum WeenieReportType : uint {
            REPORTABLE,
            INQUIRABLE,
            IGNORED,
        }

        public PropertyType type; // m_type
        public PropertyGroupName group; // m_group
        public uint propData; // m_data
        public WeenieReportType reportToWeenie; // m_report_to_weenie
        public bool required; // m_bRequired

        public BasePropertyDesc(AC2Reader data) {
            type = (PropertyType)data.ReadUInt32();
            group = (PropertyGroupName)data.ReadUInt32();
            propData = data.ReadUInt32();
            reportToWeenie = (WeenieReportType)data.ReadUInt32();
            required = data.ReadBoolean();
        }
    }
}
