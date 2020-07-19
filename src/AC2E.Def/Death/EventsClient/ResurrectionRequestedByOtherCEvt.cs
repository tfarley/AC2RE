using System.IO;

namespace AC2E.Def {

    public class ResurrectionRequestedByOtherCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__ResurrectionRequestedByOther;

        // WM_Death::PostCEvt_ResurrectionRequestedByOther
        public ResurrectionRequest _rezReq;

        public ResurrectionRequestedByOtherCEvt() {

        }

        public ResurrectionRequestedByOtherCEvt(BinaryReader data) {
            _rezReq = data.UnpackPackage<ResurrectionRequest>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_rezReq);
        }
    }
}
