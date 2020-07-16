using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class CreateFellowshipSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__CreateFellowship;

        // WM_Fellowship::SendSEvt_CreateFellowship
        public uint _lootingMethod;
        public bool _social;
        public WPString _name;

        public CreateFellowshipSEvt(BinaryReader data) {
            _lootingMethod = data.UnpackUInt32();
            _social = data.UnpackUInt32() != 0;
            _name = data.UnpackPackage<WPString>();
        }
    }
}
