namespace AC2RE.Definitions;

public class BasePropertyDesc {

    // Enum PropertyWeenieReportType::Type
    public enum WeenieReportType : uint {
        Reportable, // Reportable
        Inquirable, // Inquirable
        Ignored, // Ignored
    }

    // BasePropertyDesc
    public PropertyType type; // m_type
    public PropertyGroupName group; // m_group
    public uint propData; // m_data
    public WeenieReportType reportToWeenie; // m_report_to_weenie
    public bool required; // m_bRequired

    public BasePropertyDesc(AC2Reader data) {
        type = data.ReadEnum<PropertyType>();
        group = data.ReadEnum<PropertyGroupName>();
        propData = data.ReadUInt32();
        reportToWeenie = data.ReadEnum<WeenieReportType>();
        required = data.ReadBoolean();
    }
}
