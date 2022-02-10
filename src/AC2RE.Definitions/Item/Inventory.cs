using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Inventory : Container {

    public override PackageType packageType => PackageType.Inventory;

    public Dictionary<uint, IHeapObject> inventoryByLocation; // _inventory_by_loc
    public uint filledInventoryLocations; // _filled_invlocs
    public Dictionary<InstanceId, IHeapObject> inventoryById; // _inventory_by_iid

    public Inventory(AC2Reader data) : base(data) {
        data.ReadHO<ARHash>(v => inventoryByLocation = v);
        filledInventoryLocations = data.ReadUInt32();
        data.ReadHO<LRHash>(v => inventoryById = v.to<InstanceId, IHeapObject>());
    }
}
