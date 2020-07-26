namespace AC2E.Def {

    public class InvTransmuteAllDesc : IPackage {

        public PackageType packageType => PackageType.InvTransmuteAllDesc;

        public uint lastError; // m_lastError
        public bool ignoreAttunement; // bIgnoreAttunement
        public InstanceIdList itemsTransmutedIds; // m_itemsTransmuted
        public bool checkTakePerm; // checkTakePermFlag
        public uint moneyEarned; // m_moneyEarned
        public bool quiet; // m_bQuiet
        public bool noAnim; // noAnimFlag
        public uint status; // m_status
        public InstanceId fromContainerId; // m_fromContainerID
        public InstanceIdList itemsNotTransmutedIds; // m_itemsNotTransmuted
        public bool playedAnim; // playedAnim
        public bool noMove; // noMoveFlag
        public InstanceId targetPlayerId; // m_targetPlayerID

        public InvTransmuteAllDesc() {

        }

        public InvTransmuteAllDesc(AC2Reader data) {
            lastError = data.ReadUInt32();
            ignoreAttunement = data.ReadBoolean();
            data.ReadPkg<LList>(v => itemsTransmutedIds = new InstanceIdList(v));
            checkTakePerm = data.ReadBoolean();
            moneyEarned = data.ReadUInt32();
            quiet = data.ReadBoolean();
            noAnim = data.ReadBoolean();
            status = data.ReadUInt32();
            fromContainerId = data.ReadInstanceId();
            data.ReadPkg<LList>(v => itemsNotTransmutedIds = new InstanceIdList(v));
            playedAnim = data.ReadBoolean();
            noMove = data.ReadBoolean();
            targetPlayerId = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write(lastError);
            data.Write(ignoreAttunement);
            data.WritePkg(itemsTransmutedIds);
            data.Write(checkTakePerm);
            data.Write(moneyEarned);
            data.Write(quiet);
            data.Write(noAnim);
            data.Write(status);
            data.Write(fromContainerId);
            data.WritePkg(itemsNotTransmutedIds);
            data.Write(playedAnim);
            data.Write(noMove);
            data.Write(targetPlayerId);
        }
    }
}
