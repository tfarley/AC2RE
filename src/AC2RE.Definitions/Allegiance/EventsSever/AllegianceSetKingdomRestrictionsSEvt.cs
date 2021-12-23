namespace AC2RE.Definitions;

public class AllegianceSetKingdomRestrictionsSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceSetKingdomRestrictions;

    // WM_Allegiance::SendSEvt_AllegianceSetKingdomRestrictions
    public bool allowNeutrals; // _fAllowNeutrals
    public bool restrictionsOn; // _fRestrictionsOn

    public AllegianceSetKingdomRestrictionsSEvt(AC2Reader data) {
        allowNeutrals = data.UnpackBoolean();
        restrictionsOn = data.UnpackBoolean();
    }
}
