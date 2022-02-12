using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class StoreTemplate : IHeapObject {

    public PackageType packageType => PackageType.StoreTemplate;

    // WLib StoreTemplate
    [Flags]
    public enum Flag : uint {
        None = 0,
        HideRestricted = 1 << 0, // HideRestricted 0x00000001
    }

    public List<IHeapObject> filters; // m_filters
    public SingletonPkg<StoreSorter> sorter; // m_sorter
    public StringInfo name; // m_siName
    public StringInfo description; // m_siDesc
    public int totalSales; // m_iTotalSales
    public List<SaleTemplate> sales; // m_sales
    public DataId portraitDid; // m_didPortrait
    public int version; // m_iVersion
    public Flag flags; // m_uiFlags

    public StoreTemplate(AC2Reader data) {
        data.ReadHO<RList>(v => filters = v);
        data.ReadHO<StoreSorter>(v => sorter = v);
        data.ReadHO<StringInfo>(v => name = v);
        data.ReadHO<StringInfo>(v => description = v);
        totalSales = data.ReadInt32();
        data.ReadHO<RArray>(v => sales = v.to<SaleTemplate>());
        portraitDid = data.ReadDataId();
        version = data.ReadInt32();
        flags = data.ReadEnum<Flag>();
    }
}
