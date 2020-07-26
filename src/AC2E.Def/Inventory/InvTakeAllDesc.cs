namespace AC2E.Def {

    public class InvTakeAllDesc : IPackage {

        public PackageType packageType => PackageType.InvTakeAllDesc;

        public uint lastError; // m_lastError
        public bool ignoreAttunement; // bIgnoreAttunement
        public bool checkTakePerm; // checkTakePermFlag
        public bool quiet; // m_bQuiet
        public bool noAnim; // noAnimFlag
        public uint status; // m_status
        public InstanceId fromContainerId; // m_fromContainerID
        public InstanceIdList itemsNotTakenIds; // m_itemsNotTaken
        public bool playedAnim; // playedAnim
        public bool noMove; // noMoveFlag
        public InstanceIdList itemsTakenIds; // m_itemsTaken
        public InstanceId targetPlayerId; // m_targetPlayerID

        public InvTakeAllDesc() {

        }

        public InvTakeAllDesc(AC2Reader data) {
            lastError = data.ReadUInt32();
            ignoreAttunement = data.ReadBoolean();
            checkTakePerm = data.ReadBoolean();
            quiet = data.ReadBoolean();
            noAnim = data.ReadBoolean();
            status = data.ReadUInt32();
            fromContainerId = data.ReadInstanceId();
            data.ReadPkg<LList>(v => itemsNotTakenIds = new InstanceIdList(v));
            playedAnim = data.ReadBoolean();
            noMove = data.ReadBoolean();
            data.ReadPkg<LList>(v => itemsTakenIds = new InstanceIdList(v));
            targetPlayerId = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write(lastError);
            data.Write(ignoreAttunement);
            data.Write(checkTakePerm);
            data.Write(quiet);
            data.Write(noAnim);
            data.Write(status);
            data.Write(fromContainerId);
            data.WritePkg(itemsNotTakenIds);
            data.Write(playedAnim);
            data.Write(noMove);
            data.WritePkg(itemsTakenIds);
            data.Write(targetPlayerId);
        }
    }
}
