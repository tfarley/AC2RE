namespace AC2E.Def {

    public class QueryAllegianceProfileSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__QueryAllegianceProfile;

        // WM_Allegiance::SendSEvt_QueryAllegianceProfile
        public InstanceId _trg;

        public QueryAllegianceProfileSEvt(AC2Reader data) {
            _trg = data.UnpackInstanceId();
        }
    }
}
