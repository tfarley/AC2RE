namespace AC2RE.Definitions;

public class LevelData : IPackage {

    public PackageType packageType => PackageType.LevelData;

    public ulong xp; // mXP
    public ulong deathXp; // mDeathXP
    public uint vigor; // mVigor
    public uint heroSkillCredits; // mHeroSkillCredits
    public uint maxShieldValue; // mMaxShieldValue
    public uint health; // mHealth
    public uint skillCredits; // mSkillCredits
    public uint maxArmorValue; // mMaxArmorValue
    public uint heroSkillCreditCap; // mHeroSkillCreditCap

    public LevelData(AC2Reader data) {
        xp = data.ReadUInt64();
        deathXp = data.ReadUInt64();
        vigor = data.ReadUInt32();
        heroSkillCredits = data.ReadUInt32();
        maxShieldValue = data.ReadUInt32();
        health = data.ReadUInt32();
        skillCredits = data.ReadUInt32();
        maxArmorValue = data.ReadUInt32();
        heroSkillCreditCap = data.ReadUInt32();
    }
}
