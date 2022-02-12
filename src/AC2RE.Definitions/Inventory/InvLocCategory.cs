namespace AC2RE.Definitions;

public class InvLocCategory : IHeapObject {

    public PackageType packageType => PackageType.InvLocCategory;

    public InvLoc weaponInvLoc; // Weapon_InvLoc
    public InvLoc clothingInvLoc; // Clothing_InvLoc
    public InvLoc readySlotInvLoc; // ReadySlot_InvLoc
    public InvLoc armorInvLoc; // Armor_InvLoc
    public InvLoc jewelryInvLoc; // Jewelry_InvLoc
    public InvLoc wristWearInvLoc; // WristWear_InvLoc
    public InvLoc allInvLoc; // All_InvLoc
    public InvLoc fingerWearInvLoc; // FingerWear_InvLoc

    public InvLocCategory(AC2Reader data) {
        weaponInvLoc = data.ReadEnum<InvLoc>();
        clothingInvLoc = data.ReadEnum<InvLoc>();
        readySlotInvLoc = data.ReadEnum<InvLoc>();
        armorInvLoc = data.ReadEnum<InvLoc>();
        jewelryInvLoc = data.ReadEnum<InvLoc>();
        wristWearInvLoc = data.ReadEnum<InvLoc>();
        allInvLoc = data.ReadEnum<InvLoc>();
        fingerWearInvLoc = data.ReadEnum<InvLoc>();
    }
}
