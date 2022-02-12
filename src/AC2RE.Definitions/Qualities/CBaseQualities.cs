using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CBaseQualities {

    // Const Packed_*
    [Flags]
    public enum PackFlag : uint {
        None = 0, // Packed_None
        WeenieDesc = 1 << 0, // Packed_WeenieDesc 0x00000001
        IntHashTable = 1 << 1, // Packed_IntHashTable 0x00000002
        BoolHashTable = 1 << 2, // Packed_BoolHashTable 0x00000004
        FloatHashTable = 1 << 3, // Packed_FloatHashTable 0x00000008
        TSHashTable = 1 << 4, // Packed_TSHashTable 0x00000010
        DataIDHashTable = 1 << 5, // Packed_DataIDHashTable 0x00000020
        InstanceIDHashTable = 1 << 6, // Packed_InstanceIDHashTable 0x00000040
        StringHashTable = 1 << 7, // Packed_StringHashTable 0x00000080
        PositionHashTable = 1 << 8, // Packed_PositionHashTable 0x00000100
        StringInfoHashTable = 1 << 9, // Packed_StringInfoHashTable 0x00000200
        PackageIDHashTable = 1 << 10, // Packed_PackageIDHashTable 0x00000400
        LongIntHashTable = 1 << 11, // Packed_LongIntHashTable 0x00000800
    }

    // CBaseQualities
    public PackFlag packFlags;
    public DataId did; // via DBObj::Pack_Type
    public WeenieDesc weenieDesc; // m_wdesc
    public Dictionary<IntStat, int> ints; // m_int_table
    public Dictionary<LongIntStat, long> longs; // m_lint_table
    public Dictionary<BoolStat, bool> bools; // m_pbool_table
    public Dictionary<FloatStat, float> floats; // m_float_table
    public Dictionary<TimestampStat, double> doubles; // m_ts_table
    public Dictionary<StringStat, string> strings; // m_str_table
    public Dictionary<DataIdStat, DataId> dids; // m_did_table
    public Dictionary<InstanceIdStat, InstanceId> ids; // m_iid_table
    public Dictionary<PositionStat, Position> poss; // m_pos_table
    public Dictionary<StringInfoStat, StringInfo> stringInfos; // m_si_table
    public Dictionary<uint, ReferenceId> packageIds; // m_pid_table

    public CBaseQualities() {

    }

    public CBaseQualities(AC2Reader data) {
        packFlags = data.ReadEnum<PackFlag>();
        did = data.ReadDataId();
        if (packFlags.HasFlag(PackFlag.WeenieDesc)) {
            weenieDesc = new(data);
        }
        if (packFlags.HasFlag(PackFlag.IntHashTable)) {
            ints = data.ReadDictionary(data.ReadEnum<IntStat>, data.ReadInt32);
        }
        if (packFlags.HasFlag(PackFlag.BoolHashTable)) {
            bools = data.ReadDictionary(data.ReadEnum<BoolStat>, data.ReadBoolean);
        }
        if (packFlags.HasFlag(PackFlag.FloatHashTable)) {
            floats = data.ReadDictionary(data.ReadEnum<FloatStat>, data.ReadSingle);
        }
        if (packFlags.HasFlag(PackFlag.TSHashTable)) {
            doubles = data.ReadDictionary(data.ReadEnum<TimestampStat>, data.ReadDouble);
        }
        if (packFlags.HasFlag(PackFlag.StringHashTable)) {
            strings = data.ReadDictionary(data.ReadEnum<StringStat>, data.ReadString);
        }
        if (packFlags.HasFlag(PackFlag.DataIDHashTable)) {
            dids = data.ReadDictionary(data.ReadEnum<DataIdStat>, data.ReadDataId);
        }
        if (packFlags.HasFlag(PackFlag.InstanceIDHashTable)) {
            ids = data.ReadDictionary(data.ReadEnum<InstanceIdStat>, data.ReadInstanceId);
        }
        if (packFlags.HasFlag(PackFlag.PositionHashTable)) {
            poss = data.ReadDictionary(data.ReadEnum<PositionStat>, () => new Position(data));
        }
        if (packFlags.HasFlag(PackFlag.StringInfoHashTable)) {
            stringInfos = data.ReadDictionary(data.ReadEnum<StringInfoStat>, () => new StringInfo(data));
        }
        if (packFlags.HasFlag(PackFlag.PackageIDHashTable)) {
            packageIds = data.ReadDictionary(data.ReadUInt32, data.ReadReferenceId);
        }
        if (packFlags.HasFlag(PackFlag.LongIntHashTable)) {
            longs = data.ReadDictionary(data.ReadEnum<LongIntStat>, data.ReadInt64);
        }
    }

    public void write(AC2Writer data) {
        packFlags = 0;
        if (weenieDesc != default) packFlags |= PackFlag.WeenieDesc;
        if (ints != default) packFlags |= PackFlag.IntHashTable;
        if (bools != default) packFlags |= PackFlag.BoolHashTable;
        if (floats != default) packFlags |= PackFlag.FloatHashTable;
        if (doubles != default) packFlags |= PackFlag.TSHashTable;
        if (strings != default) packFlags |= PackFlag.StringHashTable;
        if (dids != default) packFlags |= PackFlag.DataIDHashTable;
        if (ids != default) packFlags |= PackFlag.InstanceIDHashTable;
        if (poss != default) packFlags |= PackFlag.PositionHashTable;
        if (stringInfos != default) packFlags |= PackFlag.StringInfoHashTable;
        if (packageIds != default) packFlags |= PackFlag.PackageIDHashTable;
        if (longs != default) packFlags |= PackFlag.LongIntHashTable;

        data.WriteEnum(packFlags);
        data.Write(did);
        if (packFlags.HasFlag(PackFlag.WeenieDesc)) {
            weenieDesc.write(data);
        }
        if (packFlags.HasFlag(PackFlag.IntHashTable)) {
            data.Write(ints, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.BoolHashTable)) {
            data.Write(bools, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.FloatHashTable)) {
            data.Write(floats, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.TSHashTable)) {
            data.Write(doubles, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.StringHashTable)) {
            data.Write(strings, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.DataIDHashTable)) {
            data.Write(dids, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.InstanceIDHashTable)) {
            data.Write(ids, data.WriteEnum, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.PositionHashTable)) {
            data.Write(poss, data.WriteEnum, v => v.write(data));
        }
        if (packFlags.HasFlag(PackFlag.StringInfoHashTable)) {
            data.Write(stringInfos, data.WriteEnum, v => v.write(data));
        }
        if (packFlags.HasFlag(PackFlag.PackageIDHashTable)) {
            data.Write(packageIds, data.Write, data.Write);
        }
        if (packFlags.HasFlag(PackFlag.LongIntHashTable)) {
            data.Write(longs, data.WriteEnum, data.Write);
        }
    }
}
