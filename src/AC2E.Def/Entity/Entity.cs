namespace AC2E.Def {

    public class Entity : IPackage {

        public virtual PackageType packageType => PackageType.Entity;

        public LAMultiHash hashLockTakenOnOther; // m_hashLockTakenOnOther
        public int movementFrozenCounter; // m_movementFrozenCounter
        public LogInfo logInfo; // m_logInfo
        public int undetectableCounter; // m_undetectableCounter
        public int animationFrozenCounter; // m_animationFrozenCounter
        public AAHash hashLock; // m_hashLock

        public Entity(AC2Reader data) {
            data.ReadPkg<LAMultiHash>(v => hashLockTakenOnOther = v);
            movementFrozenCounter = data.ReadInt32();
            data.ReadPkg<LogInfo>(v => logInfo = v);
            undetectableCounter = data.ReadInt32();
            animationFrozenCounter = data.ReadInt32();
            data.ReadPkg<AAHash>(v => hashLock = v);
        }
    }
}
