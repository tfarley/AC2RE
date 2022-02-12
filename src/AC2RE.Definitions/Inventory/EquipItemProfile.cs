namespace AC2RE.Definitions;

public class EquipItemProfile : IHeapObject {

    public PackageType packageType => PackageType.EquipItemProfile;

    public uint weaponLength; // _weapon_length
    public HoldingLocation primaryParentingLocation; // _priParentingLoc
    public HoldingLocation secondaryParentingLocation; // _secParentingLoc
    public InvLoc inventoryLocations; // _inventory_locations
    public bool bindOnUse; // _bind_on_use
    public InvLoc preferredInventoryLocation; // _pref_inventory_location
    public Orientation placementPos; // _placement_position

    public EquipItemProfile() {

    }

    public EquipItemProfile(AC2Reader data) {
        weaponLength = data.ReadUInt32();
        primaryParentingLocation = data.ReadEnum<HoldingLocation>();
        secondaryParentingLocation = data.ReadEnum<HoldingLocation>();
        inventoryLocations = data.ReadEnum<InvLoc>();
        bindOnUse = data.ReadBoolean();
        preferredInventoryLocation = data.ReadEnum<InvLoc>();
        placementPos = data.ReadEnum<Orientation>();
    }

    public void write(AC2Writer data) {
        data.Write(weaponLength);
        data.WriteEnum(primaryParentingLocation);
        data.WriteEnum(secondaryParentingLocation);
        data.WriteEnum(inventoryLocations);
        data.Write(bindOnUse);
        data.WriteEnum(preferredInventoryLocation);
        data.WriteEnum(placementPos);
    }
}
