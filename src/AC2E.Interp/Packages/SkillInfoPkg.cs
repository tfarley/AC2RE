using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class SkillInfoPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.SkillInfo;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        public double m_timeLastUsed;
        public uint m_levelCached;
        public double m_timeCached;
        public ulong m_nXPAllocated;
        public uint m_mask;
        public double m_timeGranted;
        public uint m_nTrainedChildren;
        public uint m_nTrainedDependents;
        public uint m_nCostWhenLearned;
        public uint m_nSkillOverride;
        public uint m_typeSkill;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_timeLastUsed);
            data.Write(m_levelCached);
            data.Write(m_timeCached);
            data.Write(m_nXPAllocated);
            data.Write(m_mask);
            data.Write(m_timeGranted);
            data.Write(m_nTrainedChildren);
            data.Write(m_nTrainedDependents);
            data.Write(m_nCostWhenLearned);
            data.Write(m_nSkillOverride);
            data.Write(m_typeSkill);
            // TODO: Need to write field types at the end, BUT there are no reference fields here - so how to determine when to write that info?
        }
    }
}
