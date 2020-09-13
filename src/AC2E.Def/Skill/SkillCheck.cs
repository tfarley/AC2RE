namespace AC2E.Def {

    public class SkillCheck : IPackage {

        public PackageType packageType => PackageType.SkillCheck;

        public float maxSr; // mMaxSR
        public SRFormula srFormula; // mSRFormula
        public bool seeded; // mSeeded
        public float luckMod; // mLuckMod
        public float slopeMod; // mSlopeMod
        public float autoSuccess; // mAutoSuccess
        public float autoFailure; // mAutoFailure
        public float minSr; // mMinSR

        public SkillCheck(AC2Reader data) {
            maxSr = data.ReadSingle();
            data.ReadPkg<SRFormula>(v => srFormula = v);
            seeded = data.ReadBoolean();
            luckMod = data.ReadSingle();
            slopeMod = data.ReadSingle();
            autoSuccess = data.ReadSingle();
            autoFailure = data.ReadSingle();
            minSr = data.ReadSingle();
        }
    }
}
