using System.IO;

namespace AC2E.Def {

    public class ExaminationRequest : IPackage {

        public NativeType nativeType => NativeType.EXAMINATIONREQUEST;

        public ExaminationRequestType type; // _type
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
            type = (ExaminationRequestType)data.ReadUInt32();
            admin = data.ReadBoolean();
            switch (type) {
                case ExaminationRequestType.UNDEF:
                    break;
                case ExaminationRequestType.EFFECT:
                case ExaminationRequestType.SKILL:
                case ExaminationRequestType.SKILL_PANEL:
                case ExaminationRequestType.QUEST:
                case ExaminationRequestType.ACT:
                    dataEnum = data.ReadUInt32();
                    break;
                case ExaminationRequestType.ITEM:
                    dataId = data.ReadInstanceId();
                    unk3 = data.ReadUInt32();
                    break;
                case ExaminationRequestType.RECIPE:
                case ExaminationRequestType.CRAFT_SKILL:
                    dataDid = data.ReadDataId();
                    break;
                case ExaminationRequestType.UNK1:
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
                case ExaminationRequestType.UNDEF:
                    break;
                case ExaminationRequestType.EFFECT:
                case ExaminationRequestType.SKILL:
                case ExaminationRequestType.SKILL_PANEL:
                case ExaminationRequestType.QUEST:
                case ExaminationRequestType.ACT:
                    data.Write(dataEnum);
                    break;
                case ExaminationRequestType.ITEM:
                    data.Write(dataId);
                    data.Write(unk3);
                    break;
                case ExaminationRequestType.RECIPE:
                case ExaminationRequestType.CRAFT_SKILL:
                    data.Write(dataDid);
                    break;
                case ExaminationRequestType.UNK1:
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
