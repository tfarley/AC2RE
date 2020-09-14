namespace AC2E.Def {

    public class InvLocCategory : IPackage {

        public PackageType packageType => PackageType.InvLocCategory;

        public uint weaponInvLoc; // Weapon_InvLoc
        public uint clothingInvLoc; // Clothing_InvLoc
        public uint readySlotInvLoc; // ReadySlot_InvLoc
        public uint armorInvLoc; // Armor_InvLoc
        public uint jewelryInvLoc; // Jewelry_InvLoc
        public uint wristWearInvLoc; // WristWear_InvLoc
        public uint allInvLoc; // All_InvLoc
        public uint fingerWearInvLoc; // FingerWear_InvLoc

        public InvLocCategory(AC2Reader data) {
            weaponInvLoc = data.ReadUInt32();
            clothingInvLoc = data.ReadUInt32();
            readySlotInvLoc = data.ReadUInt32();
            armorInvLoc = data.ReadUInt32();
            jewelryInvLoc = data.ReadUInt32();
            wristWearInvLoc = data.ReadUInt32();
            allInvLoc = data.ReadUInt32();
            fingerWearInvLoc = data.ReadUInt32();
        }
    }
}
