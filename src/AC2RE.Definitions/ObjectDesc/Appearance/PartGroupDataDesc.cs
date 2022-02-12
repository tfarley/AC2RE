using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PartGroupDataDesc {

    // Dat file 23000018 (internal) / Const *_PartGroupKey
    public enum PartGroupKey : uint {
        Invalid = 0, // Invalid_PartGroupKey

        DefaultBody = 0x40000001, // _ / DefaultBody_PartGroupKey
        EntireTree = 0x40000002, // _ / EntireTree_PartGroupKey

        Body = 2164260865, // Body
        Head = 2164260866, // Head
        Beard = 2164260867, // Beard
        Crest = 2164260868, // Crest
        Tail = 2164260869, // Tail
        TailFrill = 2164260870, // TailFrill
        Mount = 2164260871, // Mount
        Rider = 2164260872, // Rider
        Camera = 2164260873, // Camera
        BackSlot = 2164260874, // BackSlot
        Helmet = 2164260875, // Helmet
    }

    // Enum PGDDPack::Flag
    [Flags]
    public enum PackFlag : uint {
        NONE = 0,
        KEY = 1 << 0, // KEY 0x00000001
        PARENTKEY = 1 << 1, // PARENTKEY 0x00000002
        CONNECTIONPOINT = 1 << 2, // CONNECTIONPOINT 0x00000004
        SETUP = 1 << 3, // SETUP 0x00000008
        ANIMMAP = 1 << 4, // ANIMMAP 0x00000010
        APPHASH = 1 << 5, // APPHASH 0x00000020
        FXTABLE = 1 << 6, // FXTABLE 0x00000040
        STARTUPFX = 1 << 7, // STARTUPFX 0x00000080
        FXOVERRIDES = 1 << 8, // FXOVERRIDES 0x00000100
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
        packFlags = data.ReadEnum<PackFlag>();
        if (packFlags.HasFlag(PackFlag.KEY)) {
            key = data.ReadEnum<PartGroupKey>();
        }
        if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
            parentKey = data.ReadEnum<PartGroupKey>();
        }
        if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
            connectionPoint = data.ReadEnum<ConnectionPoint>();
        }
        if (packFlags.HasFlag(PackFlag.SETUP)) {
            setupDid = data.ReadDataId();
        }
        if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
            animMapDid = data.ReadDataId();
        }
        if (packFlags.HasFlag(PackFlag.APPHASH)) {
            appearanceInfos = data.ReadDictionary(data.ReadDataId, () => data.ReadDictionary(data.ReadEnum<AppearanceKey>, data.ReadSingle));
        }
        if (packFlags.HasFlag(PackFlag.FXTABLE)) {
            fxTableDid = data.ReadDataId();
        }
        if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
            startupFx = data.ReadDictionary(data.ReadEnum<FxId>, data.ReadSingle);
        }
        if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
            fxOverrides = data.ReadDictionary(data.ReadEnum<FxId>, () => data.ReadList(() => new FXData(data)));
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

        data.WriteEnum(packFlags);
        if (packFlags.HasFlag(PackFlag.KEY)) {
            data.WriteEnum(key);
        }
        if (packFlags.HasFlag(PackFlag.PARENTKEY)) {
            data.WriteEnum(parentKey);
        }
        if (packFlags.HasFlag(PackFlag.CONNECTIONPOINT)) {
            data.WriteEnum(connectionPoint);
        }
        if (packFlags.HasFlag(PackFlag.SETUP)) {
            data.Write(setupDid);
        }
        if (packFlags.HasFlag(PackFlag.ANIMMAP)) {
            data.Write(animMapDid);
        }
        if (packFlags.HasFlag(PackFlag.APPHASH)) {
            data.Write(appearanceInfos, data.Write, v => data.Write(v, data.WriteEnum, data.Write));
        }
        if (packFlags.HasFlag(PackFlag.FXTABLE)) {
            data.Write(fxTableDid);
        }
        if (packFlags.HasFlag(PackFlag.STARTUPFX)) {
            data.Write(startupFx, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.FXOVERRIDES)) {
            data.Write(fxOverrides, data.WriteEnum, v => data.Write(v, v => v.write(data)));
        }
    }
}
