﻿namespace AC2RE.Definitions;

public class SkillCheck : IHeapObject {

    public PackageType packageType => PackageType.SkillCheck;

    public float maxSr; // mMaxSR
    public SingletonPkg<SRFormula> srFormula; // mSRFormula
    public bool seeded; // mSeeded
    public float luckMod; // mLuckMod
    public float slopeMod; // mSlopeMod
    public float autoSuccess; // mAutoSuccess
    public float autoFailure; // mAutoFailure
    public float minSr; // mMinSR

    public SkillCheck(AC2Reader data) {
        maxSr = data.ReadSingle();
        data.ReadHO<SRFormula>(v => srFormula = v);
        seeded = data.ReadBoolean();
        luckMod = data.ReadSingle();
        slopeMod = data.ReadSingle();
        autoSuccess = data.ReadSingle();
        autoFailure = data.ReadSingle();
        minSr = data.ReadSingle();
    }
}
