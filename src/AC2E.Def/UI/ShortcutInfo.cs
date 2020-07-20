﻿using System.IO;
using System.Text;

namespace AC2E.Def {

    public class ShortcutInfo : IPackage {

        public NativeType nativeType => NativeType.SHORTCUTINFO;

        public ShortcutType _type;
        public string _data_str;
        public InstanceId _data_iid;
        public uint _data_enum;
        public DataId _data_dataid;

        public ShortcutInfo() {

        }

        public ShortcutInfo(AC2Reader data) {
            // TODO: Guessing on the types here
            _type = (ShortcutType)data.ReadUInt32();
            switch (_type) {
                case ShortcutType.UNDEF:
                    break;
                case ShortcutType.SKILL:
                case ShortcutType.RECIPE:
                case ShortcutType.NEW_RECIPE:
                    _data_dataid = data.ReadDataId();
                    break;
                case ShortcutType.ITEM:
                    _data_iid = data.ReadInstanceId();
                    break;
                case ShortcutType.ALIAS:
                    _data_str = data.ReadString(Encoding.Unicode);
                    break;
                default:
                    throw new InvalidDataException(_type.ToString());
            }
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            // TODO: Guessing on the types here
            data.Write((uint)_type);
            switch (_type) {
                case ShortcutType.UNDEF:
                    break;
                case ShortcutType.SKILL:
                case ShortcutType.RECIPE:
                case ShortcutType.NEW_RECIPE:
                    data.Write(_data_dataid);
                    break;
                case ShortcutType.ITEM:
                    data.Write(_data_iid);
                    break;
                case ShortcutType.ALIAS:
                    data.Write(_data_str, Encoding.Unicode);
                    break;
                default:
                    throw new InvalidDataException(_type.ToString());
            }
        }
    }
}
