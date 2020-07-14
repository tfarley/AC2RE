using AC2E.Def;
using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class UsageDescPkg : IPackage {

        public PackageType packageType => PackageType.UsageDesc;

        public StringInfoPkg m_siSuccessMessage;
        public UsageBlobPkg m_usageBlob;
        public InstanceId m_itemID;
        public PositionPkg m_posUser;
        public uint m_wtUser; // TODO: WeenieType
        public InstanceId m_userID;
        public float m_fDistanceToUsedItem;
        public InstanceId m_targetID;
        public uint m_status; // TODO: ErrorType
        public InstanceId m_effTargetID;
        public uint m_uttValid; // TODO: UsageTargetType
        public RListPkg<SingletonPkg> m_effsToApply;
        public int m_iVigorCost;
        public uint m_controlFlags;
        public bool m_bCancelsSF;
        public int m_iHealthCost;

        public UsageDescPkg() {

        }

        public UsageDescPkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<StringInfoPkg>(v => m_siSuccessMessage = v, registry);
            data.ReadPkgRef<UsageBlobPkg>(v => m_usageBlob = v, registry);
            m_itemID = data.ReadInstanceId();
            data.ReadPkgRef<PositionPkg>(v => m_posUser = v, registry);
            m_wtUser = data.ReadUInt32();
            m_userID = data.ReadInstanceId();
            m_fDistanceToUsedItem = data.ReadSingle();
            m_targetID = data.ReadInstanceId();
            m_status = data.ReadUInt32();
            m_effTargetID = data.ReadInstanceId();
            m_uttValid = data.ReadUInt32();
            data.ReadPkgRef<RListPkg<IPackage>>(v => m_effsToApply = v.to<SingletonPkg>(), registry);
            m_iVigorCost = data.ReadInt32();
            m_controlFlags = data.ReadUInt32();
            m_bCancelsSF = data.ReadUInt32() != 0;
            m_iHealthCost = data.ReadInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_siSuccessMessage, registry);
            data.Write(m_usageBlob, registry);
            data.Write(m_itemID);
            data.Write(m_posUser, registry);
            data.Write(m_wtUser);
            data.Write(m_userID);
            data.Write(m_fDistanceToUsedItem);
            data.Write(m_targetID);
            data.Write(m_status);
            data.Write(m_effTargetID);
            data.Write(m_uttValid);
            data.Write(m_effsToApply, registry);
            data.Write(m_iVigorCost);
            data.Write(m_controlFlags);
            data.Write(m_bCancelsSF);
            data.Write(m_iHealthCost);
        }
    }
}
