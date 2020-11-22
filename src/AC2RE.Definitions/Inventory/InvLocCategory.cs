namespace AC2RE.Definitions {

    public class InvLocCategory : IPackage {

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
            weaponInvLoc = (InvLoc)data.ReadUInt32();
            clothingInvLoc = (InvLoc)data.ReadUInt32();
            readySlotInvLoc = (InvLoc)data.ReadUInt32();
            armorInvLoc = (InvLoc)data.ReadUInt32();
            jewelryInvLoc = (InvLoc)data.ReadUInt32();
            wristWearInvLoc = (InvLoc)data.ReadUInt32();
            allInvLoc = (InvLoc)data.ReadUInt32();
            fingerWearInvLoc = (InvLoc)data.ReadUInt32();
        }
    }
}
