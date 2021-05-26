using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CBaseQualities {

        // Const Packed_*
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            WEENIE_DESC = 1 << 0, // 0x00000001
            INT_HASH_TABLE = 1 << 1, // 0x00000002
            BOOL_HASH_TABLE = 1 << 2, // 0x00000004
            FLOAT_HASH_TABLE = 1 << 3, // 0x00000008
            TIMESTAMP_HASH_TABLE = 1 << 4, // 0x00000010
            DATA_ID_HASH_TABLE = 1 << 5, // 0x00000020
            INSTANCE_ID_HASH_TABLE = 1 << 6, // 0x00000040
            STRING_HASH_TABLE = 1 << 7, // 0x00000080
            POSITION_HASH_TABLE = 1 << 8, // 0x00000100
            STRING_INFO_HASH_TABLE = 1 << 9, // 0x00000200
            PACKAGE_ID_HASH_TABLE = 1 << 10, // 0x00000400
            LONG_INT_HASH_TABLE = 1 << 11, // 0x00000800
        }

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
        public Dictionary<uint, PackageId> packageIds; // m_pid_table

        public CBaseQualities() {

        }

        public CBaseQualities(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            did = data.ReadDataId();
            if (packFlags.HasFlag(PackFlag.WEENIE_DESC)) {
                weenieDesc = new(data);
            }
            if (packFlags.HasFlag(PackFlag.INT_HASH_TABLE)) {
                ints = data.ReadDictionary(() => (IntStat)data.ReadUInt32(), data.ReadInt32);
            }
            if (packFlags.HasFlag(PackFlag.BOOL_HASH_TABLE)) {
                bools = data.ReadDictionary(() => (BoolStat)data.ReadUInt32(), data.ReadBoolean);
            }
            if (packFlags.HasFlag(PackFlag.FLOAT_HASH_TABLE)) {
                floats = data.ReadDictionary(() => (FloatStat)data.ReadUInt32(), data.ReadSingle);
            }
            if (packFlags.HasFlag(PackFlag.TIMESTAMP_HASH_TABLE)) {
                doubles = data.ReadDictionary(() => (TimestampStat)data.ReadUInt32(), data.ReadDouble);
            }
            if (packFlags.HasFlag(PackFlag.STRING_HASH_TABLE)) {
                strings = data.ReadDictionary(() => (StringStat)data.ReadUInt32(), data.ReadString);
            }
            if (packFlags.HasFlag(PackFlag.DATA_ID_HASH_TABLE)) {
                dids = data.ReadDictionary(() => (DataIdStat)data.ReadUInt32(), data.ReadDataId);
            }
            if (packFlags.HasFlag(PackFlag.INSTANCE_ID_HASH_TABLE)) {
                ids = data.ReadDictionary(() => (InstanceIdStat)data.ReadUInt32(), data.ReadInstanceId);
            }
            if (packFlags.HasFlag(PackFlag.POSITION_HASH_TABLE)) {
                poss = data.ReadDictionary(() => (PositionStat)data.ReadUInt32(), () => new Position(data));
            }
            if (packFlags.HasFlag(PackFlag.STRING_INFO_HASH_TABLE)) {
                stringInfos = data.ReadDictionary(() => (StringInfoStat)data.ReadUInt32(), () => new StringInfo(data));
            }
            if (packFlags.HasFlag(PackFlag.PACKAGE_ID_HASH_TABLE)) {
                packageIds = data.ReadDictionary(data.ReadUInt32, data.ReadPackageId);
            }
            if (packFlags.HasFlag(PackFlag.LONG_INT_HASH_TABLE)) {
                longs = data.ReadDictionary(() => (LongIntStat)data.ReadUInt32(), data.ReadInt64);
            }
        }

        public void write(AC2Writer data) {
            packFlags = 0;
            if (weenieDesc != default) packFlags |= PackFlag.WEENIE_DESC;
            if (ints != default) packFlags |= PackFlag.INT_HASH_TABLE;
            if (bools != default) packFlags |= PackFlag.BOOL_HASH_TABLE;
            if (floats != default) packFlags |= PackFlag.FLOAT_HASH_TABLE;
            if (doubles != default) packFlags |= PackFlag.TIMESTAMP_HASH_TABLE;
            if (strings != default) packFlags |= PackFlag.STRING_HASH_TABLE;
            if (dids != default) packFlags |= PackFlag.DATA_ID_HASH_TABLE;
            if (ids != default) packFlags |= PackFlag.INSTANCE_ID_HASH_TABLE;
            if (poss != default) packFlags |= PackFlag.POSITION_HASH_TABLE;
            if (stringInfos != default) packFlags |= PackFlag.STRING_INFO_HASH_TABLE;
            if (packageIds != default) packFlags |= PackFlag.PACKAGE_ID_HASH_TABLE;
            if (longs != default) packFlags |= PackFlag.LONG_INT_HASH_TABLE;

            data.Write((uint)packFlags);
            data.Write(did);
            if (packFlags.HasFlag(PackFlag.WEENIE_DESC)) {
                weenieDesc.write(data);
            }
            if (packFlags.HasFlag(PackFlag.INT_HASH_TABLE)) {
                data.Write(ints, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.BOOL_HASH_TABLE)) {
                data.Write(bools, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.FLOAT_HASH_TABLE)) {
                data.Write(floats, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.TIMESTAMP_HASH_TABLE)) {
                data.Write(doubles, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.STRING_HASH_TABLE)) {
                data.Write(strings, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.DATA_ID_HASH_TABLE)) {
                data.Write(dids, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.INSTANCE_ID_HASH_TABLE)) {
                data.Write(ids, v => data.Write((uint)v), data.Write);
            }
            if (packFlags.HasFlag(PackFlag.POSITION_HASH_TABLE)) {
                data.Write(poss, v => data.Write((uint)v), v => v.write(data));
            }
            if (packFlags.HasFlag(PackFlag.STRING_INFO_HASH_TABLE)) {
                data.Write(stringInfos, v => data.Write((uint)v), v => v.write(data));
            }
            if (packFlags.HasFlag(PackFlag.PACKAGE_ID_HASH_TABLE)) {
                data.Write(packageIds, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.LONG_INT_HASH_TABLE)) {
                data.Write(longs, v => data.Write((uint)v), data.Write);
            }
        }
    }
}
