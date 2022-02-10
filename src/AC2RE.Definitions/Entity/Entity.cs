using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Entity : IHeapObject {

    public virtual PackageType packageType => PackageType.Entity;

    public Dictionary<ulong, List<uint>> hashLockTakenOnOther; // m_hashLockTakenOnOther
    public int movementFrozenCounter; // m_movementFrozenCounter
    public LogInfo logInfo; // m_logInfo
    public int undetectableCounter; // m_undetectableCounter
    public int animationFrozenCounter; // m_animationFrozenCounter
    public Dictionary<uint, uint> hashLock; // m_hashLock

    public Entity(AC2Reader data) {
        data.ReadHO<LAMultiHash>(v => hashLockTakenOnOther = v);
        movementFrozenCounter = data.ReadInt32();
        data.ReadHO<LogInfo>(v => logInfo = v);
        undetectableCounter = data.ReadInt32();
        animationFrozenCounter = data.ReadInt32();
        data.ReadHO<AAHash>(v => hashLock = v);
    }
}
