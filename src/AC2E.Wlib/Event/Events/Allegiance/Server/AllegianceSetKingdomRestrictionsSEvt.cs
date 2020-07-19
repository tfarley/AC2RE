using System.IO;

namespace AC2E.WLib {

    public class AllegianceSetKingdomRestrictionsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceSetKingdomRestrictions;

        // WM_Allegiance::SendSEvt_AllegianceSetKingdomRestrictions
        public bool _fAllowNeutrals;
        public bool _fRestrictionsOn;

        public AllegianceSetKingdomRestrictionsSEvt(BinaryReader data) {
            _fAllowNeutrals = data.UnpackUInt32() != 0;
            _fRestrictionsOn = data.UnpackUInt32() != 0;
        }
    }
}
