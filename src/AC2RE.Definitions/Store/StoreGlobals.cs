namespace AC2RE.Definitions;

public class StoreGlobals : IHeapObject {

    public PackageType packageType => PackageType.StoreGlobals;

    public uint nextSaleId; // NextSaleID_StoreGlobals
    public DataId goldDid; // DIDGold_StoreGlobals

    public StoreGlobals(AC2Reader data) {
        nextSaleId = data.ReadUInt32();
        goldDid = data.ReadDataId();
    }
}
