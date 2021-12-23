using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Inventory : Container {

    public override PackageType packageType => PackageType.Inventory;

    public Dictionary<uint, IPackage> inventoryByLocation; // _inventory_by_loc
    public uint filledInventoryLocations; // _filled_invlocs
    public Dictionary<InstanceId, IPackage> inventoryById; // _inventory_by_iid

    public Inventory(AC2Reader data) : base(data) {
        data.ReadPkg<ARHash>(v => inventoryByLocation = v);
        filledInventoryLocations = data.ReadUInt32();
        data.ReadPkg<LRHash>(v => inventoryById = v.to<InstanceId, IPackage>());
    }
}
