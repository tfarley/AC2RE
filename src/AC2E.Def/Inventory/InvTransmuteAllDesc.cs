namespace AC2E.Def {

    public class InvTransmuteAllDesc : IPackage {

        public PackageType packageType => PackageType.InvTransmuteAllDesc;

        public uint m_lastError;
        public bool bIgnoreAttunement;
        public InstanceIdList m_itemsTransmuted;
        public bool checkTakePermFlag;
        public uint m_moneyEarned;
        public bool m_bQuiet;
        public bool noAnimFlag;
        public uint m_status;
        public InstanceId m_fromContainerID;
        public InstanceIdList m_itemsNotTransmuted;
        public bool playedAnim;
        public bool noMoveFlag;
        public InstanceId m_targetPlayerID;

        public InvTransmuteAllDesc() {

        }

        public InvTransmuteAllDesc(AC2Reader data, PackageRegistry registry) {
            m_lastError = data.ReadUInt32();
            bIgnoreAttunement = data.ReadBoolean();
            data.ReadPkgRef<LList>(v => m_itemsTransmuted = new InstanceIdList(v), registry);
            checkTakePermFlag = data.ReadBoolean();
            m_moneyEarned = data.ReadUInt32();
            m_bQuiet = data.ReadBoolean();
            noAnimFlag = data.ReadBoolean();
            m_status = data.ReadUInt32();
            m_fromContainerID = data.ReadInstanceId();
            data.ReadPkgRef<LList>(v => m_itemsNotTransmuted = new InstanceIdList(v), registry);
            playedAnim = data.ReadBoolean();
            noMoveFlag = data.ReadBoolean();
            m_targetPlayerID = data.ReadInstanceId();
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_lastError);
            data.Write(bIgnoreAttunement ? (uint)1 : (uint)0);
            data.Write(m_itemsTransmuted, registry);
            data.Write(checkTakePermFlag ? (uint)1 : (uint)0);
            data.Write(m_moneyEarned);
            data.Write(m_bQuiet ? (uint)1 : (uint)0);
            data.Write(noAnimFlag ? (uint)1 : (uint)0);
            data.Write(m_status);
            data.Write(m_fromContainerID);
            data.Write(m_itemsNotTransmuted, registry);
            data.Write(playedAnim ? (uint)1 : (uint)0);
            data.Write(noMoveFlag ? (uint)1 : (uint)0);
            data.Write(m_targetPlayerID);
        }
    }
}
