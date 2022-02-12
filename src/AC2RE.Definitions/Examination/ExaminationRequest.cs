using System.IO;

namespace AC2RE.Definitions;

public class ExaminationRequest : IHeapObject {

    public NativeType nativeType => NativeType.ExaminationRequest;

    // Const *_ExaminationRequestType
    public enum RequestType : uint {
        Undef = 0, // Undef_ExaminationRequestType

        Effect = 0x40000001, // Effect_ExaminationRequestType
        Item = 0x40000002, // Item_ExaminationRequestType
        Skill = 0x40000003, // Skill_ExaminationRequestType
        SkillPanel = 0x40000004, // SkillPanel_ExaminationRequestType
        Quest = 0x40000005, // Quest_ExaminationRequestType
        Act = 0x40000006, // Act_ExaminationRequestType
        Recipe = 0x40000007, // Recipe_ExaminationRequestType

        Vitae = 0x4000000A, // Vitae_ExaminationRequestType
        CraftSkill = 0x4000000B, // CraftSkill_ExaminationRequestType
        CraftSkillCategory = 0x4000000C, // CraftSkillCategory_ExaminationRequestType
        Unk1 = 0x4000000D,
    }

    public RequestType type; // _type
    public uint dataEnum; // _data_enum
    public DataId dataDid; // _data_DataID
    public bool admin; // _admin
    public InstanceId dataId; // _data_iid
    public uint unk1;
    public uint unk2;
    public uint unk3;

    public ExaminationRequest() {

    }

    public ExaminationRequest(AC2Reader data) {
        type = data.ReadEnum<RequestType>();
        admin = data.ReadBoolean();
        switch (type) {
            case RequestType.Undef:
                break;
            case RequestType.Effect:
            case RequestType.Skill:
            case RequestType.SkillPanel:
            case RequestType.Quest:
            case RequestType.Act:
                dataEnum = data.ReadUInt32();
                break;
            case RequestType.Item:
                dataId = data.ReadInstanceId();
                unk3 = data.ReadUInt32();
                break;
            case RequestType.Recipe:
            case RequestType.CraftSkill:
                dataDid = data.ReadDataId();
                break;
            case RequestType.Unk1:
                dataId = data.ReadInstanceId();
                unk1 = data.ReadUInt32();
                unk2 = data.ReadUInt32();
                unk3 = data.ReadUInt32();
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        data.Write(admin);
        switch (type) {
            case RequestType.Undef:
                break;
            case RequestType.Effect:
            case RequestType.Skill:
            case RequestType.SkillPanel:
            case RequestType.Quest:
            case RequestType.Act:
                data.Write(dataEnum);
                break;
            case RequestType.Item:
                data.Write(dataId);
                data.Write(unk3);
                break;
            case RequestType.Recipe:
            case RequestType.CraftSkill:
                data.Write(dataDid);
                break;
            case RequestType.Unk1:
                data.Write(dataId);
                data.Write(unk1);
                data.Write(unk2);
                data.Write(unk3);
                break;
            default:
                throw new InvalidDataException(type.ToString());
        }
    }
}
