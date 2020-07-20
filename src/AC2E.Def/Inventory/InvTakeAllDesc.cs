namespace AC2E.Def {

    public class InvTakeAllDesc : IPackage {

        public PackageType packageType => PackageType.InvTakeAllDesc;

        public uint m_lastError;
        public bool bIgnoreAttunement;
        public bool checkTakePermFlag;
        public bool m_bQuiet;
        public bool noAnimFlag;
        public uint m_status;
        public InstanceId m_fromContainerID;
        public InstanceIdList m_itemsNotTaken;
        public bool playedAnim;
        public bool noMoveFlag;
        public InstanceIdList m_itemsTaken;
        public InstanceId m_targetPlayerID;

        public InvTakeAllDesc() {

        }

        public InvTakeAllDesc(AC2Reader data) {
            m_lastError = data.ReadUInt32();
            bIgnoreAttunement = data.ReadBoolean();
            checkTakePermFlag = data.ReadBoolean();
            m_bQuiet = data.ReadBoolean();
            noAnimFlag = data.ReadBoolean();
            m_status = data.ReadUInt32();
            m_fromContainerID = data.ReadInstanceId();
            data.ReadPkg<LList>(v => m_itemsNotTaken = new InstanceIdList(v));
            playedAnim = data.ReadBoolean();
            noMoveFlag = data.ReadBoolean();
            data.ReadPkg<LList>(v => m_itemsTaken = new InstanceIdList(v));
            m_targetPlayerID = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write(m_lastError);
            data.Write(bIgnoreAttunement);
            data.Write(checkTakePermFlag);
            data.Write(m_bQuiet);
            data.Write(noAnimFlag);
            data.Write(m_status);
            data.Write(m_fromContainerID);
            data.WritePkg(m_itemsNotTaken);
            data.Write(playedAnim);
            data.Write(noMoveFlag);
            data.WritePkg(m_itemsTaken);
            data.Write(m_targetPlayerID);
        }
    }
}
