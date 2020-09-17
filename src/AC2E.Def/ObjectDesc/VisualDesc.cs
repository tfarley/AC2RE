using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Def {

    public class VisualDesc : IPackage {

        // Dat file 70000390
        public static readonly Dictionary<PhysiqueType, AppearanceKey> PHYSIQUE_TO_APPEARANCE = new Dictionary<PhysiqueType, AppearanceKey> {
            { PhysiqueType.SKIN_TONE, AppearanceKey.SKINCOLOR },
            { PhysiqueType.SKIN_DETAIL, AppearanceKey.SKINTEXTURE },
            { PhysiqueType.HEAD_DETAIL, AppearanceKey.HEADMESH },
            { PhysiqueType.HEAD_FRILL, AppearanceKey.BEARDMESH },
            { PhysiqueType.FRILL_COLOR, AppearanceKey.HEADCOLOR },
            { PhysiqueType.SPECIAL, AppearanceKey.SPECIAL },
            { PhysiqueType.SHIRT_CLOTHING_COLOR, AppearanceKey.CLOTHINGCOLOR },
            { PhysiqueType.PANTS_CLOTHING_COLOR, AppearanceKey.CLOTHINGCOLOR },
            { PhysiqueType.BOOTS_CLOTHING_COLOR, AppearanceKey.CLOTHINGCOLOR },
            { PhysiqueType.FACE_DETAIL, AppearanceKey.FACETEXTURE },
            { PhysiqueType.EYE_COLOR, AppearanceKey.EYES },
        };

        public NativeType nativeType => NativeType.VISUALDESC;

        // Enum VisualDescPack::Flag
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            DATABASE = 1 << 0, // 0x00000001
            PARENT = 1 << 1, // 0x00000002
            MIDESC = 1 << 2, // 0x00000004
            BEHAVIOR = 1 << 3, // 0x00000008
            MODES = 1 << 4, // 0x00000010
            SCALE = 1 << 5, // 0x00000020
            CHILDSCALE = 1 << 6, // 0x00000040
            ICONDESC = 1 << 7, // 0x00000080
            GLOBALMOD = 1 << 8, // 0x00000100
            PGDTABLE = 1 << 9, // 0x00000200
            PARTICLESCALE = 1 << 10, // 0x00000400
        }

        public PackFlag packFlags;
        public DataId did; // m_DID
        public DataId parentDid; // m_parent_did
        public DataId motionInterpDescDid; // m_midesc_did
        public DataId behaviorTableDid; // m_behaviortable_did
        public DataId modesDid; // m_modes_did
        public Vector3 scale; // m_scale
        public float childScale; // m_child_scale
        public float particleScale; // m_particle_scale
        public PartGroupDataDesc root; // m_root
        public Dictionary<uint, PartGroupDataDesc> pgdDescTable; // m_pgd_desc_table
        public PartGroupDataDesc globalAppearanceModifiers; // m_global_app_mods
        public IconDesc iconDesc; // m_icon_desc
        public string creationTimestamp; // m_creation_timestamp

        public VisualDesc() {

        }

        public VisualDesc(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.DATABASE)) {
                did = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                parentDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.MIDESC)) {
                motionInterpDescDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.BEHAVIOR)) {
                behaviorTableDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.MODES)) {
                modesDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.SCALE)) {
                scale = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.CHILDSCALE)) {
                childScale = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.PARTICLESCALE)) {
                particleScale = data.ReadSingle();
            }
            if (packFlags.HasFlag(PackFlag.ICONDESC)) {
                iconDesc = new IconDesc(data);
            }
            if (packFlags.HasFlag(PackFlag.GLOBALMOD)) {
                globalAppearanceModifiers = new PartGroupDataDesc(data);
            }
            if (packFlags.HasFlag(PackFlag.PGDTABLE)) {
                pgdDescTable = data.ReadDictionary(data.ReadUInt32, () => new PartGroupDataDesc(data));
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.DATABASE)) {
                data.Write(did);
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                data.Write(parentDid);
            }
            if (packFlags.HasFlag(PackFlag.MIDESC)) {
                data.Write(motionInterpDescDid);
            }
            if (packFlags.HasFlag(PackFlag.BEHAVIOR)) {
                data.Write(behaviorTableDid);
            }
            if (packFlags.HasFlag(PackFlag.MODES)) {
                data.Write(modesDid);
            }
            if (packFlags.HasFlag(PackFlag.SCALE)) {
                data.Write(scale);
            }
            if (packFlags.HasFlag(PackFlag.CHILDSCALE)) {
                data.Write(childScale);
            }
            if (packFlags.HasFlag(PackFlag.PARTICLESCALE)) {
                data.Write(particleScale);
            }
            if (packFlags.HasFlag(PackFlag.ICONDESC)) {
                iconDesc.write(data);
            }
            if (packFlags.HasFlag(PackFlag.GLOBALMOD)) {
                globalAppearanceModifiers.write(data);
            }
            if (packFlags.HasFlag(PackFlag.PGDTABLE)) {
                data.Write(pgdDescTable, data.Write, v => v.write(data));
            }
        }
    }
}
