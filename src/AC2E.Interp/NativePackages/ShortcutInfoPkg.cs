using AC2E.Dat;
using AC2E.Def;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Interp {

    public class ShortcutInfoPkg : IPackage {

        public NativeType nativeType => NativeType.SHORTCUTINFO;

        public ShortcutType _type;
        public string _data_str;
        public InstanceId _data_iid;
        public uint _data_enum;
        public DataId _data_dataid;

        public ShortcutInfoPkg() {

        }

        public ShortcutInfoPkg(BinaryReader data) {
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
                    _data_str = data.ReadEncryptedString(Encoding.Unicode);
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
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
                    data.WriteEncryptedString(_data_str, Encoding.Unicode);
                    break;
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
