using System.IO;

namespace AC2E.WLib {

    public class UpdateSkillInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateInfo;

        // WM_Skill::PostCEvt_Skill_UpdateInfo
        public SkillInfoPkg _info;

        public UpdateSkillInfoCEvt() {

        }

        public UpdateSkillInfoCEvt(BinaryReader data) {
            _info = data.UnpackPackage<SkillInfoPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_info);
        }
    }
}
