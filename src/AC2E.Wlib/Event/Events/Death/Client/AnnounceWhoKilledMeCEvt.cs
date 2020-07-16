using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AnnounceWhoKilledMeCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__AnnounceWhoKilledMe;

        // WM_Death::PostCEvt_Death_AnnounceWhoKilledMe
        public StringInfo _siKiller;
        public InstanceId _iidKiller;

        public AnnounceWhoKilledMeCEvt() {

        }

        public AnnounceWhoKilledMeCEvt(BinaryReader data) {
            _siKiller = data.UnpackPackage<StringInfo>();
            _iidKiller = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_siKiller);
            data.Pack(_iidKiller);
        }
    }
}
