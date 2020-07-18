using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class VassalSwearDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Vassal_SwearDone;

        // WM_Allegiance::PostCEvt_Vassal_SwearDone
        public uint _etype;
        public StringInfo _patron_name;
        public InstanceId _patron;

        public VassalSwearDoneCEvt() {

        }

        public VassalSwearDoneCEvt(BinaryReader data) {
            _etype = data.UnpackUInt32();
            _patron_name = data.UnpackPackage<StringInfo>();
            _patron = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_etype);
            data.Pack(_patron_name);
            data.Pack(_patron);
        }
    }
}
