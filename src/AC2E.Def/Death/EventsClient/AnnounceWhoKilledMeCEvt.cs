namespace AC2E.Def {

    public class AnnounceWhoKilledMeCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__AnnounceWhoKilledMe;

        // WM_Death::PostCEvt_Death_AnnounceWhoKilledMe
        public StringInfo _siKiller;
        public InstanceId _iidKiller;

        public AnnounceWhoKilledMeCEvt() {

        }

        public AnnounceWhoKilledMeCEvt(AC2Reader data) {
            _siKiller = data.UnpackPackage<StringInfo>();
            _iidKiller = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_siKiller);
            data.Pack(_iidKiller);
        }
    }
}
