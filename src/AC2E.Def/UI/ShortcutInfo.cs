using System.IO;
using System.Text;

namespace AC2E.Def {

    public class ShortcutInfo : IPackage {

        public NativeType nativeType => NativeType.SHORTCUTINFO;

        public ShortcutType type; // _type
        public string dataStr; // _data_str
        public InstanceId dataId; // _data_iid
        public uint dataEnum; // _data_enum
        public DataId dataDid; // _data_dataid

        public ShortcutInfo() {

        }

        public ShortcutInfo(AC2Reader data) {
            // TODO: Guessing on the types here
            type = (ShortcutType)data.ReadUInt32();
            switch (type) {
                case ShortcutType.UNDEF:
                    break;
                case ShortcutType.SKILL:
                case ShortcutType.RECIPE:
                case ShortcutType.NEW_RECIPE:
                    dataDid = data.ReadDataId();
                    break;
                case ShortcutType.ITEM:
                    dataId = data.ReadInstanceId();
                    break;
                case ShortcutType.ALIAS:
                    dataStr = data.ReadString(Encoding.Unicode);
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }

        public void write(AC2Writer data) {
            // TODO: Guessing on the types here
            data.Write((uint)type);
            switch (type) {
                case ShortcutType.UNDEF:
                    break;
                case ShortcutType.SKILL:
                case ShortcutType.RECIPE:
                case ShortcutType.NEW_RECIPE:
                    data.Write(dataDid);
                    break;
                case ShortcutType.ITEM:
                    data.Write(dataId);
                    break;
                case ShortcutType.ALIAS:
                    data.Write(dataStr, Encoding.Unicode);
                    break;
                default:
                    throw new InvalidDataException(type.ToString());
            }
        }
    }
}
