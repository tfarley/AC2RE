namespace AC2E.Def {

    public class UsageDesc : IPackage {

        public PackageType packageType => PackageType.UsageDesc;

        public StringInfo m_siSuccessMessage;
        public UsageBlob m_usageBlob;
        public InstanceId m_itemID;
        public Position m_posUser;
        public uint m_wtUser; // TODO: WeenieType
        public InstanceId m_userID;
        public float m_fDistanceToUsedItem;
        public InstanceId m_targetID;
        public uint m_status; // TODO: ErrorType
        public InstanceId m_effTargetID;
        public uint m_uttValid; // TODO: UsageTargetType
        public RList<SingletonPkg> m_effsToApply;
        public int m_iVigorCost;
        public uint m_controlFlags;
        public bool m_bCancelsSF;
        public int m_iHealthCost;

        public UsageDesc() {

        }

        public UsageDesc(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => m_siSuccessMessage = v);
            data.ReadPkg<UsageBlob>(v => m_usageBlob = v);
            m_itemID = data.ReadInstanceId();
            data.ReadPkg<Position>(v => m_posUser = v);
            m_wtUser = data.ReadUInt32();
            m_userID = data.ReadInstanceId();
            m_fDistanceToUsedItem = data.ReadSingle();
            m_targetID = data.ReadInstanceId();
            m_status = data.ReadUInt32();
            m_effTargetID = data.ReadInstanceId();
            m_uttValid = data.ReadUInt32();
            data.ReadPkg<RList<IPackage>>(v => m_effsToApply = v.to<SingletonPkg>());
            m_iVigorCost = data.ReadInt32();
            m_controlFlags = data.ReadUInt32();
            m_bCancelsSF = data.ReadBoolean();
            m_iHealthCost = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_siSuccessMessage);
            data.WritePkg(m_usageBlob);
            data.Write(m_itemID);
            data.WritePkg(m_posUser);
            data.Write(m_wtUser);
            data.Write(m_userID);
            data.Write(m_fDistanceToUsedItem);
            data.Write(m_targetID);
            data.Write(m_status);
            data.Write(m_effTargetID);
            data.Write(m_uttValid);
            data.WritePkg(m_effsToApply);
            data.Write(m_iVigorCost);
            data.Write(m_controlFlags);
            data.Write(m_bCancelsSF);
            data.Write(m_iHealthCost);
        }
    }
}
