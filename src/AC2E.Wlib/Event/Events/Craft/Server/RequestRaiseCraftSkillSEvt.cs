using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestRaiseCraftSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Craft__RequestRaiseCraftSkill;

        // WM_Craft::SendSEvt_RequestRaiseCraftSkill
        public DataId _didCraftSkill;

        public RequestRaiseCraftSkillSEvt(BinaryReader data) {
            _didCraftSkill = data.UnpackDataId();
        }
    }
}
