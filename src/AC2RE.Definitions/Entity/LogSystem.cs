namespace AC2RE.Definitions;

public class LogSystem : IHeapObject {

    public PackageType packageType => PackageType.LogSystem;

    public LogInfo logInfo; // m_logInfo

    public LogSystem(AC2Reader data) {
        data.ReadHO<LogInfo>(v => logInfo = v);
    }
}
