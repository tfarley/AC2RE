using AC2E.Def.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def.Structs {

    public class VisualDesc {

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

        public class AppearanceInfo {

            public Dictionary<uint, float> m_appkeyHash;

            public AppearanceInfo(BinaryReader data) {
                m_appkeyHash = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
            }

            public void write(BinaryWriter data) {
                data.Write(m_appkeyHash, data.Write, data.Write);
            }
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
            public PartGroupKey m_key;
            public PartGroupKey m_parent_key;
            public uint m_conn_pt;
            public DataId m_setupDID;
            public DataId m_animMapDID;
            public Dictionary<uint, AppearanceInfo> m_app_hash;
            public DataId m_fxtable_did;
            public Dictionary<uint, float> m_startup_fx;
            //public Dictionary<uint, List<FXData>> m_fx_overrides; // TODO: Figure this one out, need to find an example in pcaps

            public PartGroupDataDesc(BinaryReader data) {
                // TODO: Need to verify parsing of all of these properties
                packFlags = (PackFlag)data.ReadUInt32();
                if (packFlags.HasFlag(PackFlag.KEY)) {
                    m_key = (PartGroupKey)data.ReadUInt32();
                }
                if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
                    m_parent_key = (PartGroupKey)data.ReadUInt32();
                }
                if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
                    m_conn_pt = data.ReadUInt32();
                }
                if (packFlags.HasFlag(PackFlag.SETUP)) {
                    m_setupDID = data.ReadDataId();
                }
                if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
                    m_animMapDID = data.ReadDataId();
                }
                if (packFlags.HasFlag(PackFlag.APPHASH)) {
                    m_app_hash = data.ReadDictionary(data.ReadUInt32, () => new AppearanceInfo(data));
                }
                if (packFlags.HasFlag(PackFlag.FXTABLE)) {
                    m_fxtable_did = data.ReadDataId();
                }
                if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
                    m_startup_fx = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
                }
                if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
                    throw new NotImplementedException();
                }
            }

            public void write(BinaryWriter data) {
                data.Write((uint)packFlags);
                if (packFlags.HasFlag(PackFlag.KEY)) {
                    data.Write((uint)m_key);
                }
                if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
                    data.Write((uint)m_parent_key);
                }
                if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
                    data.Write(m_conn_pt);
                }
                if (packFlags.HasFlag(PackFlag.SETUP)) {
                    data.Write(m_setupDID);
                }
                if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
                    data.Write(m_animMapDID);
                }
                if (packFlags.HasFlag(PackFlag.APPHASH)) {
                    data.Write(m_app_hash, data.Write, v => v.write(data));
                }
                if (packFlags.HasFlag(PackFlag.FXTABLE)) {
                    data.Write(m_fxtable_did);
                }
                if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
                    data.Write(m_startup_fx, data.Write, data.Write);
                }
                if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
                    throw new NotImplementedException();
                }
            }
        }

        public PackFlag packFlags;
        public DataId m_parent_did;
        public Vector m_scale;
        public PartGroupDataDesc m_global_app_mods;
        // TODO: More fields here

        public VisualDesc() {

        }

        public VisualDesc(BinaryReader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                m_parent_did = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.SCALE)) {
                m_scale = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.GLOBALMOD)) {
                m_global_app_mods = new PartGroupDataDesc(data);
            }
        }

        public void write(BinaryWriter data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.PARENT)) {
                data.Write(m_parent_did);
            }
            if (packFlags.HasFlag(PackFlag.SCALE)) {
                data.Write(m_scale);
            }
            if (packFlags.HasFlag(PackFlag.GLOBALMOD)) {
                m_global_app_mods.write(data);
            }
        }
    }
}
