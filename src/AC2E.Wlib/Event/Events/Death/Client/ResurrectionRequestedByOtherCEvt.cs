using System.IO;

namespace AC2E.WLib {

    public class ResurrectionRequestedByOtherCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__ResurrectionRequestedByOther;

        // WM_Death::PostCEvt_ResurrectionRequestedByOther
        public ResurrectionRequestPkg _rezReq;

        public ResurrectionRequestedByOtherCEvt() {

        }

        public ResurrectionRequestedByOtherCEvt(BinaryReader data) {
            _rezReq = data.UnpackPackage<ResurrectionRequestPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_rezReq);
        }
    }
}
