using System.IO;

namespace AC2E.Def {

    public class ExaminationRequest : IPackage {

        public NativeType nativeType => NativeType.EXAMINATIONREQUEST;

        public ExaminationRequestType _type;
        public uint _data_enum;
        public DataId _data_DataID;
        public bool _admin;
        public InstanceId _data_iid;
        public uint unk1;
        public uint unk2;
        public uint unk3;

        public ExaminationRequest() {

        }

        public ExaminationRequest(BinaryReader data) {
            _type = (ExaminationRequestType)data.ReadUInt32();
            _admin = data.ReadUInt32() != 0;
            switch (_type) {
                case ExaminationRequestType.UNDEF:
                    break;
                case ExaminationRequestType.EFFECT:
                case ExaminationRequestType.SKILL:
                case ExaminationRequestType.SKILL_PANEL:
                case ExaminationRequestType.QUEST:
                case ExaminationRequestType.ACT:
                    _data_enum = data.ReadUInt32();
                    break;
                case ExaminationRequestType.ITEM:
                    _data_iid = data.ReadInstanceId();
                    unk3 = data.ReadUInt32();
                    break;
                case ExaminationRequestType.RECIPE:
                case ExaminationRequestType.CRAFT_SKILL:
                    _data_DataID = data.ReadDataId();
                    break;
                case ExaminationRequestType.UNK1:
                    _data_iid = data.ReadInstanceId();
                    unk1 = data.ReadUInt32();
                    unk2 = data.ReadUInt32();
                    unk3 = data.ReadUInt32();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write((uint)_type);
            data.Write(_admin ? (uint)1 : (uint)0);
            switch (_type) {
                case ExaminationRequestType.UNDEF:
                    break;
                case ExaminationRequestType.EFFECT:
                case ExaminationRequestType.SKILL:
                case ExaminationRequestType.SKILL_PANEL:
                case ExaminationRequestType.QUEST:
                case ExaminationRequestType.ACT:
                    data.Write(_data_enum);
                    break;
                case ExaminationRequestType.ITEM:
                    data.Write(_data_iid);
                    data.Write(unk3);
                    break;
                case ExaminationRequestType.RECIPE:
                case ExaminationRequestType.CRAFT_SKILL:
                    data.Write(_data_DataID);
                    break;
                case ExaminationRequestType.UNK1:
                    data.Write(_data_iid);
                    data.Write(unk1);
                    data.Write(unk2);
                    data.Write(unk3);
                    break;
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
