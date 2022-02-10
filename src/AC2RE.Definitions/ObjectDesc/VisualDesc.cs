using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Definitions;

public class VisualDesc : IHeapObject {

    // Dat file 70000390
    public static readonly Dictionary<PhysiqueType, AppearanceKey> PHYSIQUE_TO_APPEARANCE = new() {
        { PhysiqueType.SkinTone, AppearanceKey.SkinColor },
        { PhysiqueType.SkinDetail, AppearanceKey.SkinTexture },
        { PhysiqueType.HeadDetail, AppearanceKey.HeadMesh },
        { PhysiqueType.HeadFrill, AppearanceKey.BeardMesh },
        { PhysiqueType.FrillColor, AppearanceKey.HeadColor },
        { PhysiqueType.Special, AppearanceKey.Special },
        { PhysiqueType.ShirtClothingColor, AppearanceKey.ClothingColor },
        { PhysiqueType.PantsClothingColor, AppearanceKey.ClothingColor },
        { PhysiqueType.BootsClothingColor, AppearanceKey.ClothingColor },
        { PhysiqueType.FaceDetail, AppearanceKey.FaceTexture },
        { PhysiqueType.EyeColor, AppearanceKey.eyes },
    };

    public NativeType nativeType => NativeType.VisualDesc;

    // Enum VisualDescPack::Flag
    [Flags]
    public enum PackFlag : uint {
        NONE = 0,
        DATABASE = 1 << 0, // DATABASE 0x00000001
        PARENT = 1 << 1, // PARENT 0x00000002
        MIDESC = 1 << 2, // MIDESC 0x00000004
        BEHAVIOR = 1 << 3, // BEHAVIOR 0x00000008
        MODES = 1 << 4, // MODES 0x00000010
        SCALE = 1 << 5, // SCALE 0x00000020
        CHILDSCALE = 1 << 6, // CHILDSCALE 0x00000040
        ICONDESC = 1 << 7, // ICONDESC 0x00000080
        GLOBALMOD = 1 << 8, // GLOBALMOD 0x00000100
        PGDTABLE = 1 << 9, // PGDTABLE 0x00000200
        PARTICLESCALE = 1 << 10, // PARTICLESCALE 0x00000400
    }

    // VisualDesc
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
            iconDesc = new(data);
        }
        if (packFlags.HasFlag(PackFlag.GLOBALMOD)) {
            globalAppearanceModifiers = new(data);
        }
        if (packFlags.HasFlag(PackFlag.PGDTABLE)) {
            pgdDescTable = data.ReadDictionary(data.ReadUInt32, () => new PartGroupDataDesc(data));
        }
    }

    public void write(AC2Writer data) {
        packFlags = 0;
        if (did != default) packFlags |= PackFlag.DATABASE;
        if (parentDid != default) packFlags |= PackFlag.PARENT;
        if (motionInterpDescDid != default) packFlags |= PackFlag.MIDESC;
        if (behaviorTableDid != default) packFlags |= PackFlag.BEHAVIOR;
        if (modesDid != default) packFlags |= PackFlag.MODES;
        if (scale != default) packFlags |= PackFlag.SCALE;
        if (childScale != default) packFlags |= PackFlag.CHILDSCALE;
        if (particleScale != default) packFlags |= PackFlag.PARTICLESCALE;
        if (iconDesc != default) packFlags |= PackFlag.ICONDESC;
        if (globalAppearanceModifiers != default) packFlags |= PackFlag.GLOBALMOD;
        if (pgdDescTable != default) packFlags |= PackFlag.PGDTABLE;

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
