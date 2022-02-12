using System.Collections.Generic;

namespace AC2RE.Definitions;

public class InvTransmuteAllDesc : IHeapObject {

    public PackageType packageType => PackageType.InvTransmuteAllDesc;

    public ErrorType lastError; // m_lastError
    public bool ignoreAttunement; // bIgnoreAttunement
    public List<InstanceId> transmutedItemIds; // m_itemsTransmuted
    public bool checkTakePerm; // checkTakePermFlag
    public uint moneyEarned; // m_moneyEarned
    public bool quiet; // m_bQuiet
    public bool noAnim; // noAnimFlag
    public ErrorType error; // m_status
    public InstanceId fromContainerId; // m_fromContainerID
    public List<InstanceId> notTransmutedItemIds; // m_itemsNotTransmuted
    public bool playedAnim; // playedAnim
    public bool noMove; // noMoveFlag
    public InstanceId targetPlayerId; // m_targetPlayerID

    public InvTransmuteAllDesc() {

    }

    public InvTransmuteAllDesc(AC2Reader data) {
        lastError = data.ReadEnum<ErrorType>();
        ignoreAttunement = data.ReadBoolean();
        data.ReadHO<LList>(v => transmutedItemIds = v.to<InstanceId>());
        checkTakePerm = data.ReadBoolean();
        moneyEarned = data.ReadUInt32();
        quiet = data.ReadBoolean();
        noAnim = data.ReadBoolean();
        error = data.ReadEnum<ErrorType>();
        fromContainerId = data.ReadInstanceId();
        data.ReadHO<LList>(v => notTransmutedItemIds = v.to<InstanceId>());
        playedAnim = data.ReadBoolean();
        noMove = data.ReadBoolean();
        targetPlayerId = data.ReadInstanceId();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(lastError);
        data.Write(ignoreAttunement);
        data.WriteHO(LList.from(transmutedItemIds));
        data.Write(checkTakePerm);
        data.Write(moneyEarned);
        data.Write(quiet);
        data.Write(noAnim);
        data.WriteEnum(error);
        data.Write(fromContainerId);
        data.WriteHO(LList.from(notTransmutedItemIds));
        data.Write(playedAnim);
        data.Write(noMove);
        data.Write(targetPlayerId);
    }
}
