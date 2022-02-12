using System.IO;
using System.Text;

namespace AC2RE.Definitions;

public class ShortcutInfo : IHeapObject {

    public NativeType nativeType => NativeType.ShortcutInfo;

    public ShortcutType type; // _type
    public string valString; // _data_str
    public InstanceId valId; // _data_iid
    public uint valEnum; // _data_enum
    public DataId valDid; // _data_dataid

    public ShortcutInfo() {

    }

    public ShortcutInfo(AC2Reader data) {
        // TODO: Guessing on the types here
        type = data.ReadEnum<ShortcutType>();
        switch (type) {
            case ShortcutType.Undef:
                break;
            case ShortcutType.Skill:
            case ShortcutType.Recipe:
            case ShortcutType.NewRecipe:
                valDid = data.ReadDataId();
                break;
            case ShortcutType.Item:
                valId = data.ReadInstanceId();
                break;
            case ShortcutType.Alias:
                valString = data.ReadString(Encoding.Unicode);
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }

    public void write(AC2Writer data) {
        // TODO: Guessing on the types here
        data.WriteEnum(type);
        switch (type) {
            case ShortcutType.Undef:
                break;
            case ShortcutType.Skill:
            case ShortcutType.Recipe:
            case ShortcutType.NewRecipe:
                data.Write(valDid);
                break;
            case ShortcutType.Item:
                data.Write(valId);
                break;
            case ShortcutType.Alias:
                data.Write(valString, Encoding.Unicode);
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }
}
