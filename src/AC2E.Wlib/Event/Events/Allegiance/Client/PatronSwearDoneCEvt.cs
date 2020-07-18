using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class PatronSwearDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Patron_SwearDone;

        // WM_Allegiance::PostCEvt_Patron_SwearDone
        public uint _etype;
        public StringInfo _vassal_name;
        public InstanceId _vassal;

        public PatronSwearDoneCEvt() {

        }

        public PatronSwearDoneCEvt(BinaryReader data) {
            _etype = data.UnpackUInt32();
            _vassal_name = data.UnpackPackage<StringInfo>();
            _vassal = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_etype);
            data.Pack(_vassal_name);
            data.Pack(_vassal);
        }
    }
}
