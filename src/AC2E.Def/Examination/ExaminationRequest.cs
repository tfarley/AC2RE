using System.IO;

namespace AC2E.Def {

    public class ExaminationRequest : IPackage {

        public NativeType nativeType => NativeType.EXAMINATIONREQUEST;

        // Const *_ExaminationRequestType
        public enum RequestType : uint {
            UNDEF = 0,

            EFFECT = 0x40000001,
            ITEM = 0x40000002,
            SKILL = 0x40000003,
            SKILL_PANEL = 0x40000004,
            QUEST = 0x40000005,
            ACT = 0x40000006,
            RECIPE = 0x40000007,

            VITAE = 0x4000000A,
            CRAFT_SKILL = 0x4000000B,
            CRAFT_SKILL_CATEGORY = 0x4000000C,
            UNK1 = 0x4000000D,
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
            type = (RequestType)data.ReadUInt32();
            admin = data.ReadBoolean();
            switch (type) {
                case RequestType.UNDEF:
                    break;
                case RequestType.EFFECT:
                case RequestType.SKILL:
                case RequestType.SKILL_PANEL:
                case RequestType.QUEST:
                case RequestType.ACT:
                    dataEnum = data.ReadUInt32();
                    break;
                case RequestType.ITEM:
                    dataId = data.ReadInstanceId();
                    unk3 = data.ReadUInt32();
                    break;
                case RequestType.RECIPE:
                case RequestType.CRAFT_SKILL:
                    dataDid = data.ReadDataId();
                    break;
                case RequestType.UNK1:
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
            data.Write((uint)type);
            data.Write(admin);
            switch (type) {
                case RequestType.UNDEF:
                    break;
                case RequestType.EFFECT:
                case RequestType.SKILL:
                case RequestType.SKILL_PANEL:
                case RequestType.QUEST:
                case RequestType.ACT:
                    data.Write(dataEnum);
                    break;
                case RequestType.ITEM:
                    data.Write(dataId);
                    data.Write(unk3);
                    break;
                case RequestType.RECIPE:
                case RequestType.CRAFT_SKILL:
                    data.Write(dataDid);
                    break;
                case RequestType.UNK1:
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
}
