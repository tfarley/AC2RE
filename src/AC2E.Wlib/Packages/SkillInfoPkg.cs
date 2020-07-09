﻿using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class SkillInfoPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.SkillInfo;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

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

        public SkillInfoPkg() {

        }

        public SkillInfoPkg(BinaryReader data) {
            m_timeLastUsed = data.ReadDouble();
            m_levelCached = data.ReadUInt32();
            m_timeCached = data.ReadDouble();
            m_nXPAllocated = data.ReadUInt64();
            m_mask = data.ReadUInt32();
            m_timeGranted = data.ReadDouble();
            m_nTrainedChildren = data.ReadUInt32();
            m_nTrainedDependents = data.ReadUInt32();
            m_nCostWhenLearned = data.ReadUInt32();
            m_nSkillOverride = data.ReadUInt32();
            m_typeSkill = data.ReadUInt32();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
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
        }
    }
}
