namespace AC2E.Def {

    public class gmCEntity : gmEntity {

        public override PackageType packageType => PackageType.gmCEntity;

        public ExaminationProfile lastExaminationProfile; // m_lastExaminationProfile

        public gmCEntity(AC2Reader data) : base(data) {
            data.ReadPkg<ExaminationProfile>(v => lastExaminationProfile = v);
        }
    }
}
