﻿namespace AC2RE.Definitions;

public class HeroControl : IHeapObject {

    public PackageType packageType => PackageType.HeroControl;

    public DataId heroSkillCreditTokenDid; // m_didHeroSkillCreditToken

    public HeroControl(AC2Reader data) {
        heroSkillCreditTokenDid = data.ReadDataId();
    }
}
