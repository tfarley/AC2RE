namespace AC2E.Def {

    public class AnnounceWhoKilledMeCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__AnnounceWhoKilledMe;

        // WM_Death::PostCEvt_Death_AnnounceWhoKilledMe
        public StringInfo killerName; // _siKiller
        public InstanceId killerId; // _iidKiller

        public AnnounceWhoKilledMeCEvt() {

        }

        public AnnounceWhoKilledMeCEvt(AC2Reader data) {
            killerName = data.UnpackPackage<StringInfo>();
            killerId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(killerName);
            data.Pack(killerId);
        }
    }
}
