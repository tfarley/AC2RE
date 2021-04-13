using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class PartGroupDataDesc {

        // Const *_PartGroupKey
        public enum PartGroupKey : uint {
            INVALID = 0,
            DEFAULT_BODY = 0x40000001,
            ENTIRE_TREE = 0x40000002,
            // NOTE: If MSB is set (0x80000000) then it is probably a kind of dynamic/numbered key
        }

        // Enum PGDDPack::Flag
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            KEY = 1 << 0, // 0x00000001
            PARENTKEY = 1 << 1, // 0x00000002
            CONNECTIONPOINT = 1 << 2, // 0x00000004
            SETUP = 1 << 3, // 0x00000008
            ANIMMAP = 1 << 4, // 0x00000010
            APPHASH = 1 << 5, // 0x00000020
            FXTABLE = 1 << 6, // 0x00000040
            STARTUPFX = 1 << 7, // 0x00000080
            FXOVERRIDES = 1 << 8, // 0x00000100
        }

        public PackFlag packFlags;
        public PartGroupKey key; // m_key
        public PartGroupKey parentKey; // m_parent_key
        public ConnectionPoint connectionPoint; // m_conn_pt
        public DataId setupDid; // m_setupDID
        public DataId animMapDid; // m_animMapDID
        public Dictionary<DataId, Dictionary<AppearanceKey, float>> appearanceInfos; // m_app_hash
        public DataId fxTableDid; // m_fxtable_did
        public Dictionary<FxId, float> startupFx; // m_startup_fx
        public Dictionary<FxId, List<FXData>> fxOverrides; // m_fx_overrides

        public PartGroupDataDesc() {

        }

        public PartGroupDataDesc(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.KEY)) {
                key = (PartGroupKey)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
                parentKey = (PartGroupKey)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
                connectionPoint = (ConnectionPoint)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.SETUP)) {
                setupDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
                animMapDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.APPHASH)) {
                appearanceInfos = data.ReadDictionary(data.ReadDataId, () => data.ReadDictionary(() => (AppearanceKey)data.ReadUInt32(), data.ReadSingle));
            }
            if (packFlags.HasFlag(PackFlag.FXTABLE)) {
                fxTableDid = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
                startupFx = data.ReadDictionary(() => (FxId)data.ReadUInt32(), data.ReadSingle);
            }
            if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
                fxOverrides = data.ReadDictionary(() => (FxId)data.ReadUInt32(), () => data.ReadList(() => new FXData(data)));
            }
        }

        public void write(AC2Writer data) {
            packFlags = 0;
            if (key != default) packFlags |= PackFlag.KEY;
            if (parentKey != default) packFlags |= PackFlag.PARENTKEY;
            if (connectionPoint != default) packFlags |= PackFlag.CONNECTIONPOINT;
            if (setupDid != default) packFlags |= PackFlag.SETUP;
            if (animMapDid != default) packFlags |= PackFlag.ANIMMAP;
            if (appearanceInfos != default) packFlags |= PackFlag.APPHASH;
            if (fxTableDid != default) packFlags |= PackFlag.FXTABLE;
            if (startupFx != default) packFlags |= PackFlag.STARTUPFX;
            if (fxOverrides != default) packFlags |= PackFlag.FXOVERRIDES;

            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.KEY)) {
                data.Write((uint)key);
            }
            if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
                data.Write((uint)parentKey);
            }
            if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
                data.Write((uint)connectionPoint);
            }
            if (packFlags.HasFlag(PackFlag.SETUP)) {
                data.Write(setupDid);
            }
            if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
                data.Write(animMapDid);
            }
            if (packFlags.HasFlag(PackFlag.APPHASH)) {
                data.Write(appearanceInfos, data.Write, v => data.Write(v, v => data.Write((uint)v), data.Write));
            }
            if (packFlags.HasFlag(PackFlag.FXTABLE)) {
                data.Write(fxTableDid);
            }
            if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
                data.Write(startupFx, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
                data.Write(fxOverrides, v => data.Write((uint)v), v => data.Write(v, v => v.write(data)));
            }
        }
    }
}
