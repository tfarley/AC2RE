using System.Collections.Generic;

namespace AC2RE.Definitions;

public class InvTakeAllDesc : IHeapObject {

    public PackageType packageType => PackageType.InvTakeAllDesc;

    public ErrorType lastError; // m_lastError
    public bool ignoreAttunement; // bIgnoreAttunement
    public bool checkTakePerm; // checkTakePermFlag
    public bool quiet; // m_bQuiet
    public bool noAnim; // noAnimFlag
    public ErrorType error; // m_status
    public InstanceId fromContainerId; // m_fromContainerID
    public List<InstanceId> itemsNotTakenIds; // m_itemsNotTaken
    public bool playedAnim; // playedAnim
    public bool noMove; // noMoveFlag
    public List<InstanceId> itemsTakenIds; // m_itemsTaken
    public InstanceId targetPlayerId; // m_targetPlayerID

    public InvTakeAllDesc() {

    }

    public InvTakeAllDesc(AC2Reader data) {
        lastError = data.ReadEnum<ErrorType>();
        ignoreAttunement = data.ReadBoolean();
        checkTakePerm = data.ReadBoolean();
        quiet = data.ReadBoolean();
        noAnim = data.ReadBoolean();
        error = data.ReadEnum<ErrorType>();
        fromContainerId = data.ReadInstanceId();
        data.ReadHO<LList>(v => itemsNotTakenIds = v.to<InstanceId>());
        playedAnim = data.ReadBoolean();
        noMove = data.ReadBoolean();
        data.ReadHO<LList>(v => itemsTakenIds = v.to<InstanceId>());
        targetPlayerId = data.ReadInstanceId();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(lastError);
        data.Write(ignoreAttunement);
        data.Write(checkTakePerm);
        data.Write(quiet);
        data.Write(noAnim);
        data.WriteEnum(error);
        data.Write(fromContainerId);
        data.WriteHO(LList.from(itemsNotTakenIds));
        data.Write(playedAnim);
        data.Write(noMove);
        data.WriteHO(LList.from(itemsTakenIds));
        data.Write(targetPlayerId);
    }
}
