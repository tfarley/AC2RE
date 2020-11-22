namespace AC2RE.Definitions {

    public class MineUsageBlob : UsageBlob {

        public override PackageType packageType => PackageType.MineUsageBlob;

        public uint defaultRepeatCount; // m_defaultRepeatCount
        public DataId objectDid; // m_didObject
        public uint defaultBehavior; // m_defaultBehavior
        public DataId craftSkillDid; // m_didCraftSkill
        public int objectBaseQuantity; // m_iObjectBaseQuantity
        public float objectQuantityVariance; // m_fObjectQuantityVariance
        public DataId butcheryProfileDid; // m_didButcheryProfile
        public float defaultTimeScale; // m_defaultTimeScale

        public MineUsageBlob() : base() {

        }

        public MineUsageBlob(AC2Reader data) : base(data) {
            defaultRepeatCount = data.ReadUInt32();
            objectDid = data.ReadDataId();
            defaultBehavior = data.ReadUInt32();
            craftSkillDid = data.ReadDataId();
            objectBaseQuantity = data.ReadInt32();
            objectQuantityVariance = data.ReadSingle();
            butcheryProfileDid = data.ReadDataId();
            defaultTimeScale = data.ReadSingle();
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.Write(defaultRepeatCount);
            data.Write(objectDid);
            data.Write(defaultBehavior);
            data.Write(craftSkillDid);
            data.Write(objectBaseQuantity);
            data.Write(objectQuantityVariance);
            data.Write(butcheryProfileDid);
            data.Write(defaultTimeScale);
        }
    }
}
