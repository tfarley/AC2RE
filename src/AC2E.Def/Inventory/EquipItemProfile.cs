namespace AC2E.Def {

    public class EquipItemProfile : IPackage {

        public PackageType packageType => PackageType.EquipItemProfile;

        public uint weaponLength; // _weapon_length
        public InvLoc primaryParentingLocation; // _priParentingLoc
        public InvLoc secondaryParentingLocation; // _secParentingLoc
        public InvLoc inventoryLocations; // _inventory_locations
        public bool bindOnUse; // _bind_on_use
        public InvLoc preferredInventoryLocation; // _pref_inventory_location
        public uint placementPos; // _placement_position

        public EquipItemProfile() {

        }

        public EquipItemProfile(AC2Reader data) {
            weaponLength = data.ReadUInt32();
            primaryParentingLocation = (InvLoc)data.ReadUInt32();
            secondaryParentingLocation = (InvLoc)data.ReadUInt32();
            inventoryLocations = (InvLoc)data.ReadUInt32();
            bindOnUse = data.ReadBoolean();
            preferredInventoryLocation = (InvLoc)data.ReadUInt32();
            placementPos = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(weaponLength);
            data.Write((uint)primaryParentingLocation);
            data.Write((uint)secondaryParentingLocation);
            data.Write((uint)inventoryLocations);
            data.Write(bindOnUse);
            data.Write((uint)preferredInventoryLocation);
            data.Write(placementPos);
        }
    }
}
