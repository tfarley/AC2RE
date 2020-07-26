namespace AC2E.Def {

    public class EquipItemProfile : IPackage {

        public PackageType packageType => PackageType.EquipItemProfile;

        public uint weaponLength; // _weapon_length
        public uint primaryParentingLocation; // _priParentingLoc
        public uint secondaryParentingLocation; // _secParentingLoc
        public uint inventoryLocations; // _inventory_locations
        public bool bindOnUse; // _bind_on_use
        public uint preferredInventoryLocation; // _pref_inventory_location
        public uint placementPos; // _placement_position

        public EquipItemProfile() {

        }

        public EquipItemProfile(AC2Reader data) {
            weaponLength = data.ReadUInt32();
            primaryParentingLocation = data.ReadUInt32();
            secondaryParentingLocation = data.ReadUInt32();
            inventoryLocations = data.ReadUInt32();
            bindOnUse = data.ReadBoolean();
            preferredInventoryLocation = data.ReadUInt32();
            placementPos = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(weaponLength);
            data.Write(primaryParentingLocation);
            data.Write(secondaryParentingLocation);
            data.Write(inventoryLocations);
            data.Write(bindOnUse);
            data.Write(preferredInventoryLocation);
            data.Write(placementPos);
        }
    }
}
