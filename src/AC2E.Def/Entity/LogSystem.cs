namespace AC2E.Def {

    public class LogSystem : IPackage {

        public PackageType packageType => PackageType.LogSystem;

        public LogInfo logInfo; // m_logInfo

        public LogSystem(AC2Reader data) {
            data.ReadPkg<LogInfo>(v => logInfo = v);
        }
    }
}
