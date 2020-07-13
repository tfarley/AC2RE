using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class UsageDescPkg : IPackage {

        public PackageType packageType => PackageType.UsageDesc;

        public PkgRef<StringInfoPkg> m_siSuccessMessage;
        public PkgRef<UsageBlobPkg> m_usageBlob;
        public InstanceId m_itemID;
        public PkgRef<PositionPkg> m_posUser;
        public uint m_wtUser; // TODO: WeenieType
        public InstanceId m_userID;
        public float m_fDistanceToUsedItem;
        public InstanceId m_targetID;
        public uint m_status; // TODO: ErrorType
        public InstanceId m_effTargetID;
        public uint m_uttValid; // TODO: UsageTargetType
        public PkgRef<RListPkg<SingletonPkg>> m_effsToApply;
        public int m_iVigorCost;
        public uint m_controlFlags;
        public bool m_bCancelsSF;
        public int m_iHealthCost;

        public UsageDescPkg() {

        }

        public UsageDescPkg(BinaryReader data) {
            m_siSuccessMessage = data.ReadPkgRef<StringInfoPkg>();
            m_usageBlob = data.ReadPkgRef<UsageBlobPkg>();
            m_itemID = data.ReadInstanceId();
            m_posUser = data.ReadPkgRef<PositionPkg>();
            m_wtUser = data.ReadUInt32();
            m_userID = data.ReadInstanceId();
            m_fDistanceToUsedItem = data.ReadSingle();
            m_targetID = data.ReadInstanceId();
            m_status = data.ReadUInt32();
            m_effTargetID = data.ReadInstanceId();
            m_uttValid = data.ReadUInt32();
            m_effsToApply = data.ReadPkgRef<RListPkg<SingletonPkg>>();
            m_iVigorCost = data.ReadInt32();
            m_controlFlags = data.ReadUInt32();
            m_bCancelsSF = data.ReadUInt32() != 0;
            m_iHealthCost = data.ReadInt32();
        }

        public void resolveRefs() {
            PackageManager.convert<ARHashPkg<IPackage>>(m_effsToApply.id, v => v.to<SingletonPkg>());
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(m_siSuccessMessage, references);
            data.Write(m_usageBlob, references);
            data.Write(m_itemID);
            data.Write(m_posUser, references);
            data.Write(m_wtUser);
            data.Write(m_userID);
            data.Write(m_fDistanceToUsedItem);
            data.Write(m_targetID);
            data.Write(m_status);
            data.Write(m_effTargetID);
            data.Write(m_uttValid);
            data.Write(m_effsToApply, references);
            data.Write(m_iVigorCost);
            data.Write(m_controlFlags);
            data.Write(m_bCancelsSF);
            data.Write(m_iHealthCost);
        }
    }
}
