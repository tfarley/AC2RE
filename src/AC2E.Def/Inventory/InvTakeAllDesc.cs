﻿namespace AC2E.Def {

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

        public InvTakeAllDesc(AC2Reader data, PackageRegistry registry) {
            m_lastError = data.ReadUInt32();
            bIgnoreAttunement = data.ReadBoolean();
            checkTakePermFlag = data.ReadBoolean();
            m_bQuiet = data.ReadBoolean();
            noAnimFlag = data.ReadBoolean();
            m_status = data.ReadUInt32();
            m_fromContainerID = data.ReadInstanceId();
            data.ReadPkgRef<LList>(v => m_itemsNotTaken = new InstanceIdList(v), registry);
            playedAnim = data.ReadBoolean();
            noMoveFlag = data.ReadBoolean();
            data.ReadPkgRef<LList>(v => m_itemsTaken = new InstanceIdList(v), registry);
            m_targetPlayerID = data.ReadInstanceId();
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_lastError);
            data.Write(bIgnoreAttunement ? (uint)1 : (uint)0);
            data.Write(checkTakePermFlag ? (uint)1 : (uint)0);
            data.Write(m_bQuiet ? (uint)1 : (uint)0);
            data.Write(noAnimFlag ? (uint)1 : (uint)0);
            data.Write(m_status);
            data.Write(m_fromContainerID);
            data.Write(m_itemsNotTaken, registry);
            data.Write(playedAnim ? (uint)1 : (uint)0);
            data.Write(noMoveFlag ? (uint)1 : (uint)0);
            data.Write(m_itemsTaken, registry);
            data.Write(m_targetPlayerID);
        }
    }
}
