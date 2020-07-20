namespace AC2E.Def {

    public class ResurrectionRequestedByOtherCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__ResurrectionRequestedByOther;

        // WM_Death::PostCEvt_ResurrectionRequestedByOther
        public ResurrectionRequest _rezReq;

        public ResurrectionRequestedByOtherCEvt() {

        }

        public ResurrectionRequestedByOtherCEvt(AC2Reader data) {
            _rezReq = data.UnpackPackage<ResurrectionRequest>();
        }

        public void write(AC2Writer data) {
            data.Pack(_rezReq);
        }
    }
}
