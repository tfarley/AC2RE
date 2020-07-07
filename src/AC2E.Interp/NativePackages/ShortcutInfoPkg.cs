using AC2E.Dat;
using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class ShortcutInfoPkg : IPackage {

        public NativeType nativeType => NativeType.SHORTCUTINFO;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public ShortcutType _type;
        public string _data_str;
        public InstanceId _data_iid;
        public uint _data_enum;
        public DataId _data_dataid;

        public void write(BinaryWriter data, List<IPackage> references) {
            // TODO: Guessing on the types here
            switch (_type) {
                case ShortcutType.UNDEF:
                    data.Write((uint)0);
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
                    data.WriteEncryptedString(_data_str);
                    break;
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
