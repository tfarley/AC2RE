﻿using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public class CBaseQualities {

        // Const Packed_*
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
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
        public Dictionary<uint, int> intTable; // m_int_table
        public Dictionary<uint, long> longTable; // m_lint_table
        public Dictionary<uint, bool> boolTable; // m_pbool_table
        public Dictionary<uint, float> floatTable; // m_float_table
        public Dictionary<uint, double> doubleTable; // m_ts_table
        public Dictionary<uint, string> stringTable; // m_str_table
        public Dictionary<uint, DataId> dataIdTable; // m_did_table
        public Dictionary<uint, InstanceId> instanceIdTable; // m_iid_table
        public Dictionary<uint, Position> posTable; // m_pos_table
        public Dictionary<uint, StringInfo> stringInfoTable; // m_si_table
        public Dictionary<uint, PackageId> packageIdTable; // m_pid_table

        public CBaseQualities() {

        }

        public CBaseQualities(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            did = data.ReadDataId();
            if (packFlags.HasFlag(PackFlag.WEENIE_DESC)) {
                weenieDesc = new WeenieDesc(data);
            }
            if (packFlags.HasFlag(PackFlag.INT_HASH_TABLE)) {
                intTable = data.ReadDictionary(data.ReadUInt32, data.ReadInt32);
            }
            if (packFlags.HasFlag(PackFlag.BOOL_HASH_TABLE)) {
                boolTable = data.ReadDictionary(data.ReadUInt32, data.ReadBoolean);
            }
            if (packFlags.HasFlag(PackFlag.FLOAT_HASH_TABLE)) {
                floatTable = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
            }
            if (packFlags.HasFlag(PackFlag.TIMESTAMP_HASH_TABLE)) {
                doubleTable = data.ReadDictionary(data.ReadUInt32, data.ReadDouble);
            }
            if (packFlags.HasFlag(PackFlag.STRING_HASH_TABLE)) {
                stringTable = data.ReadDictionary(data.ReadUInt32, data.ReadString);
            }
            if (packFlags.HasFlag(PackFlag.DATA_ID_HASH_TABLE)) {
                dataIdTable = data.ReadDictionary(data.ReadUInt32, data.ReadDataId);
            }
            if (packFlags.HasFlag(PackFlag.INSTANCE_ID_HASH_TABLE)) {
                instanceIdTable = data.ReadDictionary(data.ReadUInt32, data.ReadInstanceId);
            }
            if (packFlags.HasFlag(PackFlag.POSITION_HASH_TABLE)) {
                posTable = data.ReadDictionary(data.ReadUInt32, () => new Position(data));
            }
            if (packFlags.HasFlag(PackFlag.STRING_INFO_HASH_TABLE)) {
                stringInfoTable = data.ReadDictionary(data.ReadUInt32, () => new StringInfo(data));
            }
            if (packFlags.HasFlag(PackFlag.PACKAGE_ID_HASH_TABLE)) {
                packageIdTable = data.ReadDictionary(data.ReadUInt32, data.ReadPackageId);
            }
            if (packFlags.HasFlag(PackFlag.LONG_INT_HASH_TABLE)) {
                longTable = data.ReadDictionary(data.ReadUInt32, data.ReadInt64);
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)packFlags);
            data.Write(did);
            if (packFlags.HasFlag(PackFlag.WEENIE_DESC)) {
                weenieDesc.write(data);
            }
            if (packFlags.HasFlag(PackFlag.INT_HASH_TABLE)) {
                data.Write(intTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.BOOL_HASH_TABLE)) {
                data.Write(boolTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.FLOAT_HASH_TABLE)) {
                data.Write(floatTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.TIMESTAMP_HASH_TABLE)) {
                data.Write(doubleTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.STRING_HASH_TABLE)) {
                data.Write(stringTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.DATA_ID_HASH_TABLE)) {
                data.Write(dataIdTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.INSTANCE_ID_HASH_TABLE)) {
                data.Write(instanceIdTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.POSITION_HASH_TABLE)) {
                data.Write(posTable, data.Write, v => v.write(data));
            }
            if (packFlags.HasFlag(PackFlag.STRING_INFO_HASH_TABLE)) {
                data.Write(stringInfoTable, data.Write, v => v.write(data));
            }
            if (packFlags.HasFlag(PackFlag.PACKAGE_ID_HASH_TABLE)) {
                data.Write(packageIdTable, data.Write, data.Write);
            }
            if (packFlags.HasFlag(PackFlag.LONG_INT_HASH_TABLE)) {
                data.Write(longTable, data.Write, data.Write);
            }
        }
    }
}
