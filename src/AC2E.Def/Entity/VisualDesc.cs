using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class VisualDesc : IPackage {

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

        // Const *_PartGroupKey
        public enum PartGroupKey : uint {
            INVALID = 0,
            DEFAULT_BODY = 0x40000001,
            ENTIRE_TREE = 0x40000002,
        }

        public class PartGroupDataDesc {

            // Enum PGDDPack::Flag
            [Flags]
            public enum PackFlag : uint {
                NONE = 0,
                KEY = 1 << 0,
                PARENTKEY = 1 << 1,
                CONNECTIONPOINT = 1 << 2,
                SETUP = 1 << 3,
                ANIMMAP = 1 << 4,
                APPHASH = 1 << 5,
                FXTABLE = 1 << 6,
                STARTUPFX = 1 << 7,
                FXOVERRIDES = 1 << 8,
            }

            public PackFlag packFlags;
            public PartGroupKey key; // m_key
            public PartGroupKey parentKey; // m_parent_key
            public uint connectionPoint; // m_conn_pt
            public DataId setupDid; // m_setupDID
            public DataId animMapDid; // m_animMapDID
            public Dictionary<uint, AppearanceInfo> appearances; // m_app_hash
            public DataId fxTableDid; // m_fxtable_did
            public Dictionary<uint, float> startupFx; // m_startup_fx
            //public Dictionary<uint, List<FXData>> fxOverrides; // m_fx_overrides // TODO: Figure this one out, need to find an example in pcaps

            public PartGroupDataDesc() {

            }

            public PartGroupDataDesc(BinaryReader data) {
                // TODO: Need to verify parsing of all of these properties
                packFlags = (PackFlag)data.ReadUInt32();
                if (packFlags.HasFlag(PackFlag.KEY)) {
                    key = (PartGroupKey)data.ReadUInt32();
                }
                if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
                    parentKey = (PartGroupKey)data.ReadUInt32();
                }
                if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
                    connectionPoint = data.ReadUInt32();
                }
                if (packFlags.HasFlag(PackFlag.SETUP)) {
                    setupDid = data.ReadDataId();
                }
                if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
                    animMapDid = data.ReadDataId();
                }
                if (packFlags.HasFlag(PackFlag.APPHASH)) {
                    appearances = data.ReadDictionary(data.ReadUInt32, () => new AppearanceInfo(data));
                }
                if (packFlags.HasFlag(PackFlag.FXTABLE)) {
                    fxTableDid = data.ReadDataId();
                }
                if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
                    startupFx = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
                }
                if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
                    throw new NotImplementedException("PartGroupDataDesc.fxOverrides");
                }
            }

            public void write(BinaryWriter data) {
                data.Write((uint)packFlags);
                if (packFlags.HasFlag(PackFlag.KEY)) {
                    data.Write((uint)key);
                }
                if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
                    data.Write((uint)parentKey);
                }
                if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
                    data.Write(connectionPoint);
                }
                if (packFlags.HasFlag(PackFlag.SETUP)) {
                    data.Write(setupDid);
                }
                if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
                    data.Write(animMapDid);
                }
                if (packFlags.HasFlag(PackFlag.APPHASH)) {
                    data.Write(appearances, data.Write, v => v.write(data));
                }
                if (packFlags.HasFlag(PackFlag.FXTABLE)) {
                    data.Write(fxTableDid);
                }
                if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
                    data.Write(startupFx, data.Write, data.Write);
                }
                if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
                    throw new NotImplementedException("PartGroupDataDesc.fxOverrides");
                }
            }
        }

        public PackFlag packFlags;
        public DataId parentDid; // m_parent_did
        public DataId miDescDid; // m_midesc_did
        public DataId behaviorTableDid; // m_behaviortable_did
        public DataId modesDid; // m_modes_did
        public Vector scale; // m_scale
        public float childScale; // m_child_scale
        public float particleScale; // m_particle_scale
        public PartGroupDataDesc root; // m_root
        public Dictionary<uint, PartGroupDataDesc> pgdDescTable; // m_pgd_desc_table
        public PartGroupDataDesc globalAppearanceModifiers; // m_global_app_mods
        public IconDesc iconDesc; // m_icon_desc
        public string creationTimestamp; // m_creation_timestamp

        public VisualDesc() {

        }

        public VisualDesc(BinaryReader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.DATABASE)) {
                throw new NotImplementedException("VisualDesc.database");
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                parentDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.MIDESC)) {
                miDescDid = data.ReadDataId();
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

        public void write(BinaryWriter data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.DATABASE)) {
                throw new NotImplementedException("VisualDesc.database");
            }
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                data.Write(parentDid);
            }
            if (packFlags.HasFlag(PackFlag.MIDESC)) {
                data.Write(miDescDid);
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

        public void write(BinaryWriter data, PackageRegistry registry) {
            write(data);
        }
    }
}
