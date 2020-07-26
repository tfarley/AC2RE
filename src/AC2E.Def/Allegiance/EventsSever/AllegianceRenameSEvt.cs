namespace AC2E.Def {

    public class AllegianceRenameSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceRename;

        // WM_Allegiance::SendSEvt_AllegianceRename
        public WPString name; // _name

        public AllegianceRenameSEvt(AC2Reader data) {
            name = data.UnpackPackage<WPString>();
        }
    }
}
