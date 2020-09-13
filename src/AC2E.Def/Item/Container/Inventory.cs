namespace AC2E.Def {

    public class Inventory : Container {

        public override PackageType packageType => PackageType.Inventory;

        public ARHash<IPackage> inventoryByLocation; // _inventory_by_loc
        public uint filledInventoryLocations; // _filled_invlocs
        public InstanceIdRHash<IPackage> inventoryById; // _inventory_by_iid

        public Inventory(AC2Reader data) : base(data) {
            data.ReadPkg<ARHash<IPackage>>(v => inventoryByLocation = v);
            filledInventoryLocations = data.ReadUInt32();
            data.ReadPkg<LRHash<IPackage>>(v => inventoryById = new InstanceIdRHash<IPackage>(v));
        }
    }
}
