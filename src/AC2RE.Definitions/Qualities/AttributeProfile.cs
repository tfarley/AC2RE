namespace AC2RE.Definitions;

public class AttributeProfile : IHeapObject {

    public PackageType packageType => PackageType.AttributeProfile;

    public int startingHealth; // StartingHealth
    public float peaceHealthRegenBase; // PeaceHealthRegenBase
    public float peaceVigorRegenBase; // PeaceVigorRegenBase
    public float combatVigorRegenBase; // CombatVigorRegenBase
    public float combatHealthRegenBase; // CombatHealthRegenBase
    public int startingVigor; // StartingVigor
    public int baseArmorLevel; // BaseArmorLevel
    public int startingSkillCredits; // StartingSkillCredits

    public AttributeProfile(AC2Reader data) {
        startingHealth = data.ReadInt32();
        peaceHealthRegenBase = data.ReadSingle();
        peaceVigorRegenBase = data.ReadSingle();
        combatVigorRegenBase = data.ReadSingle();
        combatHealthRegenBase = data.ReadSingle();
        startingVigor = data.ReadInt32();
        baseArmorLevel = data.ReadInt32();
        startingSkillCredits = data.ReadInt32();
    }
}
