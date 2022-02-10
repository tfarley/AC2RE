namespace AC2RE.Definitions;

public class gmCEntity : gmEntity {

    public override PackageType packageType => PackageType.gmCEntity;

    public ExaminationProfile lastExaminationProfile; // m_lastExaminationProfile

    public gmCEntity(AC2Reader data) : base(data) {
        data.ReadHO<ExaminationProfile>(v => lastExaminationProfile = v);
    }
}
