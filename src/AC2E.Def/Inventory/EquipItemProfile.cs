namespace AC2E.Def {

    public class EquipItemProfile : IPackage {

        public PackageType packageType => PackageType.EquipItemProfile;

        public uint _weapon_length;
        public uint _priParentingLoc;
        public uint _secParentingLoc;
        public uint _inventory_locations;
        public bool _bind_on_use;
        public uint _pref_inventory_location;
        public uint _placement_position;

        public EquipItemProfile() {

        }

        public EquipItemProfile(AC2Reader data, PackageRegistry registry) {
            _weapon_length = data.ReadUInt32();
            _priParentingLoc = data.ReadUInt32();
            _secParentingLoc = data.ReadUInt32();
            _inventory_locations = data.ReadUInt32();
            _bind_on_use = data.ReadBoolean();
            _pref_inventory_location = data.ReadUInt32();
            _placement_position = data.ReadUInt32();
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(_weapon_length);
            data.Write(_priParentingLoc);
            data.Write(_secParentingLoc);
            data.Write(_inventory_locations);
            data.Write(_bind_on_use ? (uint)1 : (uint)0);
            data.Write(_pref_inventory_location);
            data.Write(_placement_position);
        }
    }
}
